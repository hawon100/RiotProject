using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextMap : Obj_Base
{
    public int endStageCount;

    public override void UseObj()
    {
        if (RoundData.Instance.mapIndex < endStageCount)
        {
            RoundData.Instance.mapIndex++;
            FadeLobby.Instance.fadeInGameAnim.SetTrigger("OnFade");
        }
        else
        {
            RoundData.Instance.Reset(0);
            MoveManager.Instance.clearWin.SetActive(true);
        }
    }
}
