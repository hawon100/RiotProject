using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList = new();
    public List<GameObject> ghostNoteList = new();
    [SerializeField] private Transform Center = null;
    [SerializeField] private RectTransform[] timingRect = null;
    [SerializeField] private Vector2[] timingBox = null;

    private void Start()
    {
        timingBox = new Vector2[timingRect.Length];

        for(int i = 0; i < timingBox.Length; i++)
        {
            timingBox[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2, Center.localPosition.x + timingRect[i].rect.width / 2);
        }
    }

    public bool CheckTiming()
    {
        for(int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x;

            for(int x = 0; x < timingRect.Length; x++)
            {
                if (timingBox[x].x <= t_notePosX && t_notePosX <= timingBox[x].y)
                {
                    boxNoteList[i].GetOrAddComponent<Note>().HideNote();
                    ghostNoteList[i].GetOrAddComponent<Note>().HideNote();
                    boxNoteList.RemoveAt(i);
                    ghostNoteList.RemoveAt(i);

                    return true;
                }
            }
        }

        EffectManager.Instance.JudgementEffect();
        return false;
    }
}
