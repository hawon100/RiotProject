using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffSwitch : Obj_Base
{
    private void Start() {
        transform.position += new Vector3(0, -0.9f);
    }

    public override void UseObj()
    {
        MoveManager.Instance.OnOff();
    }
}
