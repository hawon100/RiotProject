using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStage : Obj_Base
{
    [SerializeField] private int stageIndex;

    public override void UseObj()
    {
        RoundData.Instance.Reset(stageIndex);
        FadeLobby.Instance.fadeInGameAnim.SetTrigger("OnFade");
        Managers.Sound.Play(FadeLobby.Instance.fadeSound);
    }
}
