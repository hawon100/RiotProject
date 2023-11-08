using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public int[,] mapSize;
    Vector3 nextPos;

    private void Update()
    {
        nextPos = Player.Instance.transform.position + new Vector3(4.25f, 12, -6);

        transform.position = new(Mathf.Clamp(nextPos.x, 4, mapSize.GetLength(0) - 4),
        nextPos.y, Mathf.Clamp(nextPos.y, 4, mapSize.GetLength(1) - 4));
    }
}
