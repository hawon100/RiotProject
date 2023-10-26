using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMapRotation : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private void Update()
    {
        transform.position += new Vector3(0, 0, -moveSpeed * Time.deltaTime);

        if (transform.position.z < -25)
        {
            transform.position = ConveyorBelt.Instance.startPos.position;
        }
    }
}
