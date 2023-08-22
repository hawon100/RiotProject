using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Map : MonoBehaviour
{
    [SerializeField] private GameObject battleMap;
    [SerializeField] private Transform group;
    [SerializeField] private Vector2Int maxMapSize;
    [SerializeField] private int mapSize;
    [SerializeField] private int mapCount;
    [SerializeField] private int walkCount;
    private Vector2Int startPos;
    private Action createMap;
    private int curMapCount;

    private bool[,] map;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) { }
    }

    private void Init()
    {
        map = new bool[maxMapSize.x, maxMapSize.y];
        group = Instantiate(group, new(0, 0, 0), Quaternion.identity);


        startPos = new(Random.Range(0, maxMapSize.x), Random.Range(0, maxMapSize.y));
        Debug.Log(startPos);
        MakeTree(new Map_Node(startPos));

        createMap?.Invoke();
    }

    private void MakeTree(Map_Node curNode)
    {
        if (curMapCount >= mapCount) return;

        curNode = CreateMap(curNode);
        MakeTree(curNode);
    }

    private Map_Node CreateMap(Map_Node curNode)
    {
        if (curMapCount < mapCount - 1)
        {
            Vector2Int nextPos = CheckNear(curNode);
            curNode.childNode = new(nextPos)
            {
                parentNode = curNode
            };
            CheckDoor(curNode, curNode.childNode);
        }

        map[curNode.curPos.x, curNode.curPos.y] = true;
        Vector3Int curPos = new ((curNode.curPos.x - startPos.x) * mapSize, 0, (curNode.curPos.y - startPos.y) * mapSize);
        var temp = Instantiate(battleMap, curPos, Quaternion.identity, group).GetComponent<CreateMap>();
        for (int i = 0; i < 4; i++)
        {
            if (curNode.openDoor[i] && !temp.openDoor[i]) temp.openDoor[i] = true;
        }

        createMap += temp.Create;
        temp.mapSize = mapSize;
        temp.walkCount = walkCount;
        curMapCount++;

        return curNode.childNode;
    }

    Vector2Int CheckNear(Map_Node curNode)
    {
        List<Vector2Int> nodeList = new();

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i != 0 && j != 0) continue;
                if (i == 0 && j == 0) continue;

                int x = curNode.curPos.x + i;
                int y = curNode.curPos.y + j;

                try
                {
                    if (map[x, y]) continue;
                    else nodeList.Add(new(x, y));
                }
                catch
                {
                    continue;
                }
            }
        }

        if (nodeList.Count == 0) return CheckNear(curNode.parentNode);
        else return nodeList[Random.Range(0, nodeList.Count)];
    }

    private void CheckDoor(Map_Node curNode, Map_Node nextNode)
    {
        Vector2Int temp = new(curNode.curPos.x - nextNode.curPos.x, curNode.curPos.y - nextNode.curPos.y);

        if (temp.y > 0)
        {
            curNode.openDoor[1] = true;
            nextNode.openDoor[0] = true;
        }
        else if (temp.y < 0)
        {
            curNode.openDoor[0] = true;
            nextNode.openDoor[1] = true;
        }
        else if (temp.x < 0)
        {
            curNode.openDoor[3] = true;
            nextNode.openDoor[2] = true;
        }
        else if (temp.x > 0)
        {
            curNode.openDoor[2] = true;
            nextNode.openDoor[3] = true;
        }

    }
}
