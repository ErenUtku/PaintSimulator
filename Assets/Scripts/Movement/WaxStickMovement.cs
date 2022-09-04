using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WaxStickMovement : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject waxModel;
    private Camera _mainCamera;
    private Sequence mySequence;
    //Layer Masking
    private int mask = 1;

    void Start()
    {
        _mainCamera = Camera.main;
        mask = 1 << LayerMask.NameToLayer("Ignore_Occlude");
        mask = 1 << LayerMask.NameToLayer("Occlude");
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out RaycastHit raycastHit,mask))
        {
            if (raycastHit.collider.gameObject.layer == LayerMask.NameToLayer("Occlude"))
            {
                //playerObject.transform.position = raycastHit.point;
                playerObject.transform.position = new Vector3(raycastHit.point.x - 1, this.transform.position.y, raycastHit.point.z - 1);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            mySequence.Kill();
            mySequence.Append(waxModel.transform.DOMoveY(2, 1f));
        }

        if (Input.GetMouseButtonUp(0))
        {
            mySequence.Kill();
            mySequence.Append(waxModel.transform.DOMoveY(3, 1f));
        }

    }
}
