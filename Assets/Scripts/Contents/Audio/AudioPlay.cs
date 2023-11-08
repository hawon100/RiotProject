using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    private void Start()
    {
        var objs = FindObjectsOfType<AudioPlay>();
        if (objs.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Player.Instance.isLobby)
        {
            Destroy(gameObject);
        }
    }
}
