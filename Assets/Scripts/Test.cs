using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    static Test s_instance;
    static public Test Instance { get { Init(); return s_instance; } }

    private void Start()
    {
        Init();
    }

    static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("ObjectName");
            if(go == null)
            {
                go = new GameObject { name = "ObjectName" };
                go.AddComponent<Test>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Test>();
        }
    }
}
