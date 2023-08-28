using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public Transform[] tfNteAppear = new Transform[2];

    [SerializeField] int bpm = 0;
    double currentTime = 0d;

    private void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= 60d / bpm)
        {
            GameObject t_note = Managers.Resource.Instantiate("Note/Note_1", tfNteAppear[0]);
            t_note.transform.SetParent(this.transform);
            currentTime = 60d / bpm;
        }
    }
}
