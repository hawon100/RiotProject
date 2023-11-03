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
        var stageIndex = RoundData.Instance.stageIndex;
        var mapIndex = RoundData.Instance.mapIndex;


    }
}
