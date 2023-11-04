using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mob_Base : MonoBehaviour
{
    [SerializeField] protected int HP;
    [SerializeField] protected int maxHP;

    protected Animator anim;
    public Action dieAction;
    public Vector2Int curPos;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        HP = maxHP;
        dieAction += DieDestroy;
    }

    protected abstract void DieDestroy();
}
