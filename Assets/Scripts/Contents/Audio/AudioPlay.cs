using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    public static AudioPlay Instance { get; private set; }
    public bool musicStart = false;
    public AudioSource myAudio;
    private Define.Scene type;

    public void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            myAudio = GetComponent<AudioSource>();
            myAudio.loop = true;
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
