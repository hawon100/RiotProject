using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Game/RoundData", order = 1), Serializable]
public class RoundData : ScriptableObject
{
    private static RoundData _instance = null;
    public static RoundData Instance => _instance;

    public int stageIndex;
    public int mapIndex;

    [Header("Player")]
    public int HP;

    public void Reset(){
        stageIndex = 0;
        mapIndex = 0;
        HP = 6;
    }

    public void InitData()
    {
        _instance = this;
    }
}
