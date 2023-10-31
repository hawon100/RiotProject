using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item_Base : MonoBehaviour
{
    public abstract int thisItem { get; }

    public abstract void Use();
}
