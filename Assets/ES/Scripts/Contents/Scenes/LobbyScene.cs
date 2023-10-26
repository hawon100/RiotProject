using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Animator directorPlayer;
    [SerializeField] private Transform directorCamPos;
    [SerializeField] private Camera camera;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Lobby;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            directorPlayer.SetTrigger("OnDirector");
            panel.SetActive(false);
            camera.gameObject.transform.position = directorCamPos.position;
            camera.gameObject.transform.rotation = directorCamPos.rotation;
            camera.fieldOfView = 69f;
        }
    }

    public override void Clear()
    {
        Debug.Log("LoginScene Clear!");
    }
}
