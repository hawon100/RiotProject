using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Obj_Base
{
    public override void UseObj()
    {
        Player.Instance.Damage(1);
    }
}
