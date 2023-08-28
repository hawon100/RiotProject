using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance => _instance;

    public AudioClip curMap;

    public void Start()
    {
        _instance = this;
        Setting();
    }

    public void Setting(){
        Managers.Sound.Play(curMap, Define.Sound.Effect);
    }
}
