using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HairColliderController : MonoBehaviour
{
    private LevelFacade levelFacade;
    private Sequence mySequence;
    private Vector3 defaultPos;

    private void Start()
    {
        levelFacade = LevelFacade.Instance;
        levelFacade.hairFindPosition += FindDefaultPosition;
    }

    #region TRIGGERS AND COLLIDERS
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
    #endregion

    #region EVENTS AND ACTIONS

    private void FindDefaultPosition()
    {
        defaultPos = this.gameObject.transform.position;
    }

    #endregion

}
