using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    [SerializeField] private GameObject clearWin;

    public RectTransform tfNteAppear = null;
    public RectTransform ghostTfNteAppear = null;
    public int bpm;
    private int noteCount;
    double currentTime = 0d;

    TimingManager timingManager;

    private void Start()
    {
        timingManager = gameObject.GetOrAddComponent<TimingManager>();
    }

    private void Update()
    {
        if (!Player.Instance.isDead)
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
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            if (!Player.Instance.isDead)
            {
                if (collision.GetComponent<Note>().GetNoteFlag())
                {
                    EffectManager.Instance.MissEffect();
                    if (!timingManager.isLobby) if (!collision.GetComponent<Note>().isGhost) Player.Instance.Damage();
                    noteCount++;
                    if (noteCount == 2)
                    {
                        noteCount = 0;
                    }
                }
            }

            if (!collision.GetComponent<Note>().isGhost) MoveManager.Instance.nextTiming();
            timingManager.boxNoteList.Remove(collision.gameObject);
            timingManager.ghostNoteList.Remove(collision.gameObject);
            Managers.Resource.Destroy(collision.gameObject);
        }
    }
}
