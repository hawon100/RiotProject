using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform target;

    bool isAcive = false;

    private void Start()
    {
        agent.speed = 2f;
        isAcive = false;
    }

    private void Update()
    {
        if(!isAcive)
        {
            StartCoroutine(Move());
        }
        if(isAcive)
        {
            StopCoroutine(Move());
        }    
    }

    private IEnumerator Move()
    {
        agent.SetDestination(target.position);

        yield return new WaitForSeconds(1f);

        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            agent.speed = 0f;
            isAcive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            agent.speed = 2f;
            isAcive = false;
        }
    }
}
