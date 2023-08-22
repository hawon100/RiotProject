using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    static public PlayerMovement Instance { get; private set; }

    [SerializeField] private Animator anim;
    public int _damage;

    public bool isMoving = true;
    public bool isUnderAttack = false;

    public bool _isObstacleUp;
    public bool _isObstacleDown;
    public bool _isObstacleRight;
    public bool _isObstacleLeft;
    public Vector3 origPos, targetPos;
    public float timeToMove = 0.2f;

    private void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            RookPlayer(Vector3.forward);

            if (_isObstacleUp)
            {
                Attack();
            }
            else if (!isMoving && !_isObstacleUp)
            {
                StartCoroutine(MovePlayer(Vector3.forward));
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            RookPlayer(Vector3.back);

            if (_isObstacleDown)
            {
                Attack();
            }
            else if (!isMoving && !_isObstacleDown)
            {
                StartCoroutine(MovePlayer(Vector3.back));
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RookPlayer(Vector3.right);

            if (_isObstacleRight)
            {
                Attack();
            }
            else if (!isMoving && !_isObstacleRight)
            {
                StartCoroutine(MovePlayer(Vector3.right));
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RookPlayer(Vector3.left);

            if (_isObstacleLeft)
            {
                Attack();
            }
            else if (!isMoving && !_isObstacleLeft)
            {
                StartCoroutine(MovePlayer(Vector3.left));
            }
        }
    }

    void Attack()
    {
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
