using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HairColliderController : MonoBehaviour
{
    [SerializeField] private LevelFacade levelFacade;
    private Sequence mySequence;
    public Vector3 defaultPos;


    private void Start()
    {
        levelFacade.hairFindPosition += FindDefaultPosition;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("WaxObject"))
        {
            if (mySequence == null)
            {
                mySequence.Append(this.transform.DOShakePosition(0.25f,0.3f,2,90)).OnComplete(()=> 
                {
                    transform.DOMove(defaultPos, 0.1f);
                });
            }
        }   
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == ("WaxObject"))
        {
            transform.DOMove(defaultPos, 0.1f).OnComplete(()=> {
                mySequence.Kill();
            });
        }
    }

    private void FindDefaultPosition()
    {
        defaultPos = this.gameObject.transform.position;
    }


}
