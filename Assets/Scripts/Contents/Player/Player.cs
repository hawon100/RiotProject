using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mob_Base
{
    private static Player _instance = null;
    public static Player Instance => _instance;

    [SerializeField] private AudioClip moveSound;

    [SerializeField] float timeToMove = 0.2f;
    [SerializeField] bool isMoving = false;
    [SerializeField] GameObject dieWin;

    TimingManager timingManager;

    public void Init(){
        _instance = this;
    }

    protected override void Start()
    {
        base.Start();
        dieWin.SetActive(false);
        timingManager = FindObjectOfType<TimingManager>();
        isMoving = false;

        HP = RoundData.Instance.HP;
    }

    public void Move(string type)
    {
        switch(type)
        {
            case "Up": CheckMove(Vector3.forward); break;
            case "Down": CheckMove(Vector3.back); break;
            case "Right": CheckMove(Vector3.right); break;
            case "Left": CheckMove(Vector3.left); break;
        }
    }

    private void CheckMove(Vector3 movePos)
    {
        if (!timingManager.CheckTiming()) { Debug.Log("Miss"); return; } //멈추는 이펙트 추가하면 좋을 듯

        Vector2Int plusPos = Vector2Int.zero;

        if (movePos == Vector3.forward) plusPos = Vector2Int.up;
        if (movePos == Vector3.back) plusPos = Vector2Int.down;
        if (movePos == Vector3.right) plusPos = Vector2Int.right;
        if (movePos == Vector3.left) plusPos = Vector2Int.left;

        RookPlayer(movePos);

        int action = MoveManager.Instance.MoveCheck(curPos, plusPos);

        switch (action)
        {
            case 0: curPos = curPos + plusPos; StartCoroutine(MovePlayer(movePos)); break;
            case 1: break;
            case 2: Attack(); break;
        }
    }

    void Attack()
    {
        anim.SetTrigger("doAttack");
    }

    void RookPlayer(Vector3 pos)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(pos), 1f);
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;
        Managers.Sound.Play(moveSound, Define.Sound.Effect);

        float elapsedTime = 0;
        Vector3 origPos = transform.position;
        Vector3 targetPos = origPos + direction;// * 5f

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
        dieWin.SetActive(true);
        Debug.Log("Die");
    }
}