using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MouseClickProcess))]
public class MouseClickProcessEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Open TextEditorWindow"))
        {
            MouseClickTextEditorWindow window = (MouseClickTextEditorWindow)EditorWindow.GetWindow(typeof(MouseClickTextEditorWindow));
            window.Show();
        }
    }
}