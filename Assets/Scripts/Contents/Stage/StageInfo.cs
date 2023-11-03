using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageInfo : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform[] path;
    [SerializeField] private TextMeshProUGUI stageText;

    private void Update()
    {
        var stageIndex = RoundData.Instance.stageIndex + 1;
        var mapIndex = RoundData.Instance.mapIndex + 1;

        stageText.text = stageIndex.ToString() + " - " + mapIndex.ToString();

        if (stageIndex == 1)
        {
            switch (mapIndex)
            {
                case 1: player.position = path[1].position; break;
                case 2: player.position = path[2].position; break;
                case 3: player.position = path[3].position; break;
                case 4: player.position = path[4].position; break;
            }
        }
        if (stageIndex == 2)
        {
            switch (mapIndex)
            {
                case 1: player.position = path[5].position; break;
                case 2: player.position = path[6].position; break;
                case 3: player.position = path[7].position; break;
                case 4: player.position = path[8].position; break;
            }
        }
    }
}
