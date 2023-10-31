using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeLobby : MonoBehaviour
{
    public static FadeLobby Instance { get; private set; }
    public Animator fadeInGameAnim;
    public Animator fadeLobbyAnim;

    private void Awake()
    {
        Instance = this;
    }

    public void OnNextScene()
    {
        Managers.Map.LoadScene(Define.Scene.InGame);
    }

    public void OnLobbyScene()
    {
        Managers.Map.LoadScene(Define.Scene.Lobby);
    }
}
