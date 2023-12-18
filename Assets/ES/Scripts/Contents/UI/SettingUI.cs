using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private GameObject quitWin;

    public void Button(string name)
    {
        switch(name)
        {
            case "QuitBtn": QuitBtn(); break;
            case "QuitWinCancel": QuitWinCancel(); break;
            case "QuitWinDoneBtn": QuitWinDoneBtn(); break;
            case "LobbyQuitWinDoneBtn": LobbyQuitWinDoneBtn(); break;
            case "ReStart": ReStart(); break;
        }
    }

    private void LobbyQuitWinDoneBtn()
    {
        Time.timeScale = 1f;
        RoundData.Instance.Reset(0);
        FadeLobby.Instance.fadeLobbyAnim.SetTrigger("OnFade");
    }

    private void QuitWinDoneBtn()
    {
        RoundData.Instance.Reset(0);
        Application.Quit();
    }

    private void QuitWinCancel()
    {
        quitWin.SetActive(false);
    }

    private void QuitBtn()
    {
        quitWin.SetActive(true);
    }

    private void ReStart()
    {
        Time.timeScale = 1f;
        FadeLobby.Instance.fadeInGameAnim.SetTrigger("OnFade");
        Managers.Sound.Play(FadeLobby.Instance.fadeSound);
    }
}
