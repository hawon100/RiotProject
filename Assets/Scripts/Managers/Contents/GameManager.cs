using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance => _instance;
    [SerializeField] private MoveManager moveManager;

    public AudioClip curMap;

    private void Awake() {
        Init();
        moveManager.Init(); 
    }

    public void Init()
    {
        _instance = this;
    }

    public void Setting()
    {
        Managers.Sound.Play(curMap, Define.Sound.Effect);
    }
}
