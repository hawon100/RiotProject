using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectorPlayerAnim : MonoBehaviour
{
    [SerializeField] private Animator fadeAnim;
    [SerializeField] private AudioClip fadeSound;

    public void OnTitle()
    {
        fadeAnim.SetTrigger("OnFade");

        Managers.Sound.Play(fadeSound);

        StartCoroutine(NextScene());
    }

    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds(4f);

        Managers.Map.LoadScene(Define.Scene.Lobby);

        yield return null;
    }
}
