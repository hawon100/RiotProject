using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectorPlayerAnim : MonoBehaviour
{
    [SerializeField] private Animator fadeAnim;

    public void OnTitle()
    {
        fadeAnim.SetTrigger("OnFade");

        StartCoroutine(NextScene());
    }

    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds(2f);

        Managers.Map.LoadScene(Define.Scene.Lobby);

        yield return null;
    }
}
