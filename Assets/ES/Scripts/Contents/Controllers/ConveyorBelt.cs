using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public static ConveyorBelt Instance { get; set; }
    public Transform startPos;

    private void Awake()
    {
        Instance = this;
    }
}