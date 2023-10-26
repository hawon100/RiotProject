using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class Enemy_Base : Mob_Base
{
    [Header("Enemy_Base")]
    protected Transform target;
    //[SerializeField] protected GameObject die_effect;

    protected Vector2 direction;

    [SerializeField] private Slider _hpbar;
    private BoxCollider _box;

    protected override void Start()
    {
        base.Start();

        _box = GetComponent<BoxCollider>();
    }

    protected virtual void Update()
    {
        target = Player.Instance.transform;
        direction = target.position - transform.position;
        transform.LookAt(target);
    }

    protected virtual void Attack(bool isHit)
    {
        anim.SetBool("isAttack", isHit);
    }

    protected override void DieDestroy()
    {
        anim.SetBool("isDead", true);
        _box.enabled = false;

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("death") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            StartCoroutine(Death());
        }
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(2f);
        Managers.Resource.Destroy(gameObject);
    }

    public abstract void Move();
}
