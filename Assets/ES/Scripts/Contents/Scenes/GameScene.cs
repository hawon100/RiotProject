using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    [SerializeField] private Transform _target;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;
    }

    public override void Clear()
    {

    }
}
