using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintObject : MonoBehaviour
{
    [SerializeField] private MeshRenderer paintableObject;
    [SerializeField] private Texture2D brush;
    [SerializeField] private Vector2Int textureArea;
    private Texture2D _texture;

    void Start()
    {
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
            LevelFacade.Instance.CheckLevelComplete(_texture);
        }
    }

    private void Paint(Vector2 cordinates)
    {
        cordinates.x *= _texture.width;
        cordinates.y *= _texture.height;
        Color32[] textureC32 = _texture.GetPixels32();
        Color32[] brushC32 = brush.GetPixels32();
       
        Vector2Int halfBrush = new Vector2Int(brush.width / 2, brush.height / 2);


        for (int x = 0; x < brush.width; x++)
        {
            int xPos = x - halfBrush.x + (int)cordinates.x;
            if (xPos < 0 || xPos >= _texture.width)
                continue;

            for (int y = 0; y < brush.height; y++)
            {
                int yPos = y - halfBrush.y + (int)cordinates.y;
                if (yPos < 0 || yPos >= _texture.height)
                    continue;

                if (brushC32[x+ (y*brush.width)].a > 0f)
                {
                    int tPos = xPos + (_texture.width * yPos);

                    if (brushC32[x + (y * brush.width)].r < textureC32[tPos].r)
                        textureC32[tPos] = brushC32[x + (y * brush.width)];

                }                
            }
        }
        _texture.SetPixels32(textureC32);
        _texture.Apply();      
        //Debug.Log(CalculateFill(textureC32, paintableObject.material.color, 0.5f));
    }

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
    

}
