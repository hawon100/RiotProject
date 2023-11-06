using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obj_Base : MonoBehaviour
{
    public int index;

    public Vector2Int curPos;
    public bool isCanMove;

    public abstract void UseObj();
}
