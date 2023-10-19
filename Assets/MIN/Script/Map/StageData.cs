using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Game/Data", order = 1), Serializable]
public class StageData : ScriptableObject
{
    public List<MapData> battleMapData = new();
    public List<MapData> specialMapData = new();
    public List<GameObject> mapTile = new();
    public List<GameObject> mapObj = new();
    public AudioClip bgm;

    public int stageCount;
}