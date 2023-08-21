using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[System.Serializable]
public class Information
{
    public Define.EnemyType _type;
    public float _hp;
    public int _maxHp;
    public float HP => _hp / _maxHp;
    public int _damage;
}

public class Enemy : MonoBehaviour
{
    public Information _info;
    protected Animator _anim;
    protected NavMeshAgent _agent;
    [SerializeField] protected bool isMove = true;
    [SerializeField] protected bool isHit = false;
    [SerializeField] protected Slider _hpbar;

    public void OnHit(int dmg)
    {
        _info._hp -= dmg;

        isMove = false;
        isHit = true;

        if (_info._hp <= 0)
        {
            isMove = false;
            isHit = false;

            PlayerMovement.Instance.isUnderAttack = false;
            _anim.SetBool("isDead", true);
        }
    }
}
