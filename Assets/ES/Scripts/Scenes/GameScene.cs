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

        //Spawn();
    }

    void Spawn()
    {
        List<GameObject> list = new List<GameObject>();
        List<Enemy> enemy = new List<Enemy>();
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = Managers.Resource.Instantiate("Enemy/m_bat");
            Enemy emy = obj.GetComponent<Enemy>();
            emy._target = _target;
            list.Add(obj);
            enemy.Add(emy);
        }
    }

    public override void Clear()
    {

    }
}
