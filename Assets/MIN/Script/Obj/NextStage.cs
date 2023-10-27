using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStage : Obj_Base
{
    protected override void UseObj()
    {
        if(isLock) return;

        Debug.Log("NEXT");
        //Managers.Map.LoadScene(Define.Scene.InGame);
    }
}
