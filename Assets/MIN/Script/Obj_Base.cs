using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obj_Base : Base
{
    public bool isLock;

    public override void Use()
    {
        UseObj();
    }

    protected abstract void UseObj();
}
