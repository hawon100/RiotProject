using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public int[,] mapSize;
    [SerializeField] private int clamp;

    private void Update()
    {
        transform.position = new Vector3
        (Mathf.Clamp(Player.Instance.transform.position.x, clamp, mapSize.GetLength(0) - clamp),
        0, Mathf.Clamp(Player.Instance.transform.position.y, clamp, mapSize.GetLength(1) - clamp)) +
        new Vector3(0, 12, -10);//new Vector3(4.25f, 12, 6)
        transform.rotation = Quaternion.Euler(60, 0, 0);
    }
}
