using UnityEditor;
using UnityEngine;

public class LuaProcessEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Open TextEditorWindow"))
        {
           // TextEditorWindow window = (TextEditorWindow)EditorWindow.GetWindow(typeof(TextEditorWindow));
           // window.Show();
        }
    }
}