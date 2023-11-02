using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectorPlayerAnim : MonoBehaviour
{
    [SerializeField] private GameObject fade;
    [SerializeField] private GameObject panel;
    [SerializeField] private Animator fadeAnim;
    [SerializeField] private Transform directorCamPos;
    [SerializeField] private Camera cam;

    public void OnTitle()
    {
        fadeAnim.SetTrigger("OnFade");

        StartCoroutine(NextScene());
    }

    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds(2f);

        cam.gameObject.transform.position = directorCamPos.position;
        cam.gameObject.transform.rotation = directorCamPos.rotation;
        panel.SetActive(true);
        fade.SetActive(false);

        yield return null;
    }
}
