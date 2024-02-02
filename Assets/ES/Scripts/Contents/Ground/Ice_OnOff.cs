using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_OnOff : OnOff_Base
{
    [SerializeField] Material ice;
    [SerializeField] Material ground;
    MeshRenderer ren;

    protected override void Start()
    {
        ren = GetComponent<MeshRenderer>();

        if (isOff)
        {
            ren.material = ground;
            index = 1;
            MoveManager.Instance.InOutIndex(curPos, Define.MapType.Ground);
            MoveManager.Instance.InOutIndex(curPos, Define.MapType.Ground, index);
        }
        else
        {
            ren.material = ice;
            index = 2;
            MoveManager.Instance.InOutIndex(curPos, Define.MapType.Ground);
            MoveManager.Instance.InOutIndex(curPos, Define.MapType.Ground, index);
        }
    }

    public override void Use()
    {
        if (isOff)
        {
            ren.material = ice;
            index = 2;
            MoveManager.Instance.InOutIndex(curPos, Define.MapType.Ground);
            MoveManager.Instance.InOutIndex(curPos, Define.MapType.Ground, index);
        }
        else
        {
            ren.material = ground;
            index = 1;
            MoveManager.Instance.InOutIndex(curPos, Define.MapType.Ground);
            MoveManager.Instance.InOutIndex(curPos, Define.MapType.Ground, index);
        }
        base.Use();
    }
}
