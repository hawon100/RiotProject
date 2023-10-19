using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveManager : MonoBehaviour
{
    private static MoveManager _instance = null;
    public static MoveManager Instance => _instance;

    Queue<A_Requset> requsetQueue = new Queue<A_Requset>();
    A_Requset curRequset;
    A_Check a_Check;
    A_Map a_Map;

    bool isProcessing;

    private int[,] curGroundMap;
    private int[,] curObjMap;
    private int[,] curMoveMap;

    private void Start() {
        Init();
    }

    public void Init()
    {
        _instance = this;
        a_Check = GetComponent<A_Check>();
        a_Map = GetComponent<A_Map>();
        a_Map._Instance();
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.T))
        // {
        //     for (int i = 0; i < curMoveMap.GetLength(0); i++)
        //     {
        //         Debug.Log("-------------------------");
        //         for (int j = 0; j < curMoveMap.GetLength(1); j++)
        //             Debug.Log(curMoveMap[i, j]);

        //         Debug.Log("-------------------------");
        //     }
        // }
    }

    public void MapInit(int[,] _curObjMap, int[,] _curGroundMap)
    {
        curObjMap = _curObjMap;
        curGroundMap = _curGroundMap;
        curMoveMap = new int[curObjMap.GetLength(0), curObjMap.GetLength(1)];

        a_Map.InitMap(_curObjMap, _curGroundMap);

        for (int i = 0; i < curObjMap.GetLength(0); i++) for (int j = 0; j < curObjMap.GetLength(1); j++)
                if (curObjMap[i, j] == 1)
                {
                    curMoveMap[i, j] = 1;
                    Player.Instance.transform.position = new Vector3(j, 0, curMoveMap.GetLength(0) - i - 1);
                }

    }

    public void MonsterInit(int[,] spawnMap)
    {
        for (int i = 0; i < spawnMap.GetLength(0); i++)
            for (int j = 0; j < spawnMap.GetLength(1); j++)
            {
                if (spawnMap[i, j] == 1) curMoveMap[i, j] = 2;
            }

        a_Map.InitMoveMap(curMoveMap);
    }

    public static void Requset(Vector3 _start, Vector3 _end, UnityAction<List<A_Node>, bool> _callbeck)
    {
        A_Requset newRequset = new A_Requset(_start, _end, _callbeck);
        Instance.requsetQueue.Enqueue(newRequset);
        Instance.TryProcessingNext();
    }

    void TryProcessingNext()
    {
        if (!isProcessing && requsetQueue.Count > 0)
        {
            curRequset = requsetQueue.Dequeue();
            isProcessing = true;
            a_Check.find(curRequset.start, curRequset.end);
        }
    }

    public void Finished(List<A_Node> path, bool success)
    {
        curRequset.callback(path, success);
        isProcessing = false;
        TryProcessingNext();
    }
}

struct A_Requset
{
    public Vector3 start;
    public Vector3 end;
    public UnityAction<List<A_Node>, bool> callback;

    public A_Requset(Vector3 _start, Vector3 _end, UnityAction<List<A_Node>, bool> _callbeck)
    {
        start = _start;
        end = _end;
        callback = _callbeck;
    }
}
