using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerController
{
    static public PlayerMovement Instance { get; private set; }

    public bool isMoving;
    public bool isObstacleUp;
    public bool isObstacleDown;
    public bool isObstacleRight;
    public bool isObstacleLeft;
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

            if (isObstacleUp)
            {
                anim.SetTrigger("doAttack");
            }
            
            if (!isMoving && !isObstacleUp)
            {
                StartCoroutine(MovePlayer(Vector3.forward));
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            RookPlayer(Vector3.back);

            if (isObstacleDown)
            {
                anim.SetTrigger("doAttack");
            }
                
            if (!isMoving && !isObstacleDown)
            {
                StartCoroutine(MovePlayer(Vector3.back));
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RookPlayer(Vector3.right);

            if (isObstacleRight)
            {
                anim.SetTrigger("doAttack");
            }
            
            if (!isMoving && !isObstacleRight)
            {
                StartCoroutine(MovePlayer(Vector3.right));
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RookPlayer(Vector3.left);

            if (isObstacleLeft)
            {
                anim.SetTrigger("doAttack");
            }

            if (!isMoving && !isObstacleLeft)
            {
                StartCoroutine(MovePlayer(Vector3.left));
            }
        }
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
        targetPos = origPos + direction * 5f;

        while(elapsedTime < timeToMove)
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
