using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Lobby;
    }

    private void Update()
    {

    }

    public override void Clear()
    {
        Debug.Log("LoginScene Clear!");
    }
}
