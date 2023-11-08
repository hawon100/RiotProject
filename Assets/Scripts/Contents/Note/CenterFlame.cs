using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFlame : MonoBehaviour
{
    public static CenterFlame Instance;
    public AudioSource myAudio;
    private bool musicStart = false;

    private void Start()
    {
        Instance = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!musicStart)
        {
            if (collision.CompareTag("Note"))
            {
                myAudio.Play();
                musicStart = true;
            }
        }
    }
}
