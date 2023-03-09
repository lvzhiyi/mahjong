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
    using cambrian.unreal.scroll;
    using UnityEditor;
    using UnityEngine;
    using XLua;
    using Tool = EditorTool;

    [CustomEditor(typeof(ScrollBar), false)]
    /// <summary> UI LuaSpaceObject Editor </summary>
    public class UIScrollBarEditor : Base2DEditor
    {
        private LuaTable luaTable;
        private ScrollBar obj;
        private Injection[] injections;
        private int index;

        private SerializedProperty luaScriptPath;
        private SerializedProperty rect;
        private SerializedProperty siblingIndex;  //深度 int

        protected override void init()
        {
            obj = (ScrollBar)target;
            injections = obj.injections;
            index = obj.getIndex();
            luaTable = obj.LuaTable;

            serObj = new SerializedObject(target);
            luaScriptPath = serObj.FindProperty("luaScriptPath");
            siblingIndex = serObj.FindProperty("SiblingIndex");
            rect = serObj.FindProperty("rectTransform");
        }

        protected override void inspector()
        {
            Tool.BE.Vertical(() =>
            {
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
                    Tool.AC.FieldInt("索引", index);
                    Tool.AC.FieldProperty("深度", siblingIndex);
                    Tool.AC.FieldProperty("位置", rect);
                }, GStyleTool.label);

            }, GStyleTool.UIContent);
            Undo.RecordObject(target, "Undo UIMouseLuaEventHandlerEditor");
        }

        protected override void change()
        {
            EditorUtility.SetDirty(target);
        }
    }
}