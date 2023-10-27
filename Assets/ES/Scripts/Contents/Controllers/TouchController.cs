using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TouchController : MonoBehaviour
{
    private float lastTouchTime;
    private const float doubleTouchDelay = 0.5f;

    void Start()
    {
        lastTouchTime = Time.time;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            TitleScene.isDoubleTouch = true;
        }

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    if (Time.time - lastTouchTime < doubleTouchDelay) // 더블터치 판정
                    {
                        TitleScene.isDoubleTouch = true;
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
