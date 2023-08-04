using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    public Vector3 targetPos;
    public int num = 0;
    bool isMove;

    private void Start()
    {
        isMove = false;

        StartCoroutine(_Move());
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        targetPos = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.LookAt(targetPos);
        //if (!isMove) agent.SetDestination(target.position);
        //else if (isMove) agent.SetDestination(transform.position);

    }

    private IEnumerator _Move()
    {
        while (true)
        {
            agent.SetDestination(target.position);

            yield return new WaitForSeconds(1f);

            agent.SetDestination(transform.position);

            yield return new WaitForSeconds(1f);

            num++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMove = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMove = false;
        }
    }
}
