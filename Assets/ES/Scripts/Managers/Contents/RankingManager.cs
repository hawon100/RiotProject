using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    private int[] bestScore = new int[5];

    public void ScoreSet(int currentScore) //랭킹
    {
        Managers.Data.LoadData<RankData>("RankData").Rank[0].bestScore = currentScore;
        
        var tmpScore = 0;

        for(int i = 0; i < 5; i++)
        {
            bestScore[i] = Managers.Data.LoadData<RankData>("RankData").Rank[i].bestScore;
            
            while(bestScore[i] < currentScore)
            {
                tmpScore = bestScore[i];
                bestScore[i] = currentScore;

                Managers.Data.LoadData<RankData>("RankData").Rank[i].bestScore = bestScore[i];

                currentScore = tmpScore;
            }
        }

        for(int i = 0; i < 5; i++)
        {
            Managers.Data.LoadData<RankData>("RankData").Rank[i + 1].bestScore = bestScore[i + 1];
        }
    }
}
