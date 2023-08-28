using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    enum NoteType
    {
        None,
        Left,
        Right,
    }

    [SerializeField] NoteType type = NoteType.None;
    [SerializeField] float noteSpeed = 400f;

    private void Update()
    {
        switch (type)
        {
            case NoteType.Left: MoveLeft(); break;
            case NoteType.Right: MoveRight(); break;
        }
    }

    void MoveLeft()
    {
        transform.localPosition += Vector3.left * noteSpeed * Time.deltaTime;
    }

    void MoveRight()
    {
        transform.localPosition += Vector3.right * noteSpeed * Time.deltaTime;
    }
}
