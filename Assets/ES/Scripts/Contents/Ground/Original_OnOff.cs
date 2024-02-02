using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Original_OnOff : OnOff_Base
{
    protected override void Start()
    {
        base.Start();
        
        if (isOff)
        {
            material.color = new Color(0.35f, 0.35f, 0.35f);
            MoveManager.Instance.InOutIndex(curPos, Define.MapType.Ground);
        }
    }

    public override void Use()
    {
        if (isOff)
        {
            material.color = new Color(1, 1, 1);
            MoveManager.Instance.InOutIndex(curPos, Define.MapType.Ground, index);
        }
        else
        {
            material.color = new Color(0.35f, 0.35f, 0.35f);
            MoveManager.Instance.InOutIndex(curPos, Define.MapType.Ground);
        }
        base.Use();
    }
}
