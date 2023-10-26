using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : A_Unit
{
    protected override void DieDestroy()
    {
        Debug.Log("3");
    }

    public override void Move()
    {
        oneMove();
    }
}
