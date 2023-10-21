using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    MapManager _map = new MapManager();
    SoundManager _sound = new SoundManager();

    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static MapManager Map { get { return Instance._map; } }
    public static SoundManager Sound { get { return Instance._sound; } }

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