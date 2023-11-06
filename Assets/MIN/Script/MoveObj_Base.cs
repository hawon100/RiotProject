using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveObj_Base : Obj_Base
{
    [SerializeField] private bool notMove;
    [SerializeField] private bool timing;

    public override void UseObj()
    {
        if(notMove) return;
    
        if(timing){
            gameObject.SetActive(true);
            MoveManager.Instance.InOutObj(curPos, index);
        }else{
            gameObject.SetActive(false);
            MoveManager.Instance.InOutObj(curPos);
        }

        timing = !timing;
    }
}
