using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }
        
    //Content
    GameManager _game = new GameManager();
    TalkManager _talk = new TalkManager();

    //Core
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SoundManager _sound = new SoundManager();
    MapManager _map = new MapManager();
    DataManager _data = new DataManager();

    //Content
    public static GameManager Game { get { return Instance._game; } }
    public static TalkManager Talk { get { return Instance._talk; } }

    //Core
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static MapManager Map { get { return Instance._map; } }
    public static DataManager Data { get { return Instance._data; } }

    private void Awake()
    {
        Init();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Object");
            if (go == null)
            {
                go = new GameObject { name = "@Object" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._pool.Init();
            s_instance._sound.Init();
        }
    }


    public static void Clear()
    {
        Sound.Clear();
        Map.Clear();

        Pool.Clear();
    }
}