using UnityEngine;
using UnityEditor;

public class edit_WIndow : EditorWindow
{
    int[,] data;

    public static void Init()
    {
        var window = GetWindow<edit_WIndow>();
        window.titleContent = new GUIContent("edit_WIndow");
        window.Show();

        window.titleContent.text = "Map Data";
    }

    private void OnGUI()
    {
        data = new int[3,3];

        for (int x = 0; x < 3; x++)
        {
            GUILayout.BeginHorizontal();
            for (int y = 0; y < 3; y++){
                data[x, y] = EditorGUILayout.IntField(data[x, y]);
            }
            GUILayout.EndHorizontal();
        }
    }
}
