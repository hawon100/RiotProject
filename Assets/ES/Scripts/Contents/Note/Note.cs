using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    Image noteImage;

    private void Start()
    {
        noteImage = Util.GetOrAddComponent<Image>(gameObject);
    }

    private void Update()
    {
        switch (type)
        {
            case NoteType.Left: MoveLeft(); break;
            case NoteType.Right: MoveRight(); break;
        }
    }

    public void HideNote()
    {
        noteImage.enabled = false;
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
