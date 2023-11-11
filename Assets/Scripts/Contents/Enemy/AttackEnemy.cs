using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : MoveEnemy_Base
{
    protected override void Start()
    {
        base.Start();
        transform.LookAt(new Vector3(attackPos.x, 0, attackPos.y));
    }

    protected override void Behavior()
    {
        int check = MoveManager.Instance.MoveCheck(curPos, attackPos, false, true);

        anim.SetTrigger("OnAttack");

        switch (check)
        {
            case 0: break;
            case 1: break;
            case 2: Player.Instance.Damage(); break;
        }
    }

    protected override void DieDestroy()
    {
        MoveManager.Instance.DestroyEnemy(curPos, this);
        base.DieDestroy();
    }
}
