using UnityEngine;
using UnityEditor;

public class Environment : EditorWindow
{
    GameObject ground;

    [MenuItem("Tool/Environment")]
    public static void ShowWindow()
    {
        GetWindow<Environment>("environment");
    }

    private void OnGUI()
    {
        GUILayout.Label("Environment", EditorStyles.boldLabel);

        Make();
    }

    void Make()
    {
        ground = EditorGUILayout.ObjectField("ground", ground, typeof(GameObject), true) as GameObject;

        if(GUILayout.Button("Make Ground"))
        {
            Instantiate(ground);
        }
    }
}
