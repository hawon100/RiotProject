using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrcEnemy : A_Unit
{
    protected override void DieDestroy()
    {
        Debug.Log("3");
    }

    public override void Move()
    {
        for (int i = 0; i < 2; i++)
        {
            oneMove();
        }
    }
}
