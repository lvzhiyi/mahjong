/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XN 
* All rights reserved. 
* FileName:         Framework.UI
* Author:           XiNan 
* Version:          0.1 
* UnityVersion:     2018.2.21f1 
* Date:             2021-01-18
* Time:             16:23:47
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */
namespace Framework.UI
{
    using UnityEditor;
    using UnityEngine;
    using XLua;
    using Tool = EditorTool;

    [CanEditMultipleObjects]
    [CustomEditor(typeof(UnrealLuaSpaceObject), false)]
    /// <summary> UI LuaSpaceObject Editor </summary>
    public class UILuaSpaceObjectEditor : Base2DEditor
    {
        private SerializedProperty luaScriptPath;
        private SerializedProperty siblingIndex;  //深度 int
        private SerializedProperty index_space;   //索引 int
        private SerializedProperty bar_bg;        //背景图片
        private SerializedProperty visible;       //逻辑上是否显示。因绘制原因 getVisible和其并不同步
        private SerializedProperty isShow1;       //是否需要显示，不受关闭其他面板的影响

        protected override void init()
        {
            serObj = new SerializedObject(targets);
            luaScriptPath = serObj.FindProperty("luaScriptPath");
            siblingIndex = serObj.FindProperty("SiblingIndex");
            index_space = serObj.FindProperty("index_space");
            bar_bg = serObj.FindProperty("bar_bg");
            visible = serObj.FindProperty("visible");
            isShow1 = serObj.FindProperty("isShow1");
        }

        protected override void inspector()
        {
            Tool.BE.Vertical(() =>
            {
                Tool.BE.Vertical(() =>
                {
                    Tool.AC.FieldLabel("<color=#87CEFA>LUA File Path</color>", GStyleTool.title, GUILayout.Height(20));
                    luaScriptPath.stringValue = Tool.AC.FieldText(luaScriptPath.stringValue.Replace(@"\", "/").ToString(), GStyleTool.textArea, GUILayout.Height(40));
                }, GStyleTool.label);

                Tool.BE.Vertical(() =>
                {
                    Tool.AC.FieldProperty("深度", siblingIndex);
                    Tool.AC.FieldProperty("索引", index_space);
                    Tool.AC.FieldProperty("显示", visible);
                    Tool.AC.FieldProperty("背景图片", bar_bg);
                    Tool.AC.FieldProperty("不受其他因素影响", isShow1);
                }, GStyleTool.label);

            }, GStyleTool.UIContent);
            foreach (var item in targets) Undo.RecordObject(item, "Undo UILuaSpaceObjectEditor");
        }

        protected override void change()
        {
            foreach (var item in targets) EditorUtility.SetDirty(item);
        }
    }
}