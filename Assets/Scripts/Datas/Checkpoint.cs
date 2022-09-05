using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isTouched = false;

    private DetectorMovement detectorObject;
    private CheckpointContainer checkpointContainer;
    private BoxCollider _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }
    private void Start()
    {
        detectorObject = LevelFacade.Instance.GetDetectorObject();
        checkpointContainer = LevelFacade.Instance.GetCheckPointContainer();

        //Adding Checkpoint to the list
        checkpointContainer.checkPointList.Add(this);
    }

    #region TRIGGER AND COLLISIONS
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "WaxObject" && detectorObject.isActive)
        {
            isTouched = true;
            _boxCollider.enabled = false;
        }
    }
    #endregion

}
