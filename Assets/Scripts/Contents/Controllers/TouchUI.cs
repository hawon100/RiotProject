using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchUI : MonoBehaviour
{
    [SerializeField] private Define.EndType type = Define.EndType.None;
    private float lastTouchTime;
    private const float doubleTouchDelay = 0.5f;

    void Start()
    {
        lastTouchTime = Time.time;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TitleScene.isDoubleTouch = true;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            FadeLobby.Instance.fadeLobbyAnim.SetTrigger("OnFade");
        }

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    if (Time.time - lastTouchTime < doubleTouchDelay) // 더블터치 판정
                    {
                        FadeLobby.Instance.fadeLobbyAnim.SetTrigger("OnFade");
                    }
                    else
                    {

                    }
                    break;

                case TouchPhase.Moved:
                    break;

                case TouchPhase.Ended:
                    lastTouchTime = Time.time;
                    break;
            }
        }
    }
}
