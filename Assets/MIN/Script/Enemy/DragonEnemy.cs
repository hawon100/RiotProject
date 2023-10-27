using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonEnemy : A_Unit
{
    private int count = 0;
    [SerializeField] private int maxcount = 2;
    protected override void DieDestroy()
    {
        Debug.Log("3");
    }

    public override void Move()
    {
        count++;
        if(count == maxcount)
        {
            oneMove();
            count = 0;
        }
    }
}