using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Enemy
{
    private Vector3 _targetPos;

    Rigidbody _rigid;
    BoxCollider _box;

    void Start()
    {
        _box = GetComponent<BoxCollider>();
        _rigid = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Move());
    }

    void Update()
    {
        _hpbar.value = _info.HP;
        
        if(gameObject.activeSelf) LookAt();

        MoveJudge();
        TypeMove();

        if (_info.HP <= 0)
        {
            _box.enabled = false;

            if (_anim.GetCurrentAnimatorStateInfo(0).IsName("death") && _anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                StartCoroutine(Death());
            }
        }
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(2f);

        Managers.Resource.Destroy(gameObject);
    }

    void TypeMove()
    {
        switch (_info._type)
        {
            case Define.EnemyType.Bat: BatAnim(); break;
            case Define.EnemyType.Skeleton: break;
            case Define.EnemyType.Slime: break;
        }
    }

    void MoveJudge()
    {
        if (!_agent.pathPending)
        {
            _agent.SetDestination(_target.position);
            isMove = true;
        }

        if(_agent.remainingDistance <= 5f)
        {
            isMove = false;
        }
    }

    void BatAnim()
    {
         _anim.SetBool("isHit", false);

        if(isHit == true)
        {
            _anim.SetBool("isHit", true);
            _anim.SetBool("isMove", false);
            _anim.SetBool("isAttack", false);

            if (_anim.GetCurrentAnimatorStateInfo(0).IsName("gethit") && _anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                isHit = false;
            }
        }
        if (isMove && !isHit)
        {
            _anim.SetBool("isMove", true);
            _anim.SetBool("isAttack", false);
        }
        if (!isMove && !isHit)
        {
            _anim.SetBool("isMove", false);
            _anim.SetBool("isAttack", true);
        }

    }

    void LookAt()
    {
        _targetPos = new Vector3(_target.position.x, transform.position.y, _target.position.z);
        transform.LookAt(_targetPos);
    }

    IEnumerator Move()
    {
        while (true)
        {
            if (isMove)
            {
                if (_agent.isStopped) _agent.isStopped = false;

                yield return null;
            }
            if (!isMove)
            {
                if (!_agent.isStopped) _agent.isStopped = true;

                yield return null;
            }
        }
    }
}
