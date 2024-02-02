using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnOff_Base : MoveGround_Base
{
    [SerializeField] protected bool isOff;
    protected Material material;

    protected virtual void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    public override void Use()
    {
        isOff = !isOff;
    }
}
