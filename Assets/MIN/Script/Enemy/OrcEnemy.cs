using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrcEnemy : A_Unit
{
    private int count;
    [SerializeField] private int maxcount = 5;

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
