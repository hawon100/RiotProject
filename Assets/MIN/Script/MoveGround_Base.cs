using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveGround_Base : MonoBehaviour
{
    public Vector2Int curPos;
    public bool isMove;
    public int index;

    public abstract void NextTiming();
}
