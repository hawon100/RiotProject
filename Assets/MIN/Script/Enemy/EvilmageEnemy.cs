using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilmageEnemy : A_Unit
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
