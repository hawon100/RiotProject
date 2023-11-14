using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFlame : MonoBehaviour
{
    public static CenterFlame Instance;

    private void Start()
    {
        Instance = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!AudioPlay.Instance.musicStart)
        {
            if (collision.CompareTag("Note"))
            {
                AudioPlay.Instance.myAudio.Play();
                AudioPlay.Instance.musicStart = true;
            }
        }
    }
}
