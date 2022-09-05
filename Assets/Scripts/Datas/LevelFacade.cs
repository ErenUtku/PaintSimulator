using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Events;

public class LevelFacade : MonoBehaviour
{
    [Header("Game Start")]
    [SerializeField] private GameObject playfieldObject;
    [SerializeField] private float pixelOnBoard;
    public GameObject paintableArea;

    [Header("Static Objects")]
    [SerializeField] private DetectorMovement detectorObject;
    [SerializeField] CheckpointContainer checkpointContainer;

    private LevelUIController levelUIController;

    [Header("Level End Circumstances")]
    private bool isTraveledDone;

    [Header("Events and Actions")]
    public Action hairFindPosition;

    #region STATIC ATTRIBUTES

    public static LevelFacade Instance;

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

    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    { 
        levelUIController = LevelUIController.Instance;
        GameStart();
    }

    #region PUBLIC ATTRIBUTES

    public void CheckLevelComplete(Texture2D texture)
    {
        if(CalculatePixelSize(texture)>= LevelCompleteThreshold() && CheckpointTracker(out isTraveledDone))
        {
            detectorObject.gameObject.SetActive(false);
            levelUIController.PullButtonActivation(true);
            return;
        }
    }

    public float LevelCompleteThreshold()
    {
        return pixelOnBoard *1000;
    }

    #endregion

    #region PRIVATES ATTRIBUTES

    private bool CheckpointTracker(out bool isTraveledDone)
    {
        foreach (var checkpoint in checkpointContainer.checkPointList)
        {
            if (checkpoint.isTouched == false)
            {
                return isTraveledDone = false;
            }
        }
        return isTraveledDone = true;
    }

    private void GameStart()
    {
        detectorObject.gameObject.SetActive(false);

        playfieldObject.transform.DOMoveX(0, 3f).OnComplete(() =>
        {
            detectorObject.gameObject.SetActive(true);
            hairFindPosition?.Invoke();
        });
    }

    #endregion

    #region GETTERS

    public DetectorMovement GetDetectorObject()
    {
        return detectorObject;
    }

    public CheckpointContainer GetCheckPointContainer()
    {
        return checkpointContainer;
    }

    #endregion

}
