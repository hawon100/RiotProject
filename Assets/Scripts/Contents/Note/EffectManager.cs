using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; set; }
    [SerializeField] private Animator judgementAnimator = null;
    private string hit = "Hit";

    private void Start()
    {
        Instance = this;
    }

    public void JudgementEffect()
    {
        judgementAnimator.SetTrigger(hit);
    }
}
