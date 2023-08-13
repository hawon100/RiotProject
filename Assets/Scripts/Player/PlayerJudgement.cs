using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJudgement : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && this.CompareTag("RangeUp")) PlayerMovement.Instance.isObstacleUp = true;
        if (other.CompareTag("Enemy") && this.CompareTag("RangeDown")) PlayerMovement.Instance.isObstacleDown = true;
        if (other.CompareTag("Enemy") && this.CompareTag("RangeRight")) PlayerMovement.Instance.isObstacleRight = true;
        if (other.CompareTag("Enemy") && this.CompareTag("RangeLeft")) PlayerMovement.Instance.isObstacleLeft = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && this.CompareTag("RangeUp")) PlayerMovement.Instance.isObstacleUp = false;
        if (other.CompareTag("Enemy") && this.CompareTag("RangeDown")) PlayerMovement.Instance.isObstacleDown = false;
        if (other.CompareTag("Enemy") && this.CompareTag("RangeRight")) PlayerMovement.Instance.isObstacleRight = false;
        if (other.CompareTag("Enemy") && this.CompareTag("RangeLeft")) PlayerMovement.Instance.isObstacleLeft = false;
    }
}
