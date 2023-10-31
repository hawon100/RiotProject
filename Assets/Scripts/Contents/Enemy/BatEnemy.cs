using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : A_Unit
{
    [SerializeField] float timeToMove = 0.2f;
    [SerializeField] bool isMoving = false;

    public override void Move()
    {
        int randValue = Random.Range(0, 4);
        switch(randValue)
        {
            case 0: CheckMove(Vector3.forward); break;
            case 1: CheckMove(Vector3.back); break;
            case 2: CheckMove(Vector3.right); break;
            case 3: CheckMove(Vector3.left); break;
        }
    }

    private void CheckMove(Vector3 movePos)
    {
        Vector2Int plusPos = Vector2Int.zero;

        if (movePos == Vector3.forward) plusPos = Vector2Int.up;
        if (movePos == Vector3.back) plusPos = Vector2Int.down;
        if (movePos == Vector3.right) plusPos = Vector2Int.right;
        if (movePos == Vector3.left) plusPos = Vector2Int.left;

        int action = MoveManager.Instance.MoveCheck(curPos, plusPos, damage, true);
        switch (action)
        {
            case 0: curPos = curPos + plusPos; StartCoroutine(MoveEnemy(movePos)); break;
            case 1: break;
            case 2: Attack(); break;
        }
    }

    private IEnumerator MoveEnemy(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0;
        Vector3 origPos = transform.position;
        Vector3 targetPos = origPos + direction;// * 5f

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, elapsedTime / timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        isMoving = false;
    }
}
