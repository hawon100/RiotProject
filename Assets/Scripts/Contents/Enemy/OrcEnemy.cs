using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrcEnemy : A_Unit
{
    [SerializeField]private int count;
    [SerializeField] private int maxcount = 5;

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
