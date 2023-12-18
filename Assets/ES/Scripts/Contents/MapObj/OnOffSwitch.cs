using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffSwitch : Obj_Base
{
    public override void UseObj()
    {
        MoveManager.Instance.OnOff();
    }
}
