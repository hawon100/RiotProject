using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy_Base : MonoBehaviour
{
    [Header("Enemy_Base")]
    protected Transform target;
    //[SerializeField] protected GameObject die_effect;

    [SerializeField] float HP;
    [SerializeField] float maxHP;

    protected Vector2 direction;
    bool isDie;

    public Action dieAction;
    protected Animator anim;

    protected virtual void Start()
    {
        //anim = GetComponent<Animator>();
        HP = maxHP;
        dieAction += DieDestroy;
    }

    protected virtual void Update()
    {
        target = Player.Instance.transform;
        direction = target.position - transform.position;
    }

    public virtual void Enemy_Damage(float Damage)
    {
        HP -= Damage;

        if (HP <= 0)
        {
            if(isDie) return;
            isDie = true;

            dieAction?.Invoke();
        }
    }

    protected abstract void DieDestroy();
}
