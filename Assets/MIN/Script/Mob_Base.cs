using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mob_Base : MonoBehaviour
{
    [SerializeField] protected int HP;
    [SerializeField] protected int maxHP;
    public int damage;

    protected Animator anim;
    public Action dieAction;
    public Vector2Int curPos;
    bool isDie;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        HP = maxHP;
        dieAction += DieDestroy;
    }

    public void Damage(int Damage)
    {
        HP -= Damage;
        anim.SetTrigger("OnHit");

        if (HP <= 0)
        {
            if (isDie) return;
            isDie = true;
            Debug.Log("11111111111111");
            dieAction?.Invoke();
        }
    }

    protected abstract void DieDestroy();
}
