using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MoveObj_Base
{
    public override void UseObj()
    {
        Player.Instance.Damage();
    }
}
