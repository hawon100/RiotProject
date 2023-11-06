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

    [SerializeField] public GameObject clearWin;

    bool isProcessing;

    private int[,] curGroundMap;
    private int[,] curObjMap;
    private int[,] curMoveMap;
    private Enemy_Base[,] curMob;
    private Obj_Base[,] curObj;
    public List<Enemy_Base> curMoveMob;
    public List<Enemy_Base> curCheckMob;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _instance = this;
    }

    /// <summary>
    /// return 0 == move, 1 == can't move, 2 == attack
    /// </summary>
    /// <param name="curPos"></param>
    /// <param name="plusPos"></param>
    /// <returns></returns>
    public int MoveCheck(Vector2Int curPos, Vector2Int plusPos, bool isPlayer = false)
    {
        Vector2Int movePos = curPos + plusPos;

        try { if (curGroundMap[movePos.x, movePos.y] == 0) { } /*map out check*/}
        catch { return 1; }

        if (curGroundMap[movePos.x, movePos.y] != 1) return 1;
        if (curObjMap[movePos.x, movePos.y] != 0)
        {
            if (isPlayer)
            {
                curObj[movePos.x, movePos.y].UseObj();

                if (!curObj[movePos.x, movePos.y].isCanMove) return 1;
            }
            else return 1;
        }
        if (curMoveMap[movePos.x, movePos.y] != 0)
        {
            if(isPlayer){
                curMob[movePos.x, movePos.y].BackStep(plusPos);
                return 2;
            }
            else return 1;
        }

        curMob[movePos.x, movePos.y] = curMob[curPos.x, curPos.y];
        curMob[curPos.x, curPos.y] = null;

        curMoveMap[movePos.x, movePos.y] = curMoveMap[curPos.x, curPos.y];
        curMoveMap[curPos.x, curPos.y] = 0;

        return 0;
    }

    public void DestroyEnemy(Vector2Int curPos, Enemy_Base dieObj)
    {
        curMoveMap[curPos.x, curPos.y] = 0;
        curMoveMob.Remove(dieObj);
    }

    public void InOutObj(Vector2Int curPos, int index = 0)
    {
        if(curObjMap[curPos.x, curPos.y] == 0) curObjMap[curPos.x, curPos.y] = index;
        else curObjMap[curPos.x, curPos.y] = 0;
    }

    public void MapInit(int[,] _curGroundMap)
    {
        curGroundMap = _curGroundMap;
        curMoveMap = new int[_curGroundMap.GetLength(0), _curGroundMap.GetLength(1)];
    }

    public void MobInit(Enemy_Base[,] _map, Obj_Base[,] _obj, List<Enemy_Base> spawnMob, int[,] _curObjMap)
    {
        curObjMap = _curObjMap;
        curMob = _map;
        curObj = _obj;

        Enemy_Base temp;
        for (int i = 0; i < spawnMob.Count; i++)
        {
            temp = spawnMob[i].GetComponent<Enemy_Base>();
            if (temp.checkBoxSize == 0) curMoveMob.Add(temp);
            else curCheckMob.Add(temp);
        }

        for (int i = 0; i < _map.GetLength(0); i++)
            for (int j = 0; j < _map.GetLength(1); j++)
            {
                if (_map[i, j] != null) curMoveMap[i, j] = 2;
            }

        for (int i = 0; i < _curObjMap.GetLength(0); i++) for (int j = 0; j < _curObjMap.GetLength(1); j++)
                if (_curObjMap[i, j] == 1)
                {
                    curMoveMap[i, j] = 1;
                    Player.Instance.transform.position = new Vector3(i, 0, j);
                    Player.Instance.curPos = new Vector2Int(i, j);
                }

    }
}