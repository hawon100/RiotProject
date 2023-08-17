using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isMoving;
    public Vector3 origPos, targetPos;
    public float timeToMove = 0.2f;

    private void Update()
    {
        Move();
    }
    
    private void Move()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(!isMoving) StartCoroutine(MovePlayer(Vector3.forward));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!isMoving) StartCoroutine(MovePlayer(Vector3.back));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!isMoving) StartCoroutine(MovePlayer(Vector3.right));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!isMoving) StartCoroutine(MovePlayer(Vector3.left));
        }
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0;
        origPos = transform.position;
        targetPos = origPos + direction * 5f;

        while(elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        isMoving = false;
    }
}
