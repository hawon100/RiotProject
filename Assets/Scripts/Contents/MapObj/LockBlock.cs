using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBlock : Obj_Base
{
    public override void UseObj()
    {
        if(!Player.Instance.isKey) return;

        Player.Instance.isKey = false;

        MoveManager.Instance.InOutObj(curPos);
        Destroy(gameObject);
    }
}
