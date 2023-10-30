using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInput : MonoBehaviour
{
    [SerializeField] private RectTransform inputKeyWin;
    [SerializeField] private RectTransform infoWin;
    [SerializeField] private RectTransform inputKeyWinPos;
    [SerializeField] private RectTransform infoWinPos;

    public void Menu(string name)
    {
        switch (name)
        {
            case "Pause": Pause(); break;
            case "Info": Info(); break;
            case "InputKey": InputKey(); break;
            case "Setting": Setting(); break;
        }
    }

    private void Pause()
    {

    }

    private void Info()
    {
        inputKeyWin.anchoredPosition = Vector3.Lerp(inputKeyWin.anchoredPosition, infoWinPos.anchoredPosition, 0.005f);
        infoWin.anchoredPosition = Vector3.Lerp(infoWin.anchoredPosition, inputKeyWinPos.anchoredPosition, 0.005f);
    }

    private void InputKey()
    {
        inputKeyWin.anchoredPosition = Vector3.Lerp(inputKeyWin.anchoredPosition, inputKeyWinPos.anchoredPosition, 0.005f);
        infoWin.anchoredPosition = Vector3.Lerp(infoWin.anchoredPosition, infoWinPos.anchoredPosition, 0.005f);
    }

    private void Setting()
    {

    }
}
