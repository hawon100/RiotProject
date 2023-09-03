using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioClip curMap;

    public void Init()
    {
        Setting();
    }

    public void Setting()
    {
        Managers.Sound.Play(curMap, Define.Sound.Effect);
    }
}
