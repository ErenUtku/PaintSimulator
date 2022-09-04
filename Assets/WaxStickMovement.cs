using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaxStickMovement : MonoBehaviour
{
    [SerializeField] private GameObject waxObject;
    [SerializeField] private LayerMask waxLayer;
    private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out RaycastHit raycastHit, waxLayer))
        {
            //waxObject.transform.position = raycastHit.point;
            waxObject.transform.position = new Vector3(raycastHit.point.x -1,1, raycastHit.point.z -1);
        }
    }
}
