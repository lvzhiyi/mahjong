/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XN 
* All rights reserved. 
* FileName:         Framework.UI
* Author:           XiNan 
* Version:          0.1 
* UnityVersion:     2019.3.13f1 
* Date:             2020-06-05
* Time:             00:25:47
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.UI
{
    using Framework.ToolWindow;
    using UnityEditor;
    using UnityEngine;
    using XLua;
    using Tool = EditorTool;

    [CustomEditor(typeof(UnrealLuaPanel), true)]
    /// <summary> UIPanel Editor </summary>
    public class UIPanelEditor : Base2DEditor
    {
        private LuaTable luaTable;
        private UnrealLuaPanel panel;
        private Injection[] injections;

        private SerializedProperty luaScriptPath;
        private SerializedProperty layer;
        private SerializedProperty tweener;
        private SerializedProperty canvas;
        private SerializedProperty isDestory;
        private SerializedProperty panelid;
        private SerializedProperty isShow1;

        protected override void init()
        {
            panel = (UnrealLuaPanel)target;
            luaTable = panel.luaTable;
            injections = panel.injections;

            serObj = new SerializedObject(target);
            luaScriptPath = serObj.FindProperty("luaScriptPath");
            layer = serObj.FindProperty("layer");
            tweener = serObj.FindProperty("tweener");
            canvas = serObj.FindProperty("content");
            isDestory = serObj.FindProperty("isDestory");
            panelid = serObj.FindProperty("id");
            isShow1 = serObj.FindProperty("isShow1");
        }

        private string platform;

        protected override void inspector()
        {
            Tool.BE.Vertical(() =>
            {

                Tool.AC.FieldLabel(string.Concat("<color=#FFA500><b>", serObj.targetObject.name, "</b></color>"), GStyleTool.title);

                Tool.EA.Separator();

                Tool.BE.Vertical(() =>
                {
                    Tool.AC.FieldLabel("<color=#87CEFA>LUA File Path</color>", GStyleTool.title, GUILayout.Height(20));
                    Tool.BE.Horizontal(() =>
                    {
                        luaScriptPath.stringValue = Tool.AC.FieldText(luaScriptPath.stringValue.Replace(@"\", "/").ToString(), GStyleTool.textArea, GUILayout.Height(60));
                    });
                }, GStyleTool.label);

                if (luaScriptPath.stringValue.Length != 0)   //输出LuaTabel中信息
                {

                }

                Tool.BE.Vertical(() =>
                {
                    Tool.AC.FieldProperty("面板层级", layer);
                    Tool.AC.FieldProperty("出现缓动", tweener);
                    Tool.AC.FieldProperty("Canvas", canvas);
                    Tool.AC.FieldProperty("关闭销毁", isDestory);
                    Tool.AC.FieldProperty("面板ID", panelid);
                    Tool.AC.FieldProperty("不受其他因素影响", isShow1);

                }, GStyleTool.label);

                Tool.BE.Vertical(() =>
                {
                    Tool.AC.Label(string.Concat("<color=#FFA500><b>", "游戏规则设置", "</b></color>"), GStyleTool.title);
                    platform = Tool.AC.FieldText("游戏类型", platform);
                    if (Tool.AC.Button("设置全局LUA文件路径", GStyleTool.GetName("button")))
                    {
                        if (platform.Length != 0) SetLuaFilePath(panel.gameObject, platform);
                        else EditorUtility.DisplayDialog("提示", "请输入设置信息", "确定");
                    }
                }, GStyleTool.label);


                Tool.AC.ButtonRepeat(OpenAttributeSetWindow, "属性设置窗口", GStyleTool.GetName("button"));
                //Tool.AC.LabelSelectable("下次渲染的时候销毁", GStyleTool.title, GUILayout.Height(15));
            }, GStyleTool.UIContent);
            Undo.RecordObject(target, "Undo UIPanel");
        }

        protected void OpenAttributeSetWindow()
        {
            RaycastTargetChecker.Open((UnrealLuaPanel)target);
        }

        protected override void change()
        {
            EditorUtility.SetDirty(target);
        }

        public static void SetLuaFilePath(GameObject root, string platform)
        {
            var path = string.Concat("/Platform/", platform, "/Panel/");
            foreach (var item in root.transform.GetComponentsInChildren<UnrealLuaSpaceObject>())
            {
                if (typeof(UnrealLuaSpaceObject).Name == item.GetType().Name)
                    item.luaScriptPath = string.Concat(path, item.name, '/');
            }
            foreach (var item in root.transform.GetComponentsInChildren<MouseEventProcess>())
            {
                if (item.name == "inviteorjoin") continue;
                item.luaScriptPath = string.Concat(path, item.name, '/');
            }
        }
    }
}