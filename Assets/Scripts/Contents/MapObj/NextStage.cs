using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStage : Obj_Base
{
    [SerializeField] private int stageIndex;

    public override void UseObj()
    {
        if(isLock) return;

        RoundData.Instance.Reset();
        RoundData.Instance.stageIndex = stageIndex;

        Managers.Map.LoadScene(Define.Scene.InGame);
    }
}
