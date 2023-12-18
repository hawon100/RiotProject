using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInput : MonoBehaviour
{
    [SerializeField] private CameraMove cam;
    [SerializeField] private GameObject inputBackWin;
    [SerializeField] private GameObject settingWin;
    [SerializeField] private GameObject muteIcon;
    [SerializeField] private GameObject topIcon;
    [SerializeField] private GameObject easyIcon;

    private bool isMute;
    private bool isTop;
    private bool isEasy;

    public void Menu(string name)
    {
        if(!Player.Instance.isDead)
        {
            switch (name)
            {
                case "Setting": Setting(); break;
                case "Mute": Mute(); break;
                case "Top": Top(); break;
                case "Difficulty": Difficulty(); break;
            }
        }
    }

    private void Start() {
        isEasy = RoundData.Instance.isEasy;
        isTop = RoundData.Instance.isTop;
        isMute = RoundData.Instance.isMute;

        if (!Player.Instance.isLobby) AudioPlay.Instance.myAudio.mute = !isMute;
        cam.isTop = isTop;

        muteIcon.SetActive(isMute);
        topIcon.SetActive(isTop);
        easyIcon.SetActive(isEasy);
    }

    private void Mute()
    {
        isMute = !isMute;
        muteIcon.SetActive(isMute);
        if (!Player.Instance.isLobby) AudioPlay.Instance.myAudio.mute = !isMute;
        RoundData.Instance.isMute = isMute;
    }

    private void Top()
    {
        isTop = !isTop;
        topIcon.SetActive(isTop);
        cam.isTop = isTop;
        RoundData.Instance.isTop = isTop;
    }

    private void Difficulty()
    {
        isEasy = !isEasy;   
        easyIcon.SetActive(isEasy);
        RoundData.Instance.isEasy = isEasy;
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
