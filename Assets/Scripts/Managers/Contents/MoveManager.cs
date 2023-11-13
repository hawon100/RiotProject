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

    [SerializeField] private CameraMove cam;
    public GameObject clearWin;


    private int[,] curGroundMap;
    private int[,] curObjMap;
    private int[,] curMoveMap;
    private Enemy_Base[,] curMob;
    private Obj_Base[,] curObj;
    public List<MoveEnemy_Base> moveEnemy;
    public List<MoveObj_Base> moveObj;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _instance = this;
    }

    public void nextTiming()
    {
        foreach (var obj in moveObj) obj.nextTiming();
        foreach (var enemy in moveEnemy) enemy.nextTiming();
    }

    /// <summary>
    /// return 0 == move, 1 == can't move, 2 == attack
    /// </summary>
    /// <param name="curPos"></param>
    /// <param name="plusPos"></param>
    /// <returns></returns>
    public int MoveCheck(Vector2Int curPos, Vector2Int plusPos, bool isPlayer = false, bool isNotMove = false)
    {
        Vector2Int movePos = curPos + plusPos;

        try { if (curGroundMap[movePos.x, movePos.y] == 0) { } /*map out check*/}
        catch { return 1; }

        if (curGroundMap[movePos.x, movePos.y] != 1) return 1;
        if (curMoveMap[movePos.x, movePos.y] != 0)
        {
            if (isPlayer)
            {
                if(curMob[movePos.x, movePos.y].TryGetComponent<MoveEnemy_Base>(out var e)){
                    if(e.dirMove)
                        if(e.cantMove) e.attackPos = curPos;
                        else e.attackPos = movePos;
                }
                curMob[movePos.x, movePos.y].BackStep(plusPos);
                return 2;
            }
            else
            {
                if (curMoveMap[movePos.x, movePos.y] == 1) return 2;
                else return 1;
            }
        }
        if (curObjMap[movePos.x, movePos.y] != 0)
        {
            if (isPlayer)
                curObj[movePos.x, movePos.y].UseObj();
            
            if (!curObj[movePos.x, movePos.y].isCanMove) return 1;
        }

        if (!isNotMove)
        {
            curMob[movePos.x, movePos.y] = curMob[curPos.x, curPos.y];
            curMob[curPos.x, curPos.y] = null;

            curMoveMap[movePos.x, movePos.y] = curMoveMap[curPos.x, curPos.y];
            curMoveMap[curPos.x, curPos.y] = 0;
        }

        return 0;
    }

    public void InOutObj(Vector2Int curPos, int index = 0)
    {
        if (curObjMap[curPos.x, curPos.y] == 0) curObjMap[curPos.x, curPos.y] = index;
        else curObjMap[curPos.x, curPos.y] = 0;
    }

    public void DestroyEnemy(Vector2Int curPos, MoveEnemy_Base enemy = null)
    {
        curMoveMap[curPos.x, curPos.y] = 0;
        if (enemy != null) moveEnemy.Remove(enemy);
    }

    public void MapInit(int[,] _curGroundMap)
    {
        curGroundMap = _curGroundMap;
        curMoveMap = new int[_curGroundMap.GetLength(0), _curGroundMap.GetLength(1)];
    }

    public void MobInit(Enemy_Base[,] _map, Obj_Base[,] _obj, List<MoveObj_Base> _moveObj, List<MoveEnemy_Base> _moveEnemy, int[,] _curObjMap)
    {
        curObjMap = _curObjMap;
        curMob = _map;
        curObj = _obj;

        moveObj = _moveObj.ToList();
        moveEnemy = _moveEnemy.ToList();
        cam.mapSize = new int[_curObjMap.GetLength(0), _curObjMap.GetLength(1)];

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