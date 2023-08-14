using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Animator anim;
    public NavMeshAgent agent;
    public Transform target;
    public Vector3 targetPos;
    public bool isMove;

    void Start()
    {
        isMove = true;
        StartCoroutine(Move());
    }

    void Update()
    {
        LookAt();
        MoveAnim();
        MoveJudge();
    }

    void MoveJudge()
    {
        if (!agent.pathPending)
        {
            agent.SetDestination(target.position);
            isMove = true;
        }

        if(agent.remainingDistance <= 5f)
        {
            isMove = false;
        }
    }

    void MoveAnim()
    {
        if (isMove)
        {
            anim.SetBool("isMove", true);
            anim.SetBool("isAttack", false);
        }
        if (!isMove)
        {
            anim.SetBool("isMove", false);
            anim.SetBool("isAttack", true);
        }
    }
    
    void LookAt()
    {
        targetPos = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.LookAt(targetPos);
    }

    IEnumerator Move()
    {
        while (true)
        {
            if (isMove)
            {
                if (agent.isStopped) agent.isStopped = false;

                yield return null;
            }
            if (!isMove)
            {
                if (!agent.isStopped) agent.isStopped = true;

                yield return null;
            }
        }
    }
}
