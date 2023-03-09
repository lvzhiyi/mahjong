namespace Framework.ToolWindow
{
    using UnityEditor;
    using UnityEngine;
    using Tool = EditorTool;

    public class GUIStyleViewer : BaseWindowEditor
    {
        private static new EditorWindow window;
        [MenuItem("Tool Windows/GUIStyle Viewer")]
        public static void Open()
        {
            window = window ?? GetWindow<GUIStyleViewer>(true, "GUIStyle Viewer", true);//创建窗口
            window.wantsMouseMove = true;
            window.Show(true);//展示         
        }

        public GUIStyleViewer()
        {
            minSize = new Vector2(600, 800);
        }

        private string search = "";

        private void OnGUI()
        {
            Tool.BE.Horizontal(() =>
            {
                GUILayout.Space(30);
                search = EditorGUILayout.TextField("", search, "SearchTextField", GUILayout.MaxWidth(position.x / 3));
                Tool.AC.Label("", "SearchCancelButtonEmpty");
            }, "HelpBox");
            vector = Tool.BE.ScrollView(() =>
            {
                foreach (GUIStyle style in GUI.skin.customStyles)
                {
                    if (style.name.ToLower().Contains(search.ToLower()))
                    {
                        DrawStyleItem(style);
                    }
                }
            }, vector);
        }

        private void DrawStyleItem(GUIStyle style)
        {
            Tool.BE.Horizontal(() =>
            {
                GUILayout.Space(40);
                Tool.AC.LabelSelectable(style.name, GUILayout.Height(40));
                GUILayout.FlexibleSpace();
                Tool.AC.LabelSelectable(style.name, style, GUILayout.Height(40));
                GUILayout.Space(40);
                Tool.AC.LabelSelectable("", style, GUILayout.Height(40), GUILayout.Width(40));
                GUILayout.Space(50);
                if (Tool.AC.Button("Copy"))
                {
                    TextEditor textEditor = new TextEditor();
                    textEditor.text = style.name;
                    textEditor.OnFocus();
                    textEditor.Copy();
                }
            }, GStyleTool.helpBox, GUILayout.Height(40));
            GUILayout.Space(10);
        }

        protected override void close()
        {
            window = null;
        }
    }
}