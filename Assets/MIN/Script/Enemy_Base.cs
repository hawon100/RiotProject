using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class Enemy_Base : Mob_Base
{
    [Header("Enemy_Base")]
    [SerializeField] private Slider _hpbar;
    //[SerializeField] protected GameObject die_effect;

    protected Transform target;
    protected Vector2 direction;

    private BoxCollider _box;

    [Header("if 0 == noCheck")]
    public int checkBoxSize;

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

    protected virtual void Attack()
    {
        
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
