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
    using MouseLuaEventHandler = platform.poker.MouseLuaEventHandler;
    using Tool = EditorTool;

    [CanEditMultipleObjects]
    [CustomEditor(typeof(MouseLuaEventHandler), false)]
    /// <summary> UI LuaSpaceObject Editor </summary>
    public class UIMouseLuaEventHandlerEditor : Base2DEditor
    { 
        private SerializedProperty luaScriptPath;
        private SerializedProperty sound;

        protected override void init()
        {   
            serObj = new SerializedObject(targets);
            luaScriptPath = serObj.FindProperty("luaScriptPath");
            sound = serObj.FindProperty("sound");
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
                        luaScriptPath.stringValue = Tool.AC.FieldText(luaScriptPath.stringValue.Replace(@"\", "/").ToString(), GStyleTool.textArea, GUILayout.Height(40));
                    });
                }, GStyleTool.label);

                if (luaScriptPath.stringValue.Length != 0)   //输出LuaTabel中信息
                {

                }

                Tool.BE.Vertical(() =>
                {
                    Tool.AC.FieldProperty("声音", sound);
                }, GStyleTool.label);

            }, GStyleTool.UIContent);
            foreach (var item in targets) Undo.RecordObject(item, "Undo UIMouseLuaEventHandlerEditor");
        }

        protected override void change()
        {
            foreach (var item in targets) EditorUtility.SetDirty(item);
        }
    }
}