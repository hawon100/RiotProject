using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    public int MapSpawnIndex = 0;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Lobby;

        List<GameObject> mapObj = new();
        for(int i = 0; i < MapSpawnIndex; i++)
        {
            mapObj.Add(Managers.Resource.Instantiate("Stage/Map"));
        }
    }

    private void Update()
    {

    }

    public override void Clear()
    {
        Debug.Log("LoginScene Clear!");
    }
}
