using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFacade : MonoBehaviour
{
    [SerializeField] private float pixelOnBoard;

    [SerializeField] private GameObject levelEndObject;

    public static LevelFacade Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        levelEndObject.SetActive(false);
    }

    public float LevelCompleteThreshold()
    {
        return pixelOnBoard;
    }

    public void CheckLevelComplete(Texture2D texture)
    {
        if(CalculatePixelSize(texture)>= pixelOnBoard)
        {
            levelEndObject.SetActive(true);
            Debug.Log("Level Completed");
        }
    }

    public static int CalculatePixelSize(Texture2D t)
    {
        int SkipPixels = 10;//<smaller number here is more accurate and slow
        int tested = 0;
        int tran = 0;
        int x = 0;
        while (x < t.width)
        {
            int y = 0;
            while (y < t.height)
            {
                tested++;
                if (t.GetPixel(x, y).a < .5f) { tran++; }
                y += SkipPixels;
            }
            x += SkipPixels;
        }

        float percent = (float)tran / (float)tested;
        tran = Mathf.RoundToInt(t.width * t.height * percent);

        //print(" i have sampled " + tested + " pixels in a grid pattern");
        //print((percent * 100) + "% had alpha below .5");
        //print(" so i estimate " + tran  + " pixels in your _texture are transparent");

        return tran;
    }

    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
