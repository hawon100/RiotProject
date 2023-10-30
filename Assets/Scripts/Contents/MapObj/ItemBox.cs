using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : Obj_Base
{
    Item_Base myItem;

    protected override void UseObj(){
        switch(myItem.thisItem){
            case (int)Define.ItemType.Stats : Player.Instance.statsItem.Add(myItem); break;
            case (int)Define.ItemType.Attack : Player.Instance.attackItem.Add(myItem); break;
            case (int)Define.ItemType.Move : Player.Instance.moveItem.Add(myItem); break;
        }
    }
}
