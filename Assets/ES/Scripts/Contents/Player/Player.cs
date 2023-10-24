using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player _instance = null;
    public static Player Instance => _instance;

    public AudioClip moveSound;

    [SerializeField] private Animator anim;
    public Vector2Int curPos;
    public int _damage;

    public bool isMoving = false;

    public Vector3 origPos, targetPos;
    public float timeToMove = 0.2f;

    private void Start()
    {
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
        if (movePos == Vector3.back) plusPos = Vector2Int.down;
        if (movePos == Vector3.right) plusPos = Vector2Int.right;
        if (movePos == Vector3.left) plusPos = Vector2Int.left;
        curPos = curPos + plusPos; StartCoroutine(MovePlayer(movePos));
        int action = MoveManager.Instance.MoveCheck(curPos, plusPos);
        Debug.Log($"{action} {curPos} {plusPos}");
        switch (action)
        {
            case 0: curPos = curPos + plusPos; StartCoroutine(MovePlayer(movePos)); break;
            case 1: break;
            case 2: break;
        }
    }

    void Attack()
    {
        TimingManager.Instance.CheckTiming();
        anim.SetTrigger("doAttack");
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;
        Managers.Sound.Play(moveSound, Define.Sound.Effect);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 1f);

        TimingManager.Instance.CheckTiming();

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
}
