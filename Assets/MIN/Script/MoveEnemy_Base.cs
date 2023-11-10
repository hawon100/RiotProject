using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveEnemy_Base : Enemy_Base
{
    public Vector2Int attackPos;
    public bool dirMove;
    [SerializeField] private int curtiming;
    [SerializeField] private int endtiming;

    public void nextTiming()
    {
        if (curtiming < endtiming) { curtiming++; return; }

        curtiming = 0;
        Behavior();
    }

    protected abstract void Behavior();
}
