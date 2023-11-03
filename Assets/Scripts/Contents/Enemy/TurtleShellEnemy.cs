using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleShellEnemy : A_Unit
{
    [SerializeField] private int count;
    [SerializeField] private int maxcount = 3;

    public override void Movement()
    {
        count++;
        if (count == maxcount)
        {
            oneMove();
            count = 0;
        }
    }
}
