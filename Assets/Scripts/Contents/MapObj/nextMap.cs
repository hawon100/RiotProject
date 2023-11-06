using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextMap : Obj_Base
{
    public override void UseObj()
    {
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
