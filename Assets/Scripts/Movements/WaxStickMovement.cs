using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WaxStickMovement : MonoBehaviour
{
    [Header("WaxModelOnly(Depend On Level)")]
    [SerializeField] private GameObject waxModel;

    [Header("Components")]
    private Camera _mainCamera;
    private Sequence mySequence;

    [Header("Layer Order(int)")]
    private int mask = 1;

    void Start()
    {
        _mainCamera = Camera.main;

        mask = 1 << LayerMask.NameToLayer("Ignore_Occlude");
        mask = 1 << LayerMask.NameToLayer("Occlude");
    }
    
    void Update()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out RaycastHit raycastHit,mask))
        {
            if (raycastHit.collider.gameObject.layer == LayerMask.NameToLayer("Occlude"))
            {
                //playerObject.transform.position = raycastHit.point;
                transform.position = new Vector3(raycastHit.point.x - 1, this.transform.position.y, raycastHit.point.z - 1);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            mySequence.Kill();
            mySequence.Append(waxModel.transform.DOMoveY(2, 0.25f));
        }

        if (Input.GetMouseButtonUp(0))
        {
            mySequence.Kill();
            mySequence.Append(waxModel.transform.DOMoveY(3, 0.25f));
        }

    }
}
