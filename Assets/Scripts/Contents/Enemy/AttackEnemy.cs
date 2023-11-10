using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : MoveEnemy_Base
{
    protected override void Behavior()
    {
        int check = MoveManager.Instance.MoveCheck(curPos, attackPos, false, true);

        switch(check){
            case 0: break;
            case 1: break;
            case 2: Player.Instance.Damage(); break;
        }
    }
}
