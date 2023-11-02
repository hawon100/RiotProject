using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Animator directorPlayer;
    [SerializeField] private RoundData data;

    public static bool isDoubleTouch;

    protected override void Init()
    {
        base.Init();
        data.InitData();
        SceneType = Define.Scene.Title;
        isDoubleTouch = false;
        directorPlayer.SetTrigger("OnDirector");
        panel.SetActive(false);
    }

    private void Update()
    {
        if(isDoubleTouch)
        {
            //direction
            RoundData.Instance.Reset();
            Managers.Map.LoadScene(Define.Scene.Lobby);

            isDoubleTouch = false;
        }
    }

    public override void Clear()
    {
        Debug.Log("LoginScene Clear!");
    }
}
