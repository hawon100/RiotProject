using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInput : MonoBehaviour
{
    [SerializeField] private GameObject inputBackWin;
    [SerializeField] private GameObject infoWin;
    [SerializeField] private GameObject settingWin;
    [SerializeField] private GameObject muteIcon;
    [SerializeField] private GameObject easyIcon;

    private bool isInfoWinActive;
    private bool isInputBackWinActive;
    private bool isMute;
    private bool isEasy;

    public void Menu(string name)
    {
        if(!Player.Instance.isDead)
        {
            switch (name)
            {
                case "Info": Info(); break;
                case "InputKey": InputKey(); break;
                case "Setting": Setting(); break;
                case "Mute": Mute(); break;
                case "Difficulty": Difficulty(); break;
            }
        }
    }

    private void Start() {
        isEasy = RoundData.Instance.isEasy;
        isMute = RoundData.Instance.isSound;

        if (!Player.Instance.isLobby) AudioPlay.Instance.myAudio.mute = !isMute;

        muteIcon.SetActive(isMute);
        easyIcon.SetActive(isEasy);
    }

    private void Mute()
    {
        isMute = !isMute;
        muteIcon.SetActive(isMute);
        if (!Player.Instance.isLobby) AudioPlay.Instance.myAudio.mute = !isMute;
        RoundData.Instance.isSound = isMute;
    }

    private void Difficulty()
    {
        isEasy = !isEasy;
        easyIcon.SetActive(isEasy);
        RoundData.Instance.isEasy = isEasy;
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
