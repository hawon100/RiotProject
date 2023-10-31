using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStage : Obj_Base
{
    [SerializeField] private int stageIndex;

    protected override void UseObj()
    {
        if(isLock) return;

        RoundData.Instance.Reset();
        RoundData.Instance.stageIndex = stageIndex;
        FadeLobby.Instance.fadeInGameAnim.SetTrigger("OnFade");
    }
}
