using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJudgement : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (PlayerMovement.Instance.isUnderAttack == true)
            {
                enemy.OnHit(PlayerMovement.Instance._damage);
                PlayerMovement.Instance.isUnderAttack = false;
            }
        }
        if (other.CompareTag("Enemy") && this.CompareTag("RangeUp")) PlayerMovement.Instance._isObstacleUp = true;
        if (other.CompareTag("Enemy") && this.CompareTag("RangeDown")) PlayerMovement.Instance._isObstacleDown = true;
        if (other.CompareTag("Enemy") && this.CompareTag("RangeRight")) PlayerMovement.Instance._isObstacleRight = true;
        if (other.CompareTag("Enemy") && this.CompareTag("RangeLeft")) PlayerMovement.Instance._isObstacleLeft = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (PlayerMovement.Instance.isUnderAttack == true)
            {
                enemy.OnHit(PlayerMovement.Instance._damage);
                PlayerMovement.Instance.isUnderAttack = false;
            }
        }
        if (other.CompareTag("Enemy") && this.CompareTag("RangeUp")) PlayerMovement.Instance._isObstacleUp = true;
        if (other.CompareTag("Enemy") && this.CompareTag("RangeDown")) PlayerMovement.Instance._isObstacleDown = true;
        if (other.CompareTag("Enemy") && this.CompareTag("RangeRight")) PlayerMovement.Instance._isObstacleRight = true;
        if (other.CompareTag("Enemy") && this.CompareTag("RangeLeft")) PlayerMovement.Instance._isObstacleLeft = true;
    }
}
