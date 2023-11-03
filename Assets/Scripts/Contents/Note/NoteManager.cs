using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public RectTransform tfNteAppear = null;
    [SerializeField] private string prefabPath;
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
            var t_note = Managers.Resource.Instantiate(prefabPath, tfNteAppear);
            t_note.transform.SetParent(this.transform);
            timingManager.boxNoteList.Add(t_note);

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
                    MoveManager.Instance.EnemyMove();
                    noteCount = 0;
                }
            }

            timingManager.boxNoteList.Remove(collision.gameObject);
            Managers.Resource.Destroy(collision.gameObject);
        }
    }
}
