using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorMovement : MonoBehaviour
{
    [SerializeField] private GameObject waxObject;

    public bool isActive;

    void Start()
    {
        isActive = false;
    }

    void Update()
    {
        transform.position = new Vector3(waxObject.transform.position.x, -2, waxObject.transform.position.z);

        //Checking Box Collider
        if (Input.GetMouseButtonDown(0))
        {
            isActive = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isActive = false;
        }
    }

}