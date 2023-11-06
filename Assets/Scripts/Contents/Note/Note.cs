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

    [SerializeField] private NoteType type = NoteType.None;
    [SerializeField] private float noteSpeed = 400f;
    private Image noteImage;
    public bool isGhost;

    private void Start()
    {
        noteImage = gameObject.GetOrAddComponent<Image>();
    }

    private void Update()
    {
        switch (type)
        {
            case NoteType.Left: transform.localPosition += Vector3.left * noteSpeed * Time.deltaTime; break;
            case NoteType.Right: transform.localPosition += Vector3.right * noteSpeed * Time.deltaTime; break;
        }
    }

    public void HideNote()
    {
        noteImage.enabled = false;
    }
     
    public bool GetNoteFlag()
    {
        return noteImage.enabled;
    }
}
