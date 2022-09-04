using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaxStickMovement : MonoBehaviour
{
    [SerializeField] private GameObject waxObject;
    private Camera _mainCamera;

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
                //waxObject.transform.position = raycastHit.point;
                waxObject.transform.position = new Vector3(raycastHit.point.x - 1, 2, raycastHit.point.z - 1);
            }
        }
    }
}
