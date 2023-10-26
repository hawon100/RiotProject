using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Map : MonoBehaviour
{
    private static A_Map _instance = null;
    public static A_Map Instance => _instance;

    private int[,] objMap;
    private int[,] groundMap;
    private int[,] moveMap;
    A_Node[,] a_Node;

    Vector3Int center;
    int map_x;
    int map_y;

    public void InitMap(int[,] _objMap, int[,] _groundMap)
    {
        _instance = this;
        objMap = _objMap;
        groundMap = _groundMap;

        a_Node = new A_Node[groundMap.GetLength(0), groundMap.GetLength(1)];

        for (int i = 0; i < groundMap.GetLength(0); i++) for (int j = 0; j < groundMap.GetLength(1); j++){
            if(groundMap[i, j] == 0) a_Node[i, j] = new A_Node(true, new Vector3Int(i, 0, j));
            else a_Node[i, j] = new A_Node(false, new Vector3Int(i, 0, j));
        }
    }

    public void InitMoveMap(int[,] _moveMap)
    {
        moveMap = _moveMap;
    }

    public static A_Node GetNodeTransfrom(Vector3 nodePos)
    {
        return Instance.a_Node[(int)nodePos.x, (int)nodePos.z];
    }

    public static List<A_Node> find_Node(A_Node node)
    {
        List<A_Node> node_List = new List<A_Node>();
        for (int i = -1; i <= 1; i++) for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue;
                if(Mathf.Abs(i + j) != 1) continue;

                int x = node.pos.x + i;
                int z = node.pos.z + j;

                try{ node_List.Add(Instance.a_Node[x, z]); }
                catch { continue; }
            }
        return node_List;
    }
}
