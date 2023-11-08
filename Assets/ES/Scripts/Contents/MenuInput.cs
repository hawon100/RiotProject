using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInput : MonoBehaviour
{
    [SerializeField] private GameObject inputBackWin;
    [SerializeField] private GameObject infoWin;
    [SerializeField] private GameObject settingWin;
    [SerializeField] private GameObject muteIcon;
    [SerializeField] private GameObject audioListener;

    private bool isInfoWinActive;
    private bool isInputBackWinActive;
    private bool isMute = true;

    public void Menu(string name)
    {
        switch (name)
        {
            case "Info": Info(); break;
            case "InputKey": InputKey(); break;
            case "Setting": Setting(); break;
            case "Mute": Mute(); break; 
        }
    }

    private void Mute()
    {
        isMute = !isMute;
        muteIcon.SetActive(isMute);
        audioListener.GetComponent<AudioListener>().enabled = isMute;
    }

    private void Info()
    {
        isInputBackWinActive = false;
        isInfoWinActive = true;
        inputBackWin.SetActive(isInputBackWinActive);
        infoWin.SetActive(isInfoWinActive);
    }

    private void InputKey()
    {
        isInputBackWinActive = true;
        isInfoWinActive = false;
        inputBackWin.SetActive(isInputBackWinActive);
        infoWin.SetActive(isInfoWinActive);
    }

    private void Setting()
    {
        settingWin.SetActive(true);
        Time.timeScale = 0f;
    }

    public void SettingQuit()
    {
        settingWin.SetActive(false);
        Time.timeScale = 1f;
    }
}
