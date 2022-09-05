using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintObject : MonoBehaviour
{
    [SerializeField] private MeshRenderer paintableObject;
    [SerializeField] private Texture2D brush;
    [SerializeField] private Vector2Int textureArea;
    [SerializeField] private ParticleSystem particleEffect;

    [Header("Brush Stroke Apply Settings")]
    [SerializeField] private bool applyImmidiate;
    [SerializeField] private float delayTimeToApply;


    ParticleSystem.EmissionModule oThrusterEmission;
    private Texture2D _texture;

    public bool isPainting;

    void Start()
    {
        oThrusterEmission = particleEffect.emission;
        _texture = new Texture2D(textureArea.x, textureArea.y, TextureFormat.ARGB32, false);
        paintableObject.material.mainTexture = _texture;
    }
    
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            RaycastHit hitInfo;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo ))
            {
                Paint(hitInfo.textureCoord);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isPainting = false;
            LevelFacade.Instance.CheckLevelComplete(_texture);

            CancelInvoke();
            Invoke(nameof(ApplyTexture), delayTimeToApply);
        }

        //Particle System Behavior
        if (isPainting == false)
        {
            oThrusterEmission.enabled=false;
        }
        else
        {
            oThrusterEmission.enabled = true;
        }
    }

    #region PRIVATE ATTRIBUTES

    private void Paint(Vector2 cordinates)
    {
        cordinates.x *= _texture.width;
        cordinates.y *= _texture.height;

        Color32[] textureC32 = _texture.GetPixels32();
        Color32[] brushC32 = brush.GetPixels32();   
        
        Vector2Int halfBrushSize = new Vector2Int(brush.width / 2, brush.height / 2);

        for (int x = 0; x < brush.width; x++)
        {
            int PosX = x - halfBrushSize.x + (int)cordinates.x;

            if (PosX <= 0 || PosX >= _texture.width)
                continue;

            for (int y = 0; y < brush.height; y++)
            {
                int PosY = y - halfBrushSize.y + (int)cordinates.y;
                if (PosY <= 0 || PosY >= _texture.height)
                    continue;

                if (brushC32[x+ (y*brush.width)].a > 0f)
                {
                    int totalPos = PosX + (_texture.width * PosY);

                    if (brushC32[x + (y * brush.width)].r < textureC32[totalPos].r)
                        textureC32[totalPos] = brushC32[x + (y * brush.width)];
                }                
            }
        }

        //DelayPainting

        _texture.SetPixels32(textureC32);
        if (isPainting == false)
        {
            InvokeRepeating(nameof(ApplyTexture), delayTimeToApply, delayTimeToApply);
        }

        if (applyImmidiate == true)
        {
            _texture.Apply();
        }

        isPainting = true;

        //OBSOLETE
        //Debug.Log(CalculateFill(textureC32, paintableArea.material.color, 0.5f));
        //OBSOLETE
    }

    private void ApplyTexture()
    {
        _texture.Apply();
    }

    #endregion

    [System.Obsolete]
    #region STATIC FIELD
    static float CalculateFill(Color32[] colors, Color reference, float tolerance)
    {
        Vector3 target = new Vector3 { x = reference.r, y = reference.g, z = reference.b };
        int numHits = 0;
        const float sqrt_3 = 1.73205080757f;
        for (int i = 0; i < colors.Length; i++)
        {
            Vector3 next = new Vector3 { x = colors[i].r, y = colors[i].g, z = colors[i].b };
            float mag = Vector3.Magnitude(target - next) / sqrt_3;
            numHits += mag <= tolerance ? 1 : 0;
        }
        return (float)numHits / (float)colors.Length; ;
    }

    #endregion

}
