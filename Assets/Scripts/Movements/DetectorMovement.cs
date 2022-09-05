using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorMovement : MonoBehaviour
{
    public bool isActive;

    void Start()
    {
        isActive = false;
    }

    void Update()
    {
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