using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Game/Data", order = int.MinValue), Serializable]
public class MapData : ScriptableObject
{
    public AudioClip BGM;
}
