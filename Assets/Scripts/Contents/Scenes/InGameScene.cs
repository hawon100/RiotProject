using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.InGame;
    }

    public override void Clear()
    {
        Debug.Log("InGameScene Clear!");
    }
}
