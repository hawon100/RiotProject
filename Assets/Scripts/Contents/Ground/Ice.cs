using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MoveGround_Base
{
    public override void Use()
    {
        Player.Instance.cantMove = true;
    }
}
