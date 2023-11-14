using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Animator directorPlayer;
    [SerializeField] private RoundData data;
    [SerializeField] private Transform directorCamPos;
    [SerializeField] private Camera cam;
    [SerializeField] private AudioClip bgmclip;

    public static bool isDoubleTouch;

    protected override void Init()
    {
        base.Init();
        data.InitData();
        SceneType = Define.Scene.Title;
        isDoubleTouch = false;

        Managers.Sound.Play(bgmclip, Define.Sound.Bgm);
    }

    private void Update()
    {
        if (isDoubleTouch)
        {
            //direction
            directorPlayer.SetTrigger("OnDirector");
            Managers.Sound.Clear();
            panel.SetActive(false);
            cam.gameObject.transform.position = directorCamPos.position;
            cam.gameObject.transform.rotation = directorCamPos.rotation;

            isDoubleTouch = false;
        }
    }

    public override void Clear()
    {

    }
}
