using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInput : MonoBehaviour
{
    [SerializeField] private RectTransform inputKeyWin;
    [SerializeField] private RectTransform infoWin;
    [SerializeField] private RectTransform inputKeyWinPos;
    [SerializeField] private RectTransform infoWinPos;

    private bool isInfoMoving = false;
    private bool isInputKeyMoving = false;

    private void Update()
    {
        if (isInfoMoving)
        {
            inputKeyWin.anchoredPosition = Vector3.Lerp(inputKeyWin.anchoredPosition, infoWinPos.anchoredPosition, 0.005f);
            infoWin.anchoredPosition = Vector3.Lerp(infoWin.anchoredPosition, inputKeyWinPos.anchoredPosition, 0.005f);

            if (Vector3.Distance(inputKeyWin.anchoredPosition, inputKeyWinPos.anchoredPosition) < 0.01f)
            {
                isInfoMoving = false;
            }
        }
    }

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
        isInfoMoving = true;
    }

    private void InputKey()
    {
        isInputKeyMoving = true;
    }

    private void Setting()
    {

    }
}
