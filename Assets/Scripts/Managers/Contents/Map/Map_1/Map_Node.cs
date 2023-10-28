using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Node
{
    public Vector2Int curPos;
    public List<bool> openDoor = new();
    public Map_Node parentNode;
    public Map_Node childNode;

    public Map_Node(Vector2Int _curPos){
        curPos = _curPos;
        for(int i = 0; i < 4; i++){
            openDoor.Add(false);
        }
    }
}
