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
            if (PlayerController.Instance.isUnderAttack == true)
            {
                enemy.OnHit(PlayerController.Instance._damage);
                PlayerController.Instance.isUnderAttack = false;
            }
        }
        if (other.CompareTag("Enemy") && this.CompareTag("RangeUp")) PlayerController.Instance._isObstacleUp = true;
        if (other.CompareTag("Enemy") && this.CompareTag("RangeDown")) PlayerController.Instance._isObstacleDown = true;
        if (other.CompareTag("Enemy") && this.CompareTag("RangeRight")) PlayerController.Instance._isObstacleRight = true;
        if (other.CompareTag("Enemy") && this.CompareTag("RangeLeft")) PlayerController.Instance._isObstacleLeft = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (PlayerController.Instance.isUnderAttack == true)
            {
                enemy.OnHit(PlayerController.Instance._damage);
                PlayerController.Instance.isUnderAttack = false;
            }
        }
        if (other.CompareTag("Enemy") && this.CompareTag("RangeUp")) PlayerController.Instance._isObstacleUp = true;
        if (other.CompareTag("Enemy") && this.CompareTag("RangeDown")) PlayerController.Instance._isObstacleDown = true;
        if (other.CompareTag("Enemy") && this.CompareTag("RangeRight")) PlayerController.Instance._isObstacleRight = true;
        if (other.CompareTag("Enemy") && this.CompareTag("RangeLeft")) PlayerController.Instance._isObstacleLeft = true;
    }
}
