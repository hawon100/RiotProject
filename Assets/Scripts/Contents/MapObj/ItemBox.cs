using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : Obj_Base
{
    [SerializeField] Item_Base myItem;
    bool isItem;

    private void Start() {
        int ranItem = Random.Range(0, GameManager.Instance.itemList.Count);
        
        myItem = GameManager.Instance.itemList[ranItem];
        GameManager.Instance.itemList.Remove(GameManager.Instance.itemList[ranItem]);
    }

    public override void UseObj(){
        if(isItem) return;

        switch(myItem.thisItem){
            case (int)Define.ItemType.Stats : myItem.Use(); Player.Instance.statsItem.Add(myItem); break;
            case (int)Define.ItemType.Attack : Player.Instance.attackItem.Add(myItem); break;
            case (int)Define.ItemType.Move : Player.Instance.moveItem.Add(myItem); break;
        }
        isItem = true;
    }
}
