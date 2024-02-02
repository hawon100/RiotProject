using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginalEnemy : Enemy_Base
{
    public bool isoriginalEnemy = true;

    public Animator originalEnemyAnim;

    protected override void Start()
    {
        base.Start();
        originalEnemyAnim = gameObject.GetOrAddComponent<Animator>();
        //transform.LookAt(Vector3.back);
        isoriginalEnemy = true;
    }

    protected override void DieDestroy()
    {
        MoveManager.Instance.DestroyEnemy(curPos);
        base.DieDestroy();
    }
}
