using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MapData", menuName ="Game/MapData", order = 0), Serializable]
public class MapData : ScriptableObject
{
    public TextAsset groundMap;
    public TextAsset objMap;

    public List<GameObject> enemy; //임시, 나중에 EnemyList을 만들던가 아니면 싹 다 랜덤으로 나오게 하던가 할 것
    public int enemyCount; //임시
}
