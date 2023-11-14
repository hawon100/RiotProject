using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour
{
    public static DamageUI Instance { get; private set; }
    [SerializeField] private Image damage_ui;
    [SerializeField] private float startAlpha;
    [SerializeField] private float alphaSpeed;
    private bool isDamage;
    private float curAlpha;

    private void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        if (isDamage)
        {
            damage_ui.color = new Color(1, 0, 0, curAlpha);
            curAlpha-=Time.deltaTime;
            if(curAlpha <= 0)
                isDamage = false;
        }
    }

    public void Damage()
    {
        isDamage = true;
        curAlpha = startAlpha;
        //StartCoroutine(alpha(damage_ui, 1));
    }

    //IEnumerator alpha(Image image, int sec)
    //{
    //    isDamage = true;
    //    float timer = 0f;
    //    while (timer <= sec)
    //    {
    //        if(!isDamage) yield break;
    //        image.color = new Color(1, 1, 1, 1 - timer / sec);
    //        timer += Time.deltaTime;
    //        yield return null;
    //    }
    //    isDamage = false;
    //}
}
