using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairColliderController : MonoBehaviour
{
    private BoxCollider _boxCollider;
    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == ("WaxObject"))
        {
            _boxCollider.isTrigger = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("WaxObject"))
        {
            _boxCollider.isTrigger = true;
        }
    }
}
