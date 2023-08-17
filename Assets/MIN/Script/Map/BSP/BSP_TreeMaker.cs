using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSP_TreeMaker : MonoBehaviour
{
    [SerializeField] GameObject cube;
    [SerializeField] GameObject cube2;
    [SerializeField] private Vector3Int mapSize;
    [SerializeField] private int maxNodeCount;

    [SerializeField] private float minBoxSize;
    [SerializeField] private float maxBoxSize;

    [SerializeField] private List<BSP_Node> mapData = new();
    private List<Vector3Int> roomPos = new();

    private void Start()
    {
        BSP_Node startNode = new BSP_Node(0, 0, mapSize.x, mapSize.z);
        MakeTree(startNode, 0);
        MakeMap(startNode, 0);

        Vector3Int startPos = roomPos[Random.Range(0, roomPos.Count)];
        roomPos.Remove(startPos);
        MakeRoad(startPos);
    }

    private void MakeTree(BSP_Node treeNode, int count)
    {
        if (count < maxNodeCount)
        {
            RectInt size = treeNode.treeSize;

            int length = size.width >= size.height ? size.width : size.height;
            int split = Mathf.RoundToInt(Random.Range(length * minBoxSize, length * maxBoxSize));
            if (size.width >= size.height)
            {
                treeNode.leftTree = new BSP_Node(size.x, size.y, split, size.height);
                treeNode.rightTree = new BSP_Node(size.x + split, size.y, size.width - split, size.height);
            }
            else
            {
                treeNode.leftTree = new BSP_Node(size.x, size.y, size.width, split);
                treeNode.rightTree = new BSP_Node(size.x, size.y + split, size.width, size.height - split);
            }

            treeNode.leftTree.parentTree = treeNode;
            treeNode.rightTree.parentTree = treeNode;

            MakeTree(treeNode.leftTree, count + 1);
            MakeTree(treeNode.rightTree, count + 1);
        }
    }

    private void MakeMap(BSP_Node treeNode, int count)
    {
        if (count == maxNodeCount)
        {
            RectInt curtree = treeNode.treeSize;
            Vector3Int mapPos = new(curtree.x + curtree.width / 2, 0, curtree.y + curtree.height / 2);
            roomPos.Add(mapPos);
            CreateMap(mapPos);
            return;
        }
        MakeMap(treeNode.leftTree, count + 1);
        MakeMap(treeNode.rightTree, count + 1);
    }

    void MakeRoad(Vector3Int from){
        if(roomPos.Count == 0) return;

        Vector3Int to = FindCloseRoom(from, roomPos);
        roomPos.Remove(to);
        CreateRoad(from, to);

        MakeRoad(to);
    }

    private Vector3Int FindCloseRoom(Vector3Int curRoom, List<Vector3Int> RoomList){
        Vector3Int closeRoom = Vector3Int.zero;
        float distance = float.MaxValue;

        foreach(var pos in RoomList){
            float curdistance = Vector3.Distance(pos, curRoom);
            if(curdistance < distance){
                distance = curdistance;
                closeRoom = pos;
            }
        }
        return closeRoom;
    }

    private void CreateRoad(Vector3Int from, Vector3Int to)
    {
        Vector3Int start = Vector3Int.zero;
        Vector3Int end = Vector3Int.zero;

        start.x = from.x <= to.x ? from.x : to.x;
        end.x = from.x <= to.x ? to.x : from.x;
        start.z = from.z <= to.z ? from.z : to.z;
        end.z = from.z <= to.z ? to.z : from.z;

        for (int x = start.x - 1; x <= end.x + 1; x++)
        {
            for (int z = from.z - 1; z <= from.z + 1; z++)
            {
                Instantiate(cube2, new Vector3Int(x, 0, z), Quaternion.identity);
            }
        }

        for (int z = start.z - 1; z <= end.z + 1; z++)
        {
            for (int x = to.x - 1; x <= to.x + 1; x++)
            {
                Instantiate(cube2, new Vector3Int(x, 0, z), Quaternion.identity);
            }
        }
    }

    private void CreateMap(Vector3Int pos){
        for(int i = -5; i <= 5; i++){
            for(int j = -5; j <= 5; j++){
                Instantiate(cube, pos + new Vector3Int(i, 0, j), Quaternion.identity);
            }
        }
    }
}
