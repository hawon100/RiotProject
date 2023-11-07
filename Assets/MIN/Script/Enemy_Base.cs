using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy_Base : Mob_Base
{
    [Header("Enemy_Base")]
    [SerializeField] private Slider _hpbar;

    [SerializeField] private bool cantDie;

    protected override void Start()
    {
        base.Start();
    }

    public void BackStep(Vector2Int plusPos)
    {
        transform.LookAt(Player.Instance.transform);

        Vector3 movePos = new();

        if (plusPos == Vector2Int.up) movePos = Vector3.forward;
        if (plusPos == Vector2Int.down) movePos = Vector3.back;
        if (plusPos == Vector2Int.right) movePos = Vector3.right;
        if (plusPos == Vector2Int.left) movePos = Vector3.left;

        int action = MoveManager.Instance.MoveCheck(curPos, plusPos, false);

        switch (action)
        {
            case 0: curPos = curPos + plusPos; StartCoroutine(MoveEnemy(movePos, 0.2f)); break;
            case 1: if(!cantDie) dieAction?.Invoke(); break;
            case 2: break;
        }
    }

    protected override void DieDestroy()
    {
        MoveManager.Instance.InOutObj(curPos, true);
        Managers.Resource.Destroy(gameObject);
    }

    private IEnumerator MoveEnemy(Vector3 direction, float sec)
    {
        float elapsedTime = 0;
        Vector3 origPos = transform.position;
        Vector3 targetPos = origPos + direction;

        while (elapsedTime < sec)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / sec));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
    }
}
