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
        }
    }

    private void LobbyQuitWinDoneBtn()
    {
        Time.timeScale = 1f;
        RoundData.Instance.Reset(0);
        Managers.Map.LoadScene(Define.Scene.Lobby);
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
}
