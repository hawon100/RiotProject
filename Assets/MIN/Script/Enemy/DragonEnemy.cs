using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonEnemy : A_Unit
{
    [SerializeField] private int count = 0;
    [SerializeField] private int maxcount = 2;

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