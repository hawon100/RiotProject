using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBoss : Enemy_Base
{
    [SerializeField] private int skillCount;
    [SerializeField] private int curPlayerCheckCount;
    [SerializeField] private int maxPlayerCheckCount;

    public override void Movement()
    {
        CheckPlayer();
    }

    private void CheckPlayer()
    {
        Vector2Int plusPos = new();
        Vector2Int attackPlusPos = new(-1, -1);
        Debug.Log("In");
        for (int i = 0; i < 4; i++)
        {
            switch (i)
            {
                case 0: plusPos = Vector2Int.up; break;
                case 1: plusPos = Vector2Int.down; break;
                case 2: plusPos = Vector2Int.right; break;
                case 3: plusPos = Vector2Int.left; break;
            }

            int action = MoveManager.Instance.AllCheck(curPos, plusPos, damage, false, false, true);
            Debug.Log(action);
            switch (action)
            {
                case 0: curPlayerCheckCount++; break;
                case 1: curPlayerCheckCount++; break;
                case 2: skillCount++;
                        Attack(curPos + (attackPlusPos + plusPos), new(3, 2));
                break;
            }

            if(curPlayerCheckCount >= maxPlayerCheckCount){
                Debug.Log("1");
                curPlayerCheckCount = 0;
            }
        }
        Debug.Log("Out");
    }

    private void Attack(Vector2Int leftDonwPos, Vector2Int boxSize)
    {
        for(int i = 0; i < boxSize.x; i++){
            for(int j = 0; j < boxSize.y; j++){
                int x = leftDonwPos.x + i;
                int y = leftDonwPos.y + j;

                if(new Vector2Int(x, y) == curPos) continue;
                MoveManager.Instance.AttackCheck(new(x, y), damage);
            }
        }
        //Player.Instance.Damage(1);
        AttackAnim();
        skillCount = 0;
    }
}
