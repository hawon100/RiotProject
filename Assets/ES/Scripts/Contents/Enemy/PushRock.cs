using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushRock : Enemy_Base
{
    public override void BackStep(Vector2Int plusPos)
    {
        transform.LookAt(Player.Instance.transform);

        Vector3 movePos = new();
        Vector3 vector3PlusPos = new();

        if (plusPos == Vector2Int.up) movePos = Vector3.forward;
        if (plusPos == Vector2Int.down) movePos = Vector3.back;
        if (plusPos == Vector2Int.right) movePos = Vector3.right;
        if (plusPos == Vector2Int.left) movePos = Vector3.left;

        transform.LookAt(-movePos);

        Hit(plusPos);

        int action = 0;
        while (action == 0)
        {
            action = MoveManager.Instance.MoveCheck(curPos, plusPos, false);

            switch (action)
            {
                case 0: curPos = curPos + plusPos; movePos += vector3PlusPos; break;
                case 1: break;
                case 2: break;
            }
        }
        StartCoroutine(MoveEnemy(movePos, 0.2f));
    }

    protected override void DieDestroy()
    {
        MoveManager.Instance.DestroyEnemy(curPos);
        base.DieDestroy();
    }
}
