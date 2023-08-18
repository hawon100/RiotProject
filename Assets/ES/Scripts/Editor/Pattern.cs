using UnityEngine;
using UnityEditor;
using System.Reflection;

public class Pattern : EditorWindow
{
    GameObject cube0;
    GameObject cube1;
    Transform point;

    string myString = "Hello, World!";

    public void DebugInfo()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }

    [MenuItem("Tool/pattern")]
    public static void ShowWindow()
    {
        GetWindow<Pattern>("pattern");
    }

    private void OnGUI()
    {
        GUILayout.Label("Pattern", EditorStyles.boldLabel);

        myString = EditorGUILayout.TextField("Name", myString);

        cube0 = EditorGUILayout.ObjectField("Cube0", cube0, typeof(GameObject), true) as GameObject;
        cube1 = EditorGUILayout.ObjectField("Cube1", cube1, typeof(GameObject), true) as GameObject;

        point = EditorGUILayout.ObjectField("Point", point, typeof(Transform), true) as Transform;

        if (GUILayout.Button("Press Me"))
        {
            Debug.Log(myString);
        }

        if (GUILayout.Button("Clear"))
        {
            DebugInfo();
        }


        if (GUILayout.Button("Spawn"))
        {
            switch (myString)
            {
                case "A": Instantiate(cube0, point.position, point.rotation); break;
                case "B": Instantiate(cube1, point.position, point.rotation); break;
            }
        }
    }
}
