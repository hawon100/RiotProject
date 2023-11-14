using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoteInfo
{
    public GameObject note;
    public GameObject ghostNote;
    public int noteCount;
}

public class NoteManager : MonoBehaviour
{
    public NoteInfo noteInfo;
    public RectTransform tfNteAppear = null;
    public RectTransform ghostTfNteAppear = null;
    public int bpm;
    private int noteCount;
    double currentTime = 0d;

    private Vector3 spawnPos = new Vector3(0, 0, 0);

    TimingManager timingManager;

    List<GameObject> noteObj = new List<GameObject>();
    List<GameObject> ghostNoteObj = new List<GameObject>();

    private void Start()
    {
        timingManager = GetComponent<TimingManager>();

        for (int i = 0; i < noteInfo.noteCount; i++)
        {
            noteObj.Add(Managers.Resource.Instantiate(noteInfo.note));
            ghostNoteObj.Add(Managers.Resource.Instantiate(noteInfo.ghostNote));
        }

        foreach (var item in noteObj)
        {
            Managers.Resource.Destroy(item);
        }

        foreach (var ghostItem in ghostNoteObj)
        {
            Managers.Resource.Destroy(ghostItem);
        }
    }

    private void Update()
    {
        if (!MoveManager.Instance.clearWin.activeSelf)
        {
            if (!Player.Instance.isDead)
            {
                currentTime += Time.deltaTime;

                if (currentTime >= 60d / bpm)
                {
                    var t_note = Managers.Resource.Instantiate(noteInfo.note, tfNteAppear);
                    var ghost_note = Managers.Resource.Instantiate(noteInfo.ghostNote, ghostTfNteAppear);

                    t_note.GetComponent<UnityEngine.UI.Image>().enabled = true;
                    ghost_note.GetComponent<UnityEngine.UI.Image>().enabled = true;

                    t_note.GetComponent<RectTransform>().anchoredPosition = spawnPos;
                    ghost_note.GetComponent<RectTransform>().anchoredPosition = spawnPos;

                    t_note.transform.SetParent(this.transform);
                    ghost_note.transform.SetParent(this.transform);

                    timingManager.boxNoteList.Add(t_note);
                    timingManager.ghostNoteList.Add(ghost_note);

                    currentTime -= 60d / bpm;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            if (!MoveManager.Instance.clearWin.activeSelf)
            {
                if (!Player.Instance.isDead)
                {
                    if (collision.GetComponent<Note>().GetNoteFlag())
                    {
                        EffectManager.Instance.MissEffect();
                        DamageUI.Instance.Damage();
                        if (!timingManager.isLobby) if (!collision.GetComponent<Note>().isGhost) Player.Instance.Damage();
                        noteCount++;
                        if (noteCount == 2)
                        {
                            noteCount = 0;
                        }
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
