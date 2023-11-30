using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RankInfomation
{
    public int bestScore;
}

[CreateAssetMenu(fileName = "new RankData", menuName = "Data/RankData", order = int.MinValue)]
public class RankData : BaseData
{
    public RankInfomation[] Rank;
}
