using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

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
    [SerializeField] private List<Enemy_Base> curMoveMob;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _instance = this;
        a_Check = GetComponent<A_Check>();
        a_Map = GetComponent<A_Map>();
    }

    public void EnemyMove(){
        for(int i = 0; i < curMoveMob.Count; i++){
            a_Check.GetMoveMap(curMoveMap);
            curMoveMob[i].Move();
        }
    }

    /// <summary>
    /// return 0 == move, 1 == can't move, 2 == attack
    /// </summary>
    /// <param name="curPos"></param>
    /// <param name="plusPos"></param>
    /// <returns></returns>
    public int MoveCheck(Vector2Int curPos, Vector2Int plusPos)
    {
        Vector2Int movePos = curPos + plusPos;

        if (curGroundMap[movePos.x, movePos.y] != 1) return 1;
        if (curObjMap[movePos.x, movePos.y] != 0)
        {
            //체크 후 공격, 이동불가 리턴
            //return 1 or 2;
        }
        if (curMoveMap[movePos.x, movePos.y] != 0){
            //공격
            return 2;
        }
        
        curMoveMap[movePos.x, movePos.y] = curMoveMap[curPos.x, curPos.y];
        curMoveMap[curPos.x, curPos.y] = 0;

        return 0;
    }

    public void DestroyEnemy(Vector2Int curPos){
        curMoveMap[curPos.x, curPos.y] = 0;

        for (int i = 0; i < curMoveMob.Count; i++)
                if (curMoveMob[i] == null)
                    curMoveMob.Remove(curMoveMob[i]);
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
                    Player.Instance.transform.position = new Vector3(i, 0, j);
                    Player.Instance.curPos = new Vector2Int(i, j);
                }

    }

    public void MonsterInit(Mob_Base[,] spawnMap, List<Mob_Base> spawnMob)
    {
        for(int i = 0; i < spawnMob.Count; i++){
            curMoveMob.Add(spawnMob[i].GetComponent<Enemy_Base>());
        }

        for (int i = 0; i < spawnMap.GetLength(0); i++)
            for (int j = 0; j < spawnMap.GetLength(1); j++)
            {
                if (spawnMap[i, j] != null) curMoveMap[i, j] = 2;
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
