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
    protected NavMeshAgent agent;
    [SerializeField] private Slider _hpbar;
    [SerializeField] public Transform _target;
    [SerializeField] private float curAttackDelay;
    [SerializeField] private float maxAttackDelay;
    private BoxCollider _box;
    private Vector3 _targetPos;

    void Start()
    {
        _box = GetComponent<BoxCollider>();
        _anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        HandleDeath();
        HandleMove();
    }

    void HandleMove()
    {
        _targetPos = new Vector3(_target.position.x, transform.position.y, _target.position.z);
        transform.LookAt(_targetPos);
        agent.SetDestination(Player.Instance.transform.position);

        if (agent.remainingDistance <= agent.stoppingDistance && !agent.isStopped)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    void Attack(bool isHit)
    {
        curAttackDelay += Time.deltaTime;
        if(curAttackDelay < maxAttackDelay) return;
        
        _anim.SetBool("isAttack", isHit);

        curAttackDelay = 0;
    }

    void HandleDeath()
    {
        _hpbar.value = _info.HP;

        if (_info._hp <= 0)
        {
            _anim.SetBool("isDead", true);
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

    public void OnDamage(int dmg)
    {
        _info._hp -= dmg;
    }
}
