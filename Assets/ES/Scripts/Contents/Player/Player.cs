using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Player : Mob_Base
{
    private static Player _instance = null;
    public static Player Instance => _instance;

    public AudioClip moveSound;

    public bool isMoving = false;
    public bool isUnderAttack = false;

    public bool _isObstacleUp;
    public bool _isObstacleDown;
    public bool _isObstacleRight;
    public bool _isObstacleLeft;
    public Vector3 origPos, targetPos;
    public float timeToMove = 0.2f;

    TimingManager timingManager;

    protected override void Start()
    {
        base.Start();

        timingManager = FindObjectOfType<TimingManager>();
        _instance = this;
        isMoving = false;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) CheckMove(Vector3.forward);
        if (Input.GetKeyDown(KeyCode.DownArrow)) CheckMove(Vector3.back);
        if (Input.GetKeyDown(KeyCode.RightArrow)) CheckMove(Vector3.right);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) CheckMove(Vector3.left);
    }

    private void CheckMove(Vector3 movePos)
    {
        Vector2Int plusPos = Vector2Int.zero;

        if (movePos == Vector3.forward) plusPos = Vector2Int.up;
        else if (movePos == Vector3.back) plusPos = Vector2Int.down;
        else if (movePos == Vector3.right) plusPos = Vector2Int.right;
        else if (movePos == Vector3.left) plusPos = Vector2Int.left;

        RookPlayer(movePos);

        int action = MoveManager.Instance.MoveCheck(curPos, plusPos);
        Debug.Log($"{action}");
        switch (action)
        {
            case 0: curPos = curPos + plusPos; StartCoroutine(MovePlayer(movePos)); break;
            case 1: break;
            case 2: /*attack*/ break;
        }
    }

    void Attack()
    {
        timingManager.CheckTiming();
        anim.SetTrigger("doAttack");

        isUnderAttack = true;

        _isObstacleUp = false;
        _isObstacleDown = false;
        _isObstacleRight = false;
        _isObstacleLeft = false;
    }

    void RookPlayer(Vector3 pos)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(pos), 1f);
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;
        Managers.Sound.Play(moveSound, Define.Sound.Effect);

        timingManager.CheckTiming();

        float elapsedTime = 0;
        origPos = transform.position;
        targetPos = origPos + direction;// * 5f

        while (elapsedTime < timeToMove)
        {
            anim.SetBool("isDash", true);
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        anim.SetBool("isDash", false);

        isMoving = false;
    }

    protected override void DieDestroy()
    {
        Debug.Log("Die");
    }
}
