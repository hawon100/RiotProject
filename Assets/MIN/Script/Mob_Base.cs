using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mob_Base : Base
{
    [SerializeField] protected int HP;
    [SerializeField] protected int maxHP;
    public int damage;

    public Action dieAction;
    protected Animator anim;
    bool isDie;

    public override void Use()
    {
        Damage(1);
    }

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        HP = maxHP;
        dieAction += DieDestroy;
    }

    private void Damage(int Damage)
    {
        HP -= Damage;
        anim.SetTrigger("OnHit");

        if (HP <= 0)
        {
            if (isDie) return;
            isDie = true;

            dieAction?.Invoke();
        }
    }

    protected abstract void DieDestroy();
}
