using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; set; }
    [SerializeField] private Animator missAnim = null;
    [SerializeField] private Animator perfectAnim = null;
    private string hit = "Hit";

    private void Start()
    {
        Instance = this;
    }

    public void MissEffect()
    {
        missAnim.SetTrigger(hit);
    }

    public void PerfectEffect()
    {
        perfectAnim.SetTrigger(hit);
    }
}
