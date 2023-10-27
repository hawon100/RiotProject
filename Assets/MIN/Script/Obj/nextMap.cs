using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextMap : Obj_Base
{
    protected override void UseObj()
    {
        if(MoveManager.Instance.curMoveMob.Count == 0 && MoveManager.Instance.curCheckMob.Count == 0) isLock = false;

        if(isLock) return;

        Managers.Map.LoadScene(Define.Scene.InGame);
    }
}
