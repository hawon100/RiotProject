using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleShellEnemy : A_Unit
{
    protected override void DieDestroy()
    {
        Debug.Log("3");
    }

    public override void Move()
    {
        for(int i = 0; i < 5; i++)
        {
            oneMove();
        }
    }
}
