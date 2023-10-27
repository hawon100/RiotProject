using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Animator directorPlayer;
    [SerializeField] private Transform directorCamPos;
    [SerializeField] private Camera camera;
    [SerializeField] private ParticleSystem slashEffect;

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
            camera.gameObject.transform.position = directorCamPos.position;
            camera.gameObject.transform.rotation = directorCamPos.rotation;
            camera.fieldOfView = 69f;
            slashEffect.Play();
            isDoubleTouch = false;
        }
    }

    public override void Clear()
    {
        Debug.Log("LoginScene Clear!");
    }
}
