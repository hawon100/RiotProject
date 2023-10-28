using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilmageEnemy : A_Unit
{
    [SerializeField] private int count;
    [SerializeField] private int maxcount = 3;

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
