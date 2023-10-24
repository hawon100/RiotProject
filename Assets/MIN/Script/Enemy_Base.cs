using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy_Base : Mob_Base
{
    [Header("Enemy_Base")]
    protected Transform target;
    //[SerializeField] protected GameObject die_effect;

    protected Vector2 direction;

    protected virtual void Update()
    {
        target = Player.Instance.transform;
        direction = target.position - transform.position;
    }
}
