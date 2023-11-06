using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Obj_Base
{
    public override void UseObj()
    {
        Player.Instance.isKey = true;

        MoveManager.Instance.InOutObj(curPos);
        Destroy(gameObject);
    }
}
