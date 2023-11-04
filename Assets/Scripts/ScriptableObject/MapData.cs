using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MapData", menuName ="Game/MapData", order = 0), Serializable]
public class MapData : ScriptableObject
{
    public TextAsset groundMap;
    public TextAsset objMap;
    public TextAsset enemyMap;
}
