using UnityEditor;
using UnityEngine;

public class MouseClickTextEditorWindow : EditorWindow
{
    //[MenuItem("spotred/MouseClickTextEditorWindow")]
    public static void ShowWindow()
    {
        MouseClickTextEditorWindow window = (MouseClickTextEditorWindow)EditorWindow.GetWindow(typeof(MouseClickTextEditorWindow));
        window.Show();
    }

    ////-----------------------------------START---------------------------------------------
    string text = "Nothing Opened...";
    Vector2 scroll;

    string key= "refreshScript";

    private void OnGUI()
    {
        GUILayout.Label("Select an object in the hierarchy view");

        GameObject gameobj = Selection.activeGameObject;
        if (gameobj)
        {
            gameobj.name = EditorGUILayout.TextField("Object Name: ", gameobj.name);
            MouseClickProcess process = gameobj.GetComponent<MouseClickProcess>();
            if (process != null)
            {
                GUI.SetNextControlName("lua_exe");
                if (GUILayout.Button("执行代码:lua_exe"))
                {
                    key = "lua_exe";
                    GUI.FocusControl("lua_exe");
                }
                //GUI.SetNextControlName("refreshScript");
                //if (GUILayout.Button("刷新代码:refreshScript"))
                //{
                //    key = "refreshScript";
                //    GUI.FocusControl("refreshScript");
                //}
                //GUI.SetNextControlName("executeScript");
                //if (GUILayout.Button("执行代码:executeScript"))
                //{
                //    key = "executeScript";
                //    GUI.FocusControl("executeScript");
                //}
                //GUI.SetNextControlName("onCommandScript");
                //if (GUILayout.Button("通信回调代码:onCommandScript"))
                //{
                //    key = "onCommandScript";
                //    GUI.FocusControl("onCommandScript");
                //}
                //GUI.SetNextControlName("onCommandErrorScript");
                //if (GUILayout.Button("通信错误回调代码:onCommandErrorScript"))
                //{
                //    key = "onCommandErrorScript";
                //    GUI.FocusControl("onCommandErrorScript");
                //}

                GUILayout.Label("key:" + key);
                scroll = EditorGUILayout.BeginScrollView(scroll);
                this.resetValue(process);
                EditorGUILayout.EndScrollView();
            }
        }
        if (GUILayout.Button("Close")) this.Close();
        if (GUILayout.Button("copyXLua")) this.copyXLua();
        //if (GUILayout.Button("toCSAndCopy")) this.toCSAndCopy();
        //if (GUILayout.Button("toXLuaAndCopy")) this.toXLuaAndCopy();
        this.Repaint();
    }

    private void copyXLua()
    {
        string str = "";
        GameObject gameobj = Selection.activeGameObject;
        MouseClickProcess process = gameobj.GetComponent<MouseClickProcess>();
        //if (key == "lua_exe") str = process.lua_exe;
        //else if (key == "refreshScript") str = process.refreshScript;
        //else if (key == "executeScript") str = process.executeScript;
        //else if (key == "onCommandScript") str = process.onCommandScript;
        //else if (key == "onCommandErrorScript") str = process.onCommandErrorScript;
        TextEditor textEditor = new TextEditor();
        textEditor.text = str;
        textEditor.OnFocus();
        textEditor.Copy();
    }

    private void toCSAndCopy()
    {

    }

    private void toXLuaAndCopy()
    {

    }

    private void resetValue(MouseClickProcess process)
    {
        if (key == "lua_exe")
        {
            //text = EditorGUILayout.TextArea(process.lua_exe, GUILayout.Height(position.height - 30));
            //process.lua_exe = text;
        }
        //else if (key == "refreshScript")
        //{
        //    text = EditorGUILayout.TextArea(process.refreshScript, GUILayout.Height(position.height - 30));
        //    process.refreshScript = text;
        //}
        //else if (key == "executeScript")
        //{
        //    text = EditorGUILayout.TextArea(process.executeScript, GUILayout.Height(position.height - 30));
        //    process.executeScript = text;
        //}
        //else if (key == "onCommandScript")
        //{
        //    text = EditorGUILayout.TextArea(process.onCommandScript, GUILayout.Height(position.height - 30));
        //    process.onCommandScript = text;
        //}
        //else if (key == "onCommandErrorScript")
        //{
        //    text = EditorGUILayout.TextArea(process.onCommandErrorScript, GUILayout.Height(position.height - 30));
        //    process.onCommandErrorScript = text;
        //}
    }
}
