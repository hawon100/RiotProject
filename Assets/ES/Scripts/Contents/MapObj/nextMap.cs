using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextMap : Obj_Base
{
    public int endStageCount;

    public override void UseObj()
    {
        MoveManager.Instance.allStop = true;
        if (RoundData.Instance.mapIndex < endStageCount)
        {
            RoundData.Instance.mapIndex++;
            FadeLobby.Instance.fadeInGameAnim.SetTrigger("OnFade");
            Managers.Sound.Play(FadeLobby.Instance.fadeSound);
        }
        else
        {
            RoundData.Instance.Reset(0);
            MoveManager.Instance.clearWin.SetActive(true);
        }
    }
}
