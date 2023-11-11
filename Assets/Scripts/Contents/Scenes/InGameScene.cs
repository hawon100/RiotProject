using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameScene : BaseScene
{
    //GameObject audioObj;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.InGame;

        //audioObj = Managers.Resource.Instantiate("Audio/Audio");
    }

    //private void Update()
    //{
    //    CenterFlame.Instance.myAudio = audioObj.GetComponent<AudioSource>();
    //}

    public override void Clear()
    {
        Debug.Log("InGameScene Clear!");
    }
}
