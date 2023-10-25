using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class A_Unit : Enemy_Base
{
    [Header("A_STAR")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected bool isMove;

    List<A_Node> path;

    bool isEnd = true;

    int targetIndex;

    protected override void Start() {
        base.Start();
    }

    protected void oneMove(){
        MoveManager.Requset(transform.position, target.position, Found);
    }

    protected void Found(List<A_Node> newPath, bool success)
    {
        isEnd = false;
        path = newPath;
        //StopAllCoroutines();
        StartCoroutine(Follow());
    }

    IEnumerator Follow()
    {
        A_Node curWayPoint = path[0];
        targetIndex = 0;

        if (transform.position == curWayPoint.pos)
        {
            Debug.Log($"{curWayPoint.pos}");
            targetIndex++;
            if (targetIndex >= path.Count - 1)
            {
                yield break;
            }
            curWayPoint = path[targetIndex];
        }
        Debug.Log("select");
        yield return StartCoroutine(MoveTo(curWayPoint.pos, 0.5f));
        isEnd = true;
        yield break;
    }

    private IEnumerator MoveTo(Vector3 target, float sec)
    {
        float timer = 0f;
        Vector3 start = transform.position;

        while (timer <= sec)
        {
            if(!isMove) yield break;

            transform.position = Vector3.Lerp(start, target, timer / sec);
            timer += Time.deltaTime * moveSpeed;

            yield return null;
        }

        yield break;
    }
}
