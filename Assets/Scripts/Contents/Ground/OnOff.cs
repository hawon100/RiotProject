using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MoveGround_Base
{
    [SerializeField] private bool isOff;
    Material material;

    private void Start()
    {
        material = GetComponent<Renderer>().material;

        if (isOff)
        {
            material.color = new Color(0.35f, 0.35f, 0.35f);
            //MoveManager.Instance.InOutIndex(curPos, Define.MapType.Ground);
        }
    }

    public override void NextTiming()
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
        isOff = !isOff;
    }
}
