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
    [SerializeField] public Transform _target;
    private BoxCollider _box;
    private Vector3 _targetPos;

    void Start()
    {
        _box = GetComponent<BoxCollider>();
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Move());
    }

    void Update()
    {
        if (_info._hp <= 0)
        {
            HandleDeath();
        }

        UpdateHPBar();
        MoveJudge();
        HandleAnimation();
    }

    void HandleDeath()
    {
        isMove = false;
        isHit = false;
        Player.Instance.isUnderAttack = false;
        _anim.SetBool("isDead", true);
        _box.enabled = false;

        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("death") && _anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            StartCoroutine(Death());
        }
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(2f);
        Managers.Resource.Destroy(gameObject);
    }

    void UpdateHPBar()
    {
        _hpbar.value = _info.HP;
    }

    void MoveJudge()
    {
        _targetPos = new Vector3(_target.position.x, transform.position.y, _target.position.z);
        transform.LookAt(_targetPos);

        if (!_agent.pathPending)
        {
            _agent.SetDestination(_target.position);
            isMove = true;
        }

        if (_agent.remainingDistance <= 5f)
        {
            isMove = false;
        }
    }

    void HandleAnimation()
    {
        _anim.SetBool("isHit", false);

        if (isHit)
        {
            _anim.SetBool("isHit", true);
            _anim.SetBool("isMove", false);
            _anim.SetBool("isAttack", false);

            if (_anim.GetCurrentAnimatorStateInfo(0).IsName("gethit") && _anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                isHit = false;
            }
        }
        else
        {
            if (isMove)
            {
                _anim.SetBool("isMove", true);
                _anim.SetBool("isAttack", false);
            }
            else
            {
                _anim.SetBool("isMove", false);
                _anim.SetBool("isAttack", true);
            }
        }
    }

    private IEnumerator Move()
    {
        while (true)
        {
            if (isMove)
            {
                if (_agent.isStopped) _agent.isStopped = false;
            }
            else
            {
                if (!_agent.isStopped) _agent.isStopped = true;
            }

            yield return null;
        }
    }

    public void OnDamage(int dmg)
    {
        _info._hp -= dmg;
        isMove = false;
        isHit = true;
    }
}
