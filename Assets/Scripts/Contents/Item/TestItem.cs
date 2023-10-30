using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : Item_Base
{
    public override int thisItem => (int)Define.ItemType.Stats;

    public override void Use()
    {
        Player.Instance.damage += 1;
    }
}
