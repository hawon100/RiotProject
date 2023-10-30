using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public Transform[] tfNteAppear = new Transform[2];

    [SerializeField] int bpm = 0;
    private int noteCount;
    double currentTime = 0d;

    TimingManager timingManager;
    private void Start()
    {
        timingManager = Util.GetOrAddComponent<TimingManager>(gameObject);
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= 60d / bpm)
        {
            GameObject t_note_0 = Managers.Resource.Instantiate("Note/Note_0", tfNteAppear[0]);
            GameObject t_note_1 = Managers.Resource.Instantiate("Note/Note_1", tfNteAppear[1]);
            t_note_0.transform.SetParent(this.transform);
            t_note_1.transform.SetParent(this.transform);
            timingManager.boxNoteList_0.Add(t_note_0);
            timingManager.boxNoteList_1.Add(t_note_1);
            currentTime -= 60d / bpm;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            timingManager.boxNoteList_0.Remove(collision.gameObject);
            timingManager.boxNoteList_1.Remove(collision.gameObject);
            Managers.Resource.Destroy(collision.gameObject);
            EffectManager.Instance.JudgementEffect();
            //miss

            noteCount++;

            if (noteCount == 2)
            {
                MoveManager.Instance.EnemyMove();
                noteCount = 0;
            }
        }
    }
}
