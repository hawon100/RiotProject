using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidPlugin : MonoBehaviour
{
    private AndroidJavaObject UnityActivity;
    private AndroidJavaObject UnityInstance;

    // Start is called before the first frame update
    void Start()
    {
        AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        UnityActivity = ajc.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaClass ajc2 = new AndroidJavaClass("com.example.unitylove.Uplugin");
        UnityInstance = ajc2.CallStatic<AndroidJavaObject>("instance");

        UnityInstance.Call("setContext", UnityActivity);
    }

    public void ShowToast(string msg, bool isLong)
    {
        UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            if (isLong == false)
            {
                UnityInstance.Call("ShowToast", msg, 0);
            }
            else
            {
                UnityInstance.Call("ShowToast", msg, 1);
            }
        }));
    }
}
