using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelUIController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private GameObject levelEndButton;
    [SerializeField] private GameObject pullItButton;

    [Header("Level End Conditions")]
    private GameObject paintableObject;
    private int pullTime = 0;

    public static LevelUIController Instance;
    private void Awake()
    {
        Instance = this;

        levelEndButton.SetActive(false);
        pullItButton.SetActive(false);
    }

    private void Start()
    {
        paintableObject=LevelFacade.Instance.paintableArea;
    }

    #region LEVEL_END_CONDITIONS

    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void LevelFinishAnimation()
    {
        pullTime++;

        paintableObject.transform.DOLocalMoveY(0.1f, 0.1f).OnComplete(() =>
        {
            paintableObject.transform.DOLocalMoveY(0f, 0.1f).OnComplete(() =>
            {
                if (pullTime >= 3)
                {
                    pullItButton.SetActive(false);
                    paintableObject.transform.DOMove(new Vector3(-10, 20, 0), 2f).OnComplete(() =>
                    {
                        levelEndButton.SetActive(true);
                    });
                }
            });
        });

    }

    public void PullButtonActivation(bool value)
    {
        pullItButton.SetActive(value);
    }

    #endregion

}
