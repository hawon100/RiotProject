using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Animator directorPlayer;
    [SerializeField] private Transform directorCamPos;
    [SerializeField] private Camera cam;

    public static bool isDoubleTouch;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Title;
        isDoubleTouch = false;
    }

    private void Update()
    {
        if(isDoubleTouch)
        {
            directorPlayer.SetTrigger("OnDirector");
            panel.SetActive(false);
            cam.gameObject.transform.position = directorCamPos.position;
            cam.gameObject.transform.rotation = directorCamPos.rotation;
            cam.fieldOfView = 69f;
            isDoubleTouch = false;
        }
    }

    public override void Clear()
    {
        Debug.Log("LoginScene Clear!");
    }
}
