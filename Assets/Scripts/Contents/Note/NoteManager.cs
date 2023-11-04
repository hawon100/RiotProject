using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public RectTransform tfNteAppear = null;
    public RectTransform ghostTfNteAppear = null;
    [SerializeField] int bpm = 0;
    private int noteCount;
    double currentTime = 0d;

    TimingManager timingManager;

    private void Start()
    {
        timingManager = gameObject.GetOrAddComponent<TimingManager>();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= 60d / bpm)
        {
            var t_note = Managers.Resource.Instantiate("Note/Note_0", tfNteAppear);
            var ghost_note = Managers.Resource.Instantiate("Note/Note_1", ghostTfNteAppear);
            t_note.transform.SetParent(this.transform);
            ghost_note.transform.SetParent(this.transform);
            timingManager.boxNoteList.Add(t_note);
            timingManager.ghostNoteList.Add(ghost_note);

            currentTime -= 60d / bpm;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            if (collision.GetComponent<Note>().GetNoteFlag())
            {
                EffectManager.Instance.JudgementEffect();
                noteCount++;
                if (noteCount == 2)
                {
                    noteCount = 0;
                }
            }

            timingManager.boxNoteList.Remove(collision.gameObject);
            timingManager.ghostNoteList.Remove(collision.gameObject);

            //if (timingManager.ghostNoteList.Count > 0)
            //{
            //    if (timingManager.ghostNoteList[0] != null)
            //    {
            //        Managers.Resource.Destroy(timingManager.ghostNoteList[0]);
            //    }
            //    timingManager.ghostNoteList.RemoveAt(0);
            //}

            Managers.Resource.Destroy(collision.gameObject);

        }
    }
}
