using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnemy : A_Unit
{
    private int count;
    [SerializeField] private int maxcount = 4;

    protected override void DieDestroy()
    {
        Debug.Log("3");
    }

    public override void Move()
    {
        count++;
        if (count == maxcount)
        {
            oneMove();
        }
    }
}
