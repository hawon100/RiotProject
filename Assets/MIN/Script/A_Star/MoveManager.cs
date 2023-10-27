using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using TMPro;

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
    private Mob_Base[,] curEnemyMap;
    [SerializeField] private List<Enemy_Base> curMoveMob;
    [SerializeField] private List<Enemy_Base> curCheckMob;

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

    public void EnemyMove()
    {
        if (curMoveMob.Count != 0)
            for (int i = 0; i < curMoveMob.Count; i++)
            {
                a_Check.GetMoveMap(curMoveMap);
                curMoveMob[i].Move();
            }
        Classification();
    }

    /// <summary>
    /// return 0 == move, 1 == can't move, 2 == attack
    /// </summary>
    /// <param name="curPos"></param>
    /// <param name="plusPos"></param>
    /// <returns></returns>
    public int MoveCheck(Vector2Int curPos, Vector2Int plusPos, bool isFly = false)
    {
        Vector2Int movePos = curPos + plusPos;

        try { if (curGroundMap[movePos.x, movePos.y] == 0) { } /*map out check*/}
        catch { return 1; }

        if (curGroundMap[movePos.x, movePos.y] != 1 && !isFly) return 1;
        if (curObjMap[movePos.x, movePos.y] != 0)
        {
            //체크 후 공격, 이동불가 리턴
            //return 1 or 2;
        }
        if (curMoveMap[movePos.x, movePos.y] != 0)
        {
            curEnemyMap[movePos.x, movePos.y].Damage(1);
            return 2;
        }

        curEnemyMap[movePos.x, movePos.y] = curEnemyMap[curPos.x, curPos.y];
        curEnemyMap[curPos.x, curPos.y] = null;

        curMoveMap[movePos.x, movePos.y] = curMoveMap[curPos.x, curPos.y];
        curMoveMap[curPos.x, curPos.y] = 0;

        return 0;
    }

    public void DestroyEnemy(Vector2Int curPos)
    {
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
    }

    public void MonsterInit(Mob_Base[,] spawnMap, List<Mob_Base> spawnMob)
    {
        curEnemyMap = spawnMap;
        Enemy_Base temp;
        for (int i = 0; i < spawnMob.Count; i++)
        {
            temp = spawnMob[i].GetComponent<Enemy_Base>();
            if (temp.checkBoxSize == 0) curMoveMob.Add(temp);
            else curCheckMob.Add(temp);
        }


        for (int i = 0; i < spawnMap.GetLength(0); i++)
            for (int j = 0; j < spawnMap.GetLength(1); j++)
            {
                if (spawnMap[i, j] != null) curMoveMap[i, j] = 2;
            }

        for (int i = 0; i < curObjMap.GetLength(0); i++) for (int j = 0; j < curObjMap.GetLength(1); j++)
                if (curObjMap[i, j] == 1)
                {
                    curMoveMap[i, j] = 1;
                    curEnemyMap[i, j] = Player.Instance.GetComponent<Mob_Base>();
                    Player.Instance.transform.position = new Vector3(i, 0, j);
                    Player.Instance.curPos = new Vector2Int(i, j);
                }

        a_Map.InitMoveMap(curMoveMap);
    }

    private void Classification()
    {
        if (curCheckMob.Count == 0) return;

        for (int i = 0; i < curCheckMob.Count; i++)
        {
            if (curCheckMob[i].checkBoxSize == 0)
            {
                curMoveMob.Add(curCheckMob[i]);
                curCheckMob.Remove(curCheckMob[i]);
            }
            else
            {
                if (CheckPlayer(curCheckMob[i].curPos, curCheckMob[i].checkBoxSize))
                {
                    curMoveMob.Add(curCheckMob[i]);
                    curCheckMob.Remove(curCheckMob[i]);
                }
            }
        }
    }

    private bool CheckPlayer(Vector2Int pos, int size)
    {
        for (int i = -size; i <= size; i++) for (int j = -size; j <= size; j++)
            {
                int x = pos.x + i;
                int y = pos.y + j;

                try { if (curMoveMap[x, y] == 1) return true; }
                catch { continue; }
            }
        return false;
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
