using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList_0 = new List<GameObject>();
    public List<GameObject> boxNoteList_1 = new List<GameObject>();
    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform[] timingRect = null;
    Vector2[] timingBox = null;

    private void Start()
    {
        timingBox = new Vector2[timingRect.Length];

        for(int i = 0; i < timingBox.Length; i++)
        {
            timingBox[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2, Center.localPosition.x + timingRect[i].rect.width / 2);
        }
    }

    public void CheckTiming()
    {
        for(int i = 0; i < boxNoteList_0.Count; i++)
        {
            float t_notePosX = boxNoteList_0[i].transform.localPosition.x;

            for(int x = 0; x < timingRect.Length; x++)
            {
                if (timingBox[x].x <= t_notePosX && t_notePosX <= timingBox[x].y)
                {
                    boxNoteList_0[i].GetOrAddComponent<Note>().HideNote();
                    boxNoteList_0.RemoveAt(i);
                    boxNoteList_1[i].GetOrAddComponent<Note>().HideNote();
                    boxNoteList_1.RemoveAt(i);
                    Debug.Log("Hit" + x);
                    return;
                    
                }
            }
        }
        Debug.Log("Miss");
    }
}
