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
            if (Player.Instance.isUnderAttack == true)
            {
                enemy.OnHit(Player.Instance._damage);
                Player.Instance.isUnderAttack = false;
            }
        }
        if (other.CompareTag("Enemy") && this.CompareTag("RangeUp")) Player.Instance._isObstacleUp = true;
        if (other.CompareTag("Enemy") && this.CompareTag("RangeDown")) Player.Instance._isObstacleDown = true;
        if (other.CompareTag("Enemy") && this.CompareTag("RangeRight")) Player.Instance._isObstacleRight = true;
        if (other.CompareTag("Enemy") && this.CompareTag("RangeLeft")) Player.Instance._isObstacleLeft = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (Player.Instance.isUnderAttack == true)
            {
                enemy.OnHit(Player.Instance._damage);
                Player.Instance.isUnderAttack = false;
            }
        }
        if (other.CompareTag("Enemy") && this.CompareTag("RangeUp")) Player.Instance._isObstacleUp = true;
        if (other.CompareTag("Enemy") && this.CompareTag("RangeDown")) Player.Instance._isObstacleDown = true;
        if (other.CompareTag("Enemy") && this.CompareTag("RangeRight")) Player.Instance._isObstacleRight = true;
        if (other.CompareTag("Enemy") && this.CompareTag("RangeLeft")) Player.Instance._isObstacleLeft = true;
    }
}
