using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextMap : Obj_Base
{
    protected override void UseObj()
    {
        if (MoveManager.Instance.curMoveMob.Count == 0 && MoveManager.Instance.curCheckMob.Count == 0) isLock = false;

        if (isLock) return;

        if (RoundData.Instance.mapIndex < 3)
        {
            RoundData.Instance.mapIndex++;
            FadeLobby.Instance.fadeInGameAnim.SetTrigger("OnFade");
        }
        else
        {
            RoundData.Instance.Reset();
            MoveManager.Instance.clearWin.SetActive(true);
        }
    }
}
