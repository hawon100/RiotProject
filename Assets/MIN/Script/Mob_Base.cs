using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mob_Base : MonoBehaviour
{
    [SerializeField] int HP;
    [SerializeField] int maxHP;
    public int damage;

    public Action dieAction;
    protected Animator anim;
    bool isDie;

    public Vector2Int curPos;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        HP = maxHP;
        dieAction += DieDestroy;
    }

    public virtual void Damage(int Damage)
    {
        HP -= Damage;

        if (HP <= 0)
        {
            if (isDie) return;
            isDie = true;

            dieAction?.Invoke();
        }
    }

    protected abstract void DieDestroy();
}
