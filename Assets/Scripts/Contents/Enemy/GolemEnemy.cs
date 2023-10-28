using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemEnemy : A_Unit
{
    [SerializeField] private int count;
    [SerializeField] private int maxcount = 5;

    public override void Move()
    {
        count++;
        if (count == maxcount)
        {
            oneMove();
            count = 0;
        }
    }
}
