using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [TextArea]
    public string sentence;
    public Sprite cg;
}

[CreateAssetMenu(fileName = "New TalkData", menuName = "Data/TalkData", order = int.MinValue)]
public class TalkData : BaseData
{
    public Dialogue[] dialogue;
}
