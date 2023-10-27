using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : A_Unit
{
    [SerializeField] private float timeToMove = 0.2f;

    [SerializeField] private int count;
    [SerializeField] private int maxcount;

    public override void Move()
    {
        switch (count)
        {
            case 0: CheckMove(Vector3.forward); count++; break;
            case 1: CheckMove(Vector3.back);  count = 0; break;
        }
    }

    private void CheckMove(Vector3 movePos)
    {
        Vector2Int plusPos = Vector2Int.zero;

        if (movePos == Vector3.forward) plusPos = Vector2Int.up;
        if (movePos == Vector3.back) plusPos = Vector2Int.down;

        int action = MoveManager.Instance.MoveCheck(curPos, plusPos);
        switch (action)
        {
            case 0: curPos = curPos + plusPos; StartCoroutine(MovePlayer(movePos)); break;
            case 1: break;
            case 2: Attack(); break;
        }
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
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
        yield return null;
    }
}
