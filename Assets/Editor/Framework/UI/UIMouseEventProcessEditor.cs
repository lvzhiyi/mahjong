/* * * * * * * * * * * * * * * * * * * * * * * *
* mqipai
* FileName:         Framework.UI
* Author:           XiNan
* Version:          14.4.1
* UnityVersion:     2018.2.21f1
* Date:             2021-02-22
* Time:             16:12:30
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.UI
{
    using UnityEditor;
    using UnityEngine;
    using Tool = EditorTool;

    [CustomEditor(typeof(MouseEventProcess), false)]
    public class UIMouseEventProcessEditor : Base2DEditor
    {
        private MouseEventProcess obj;
        private Injection[] injections;

        private SerializedProperty luaScriptPath;
        private SerializedProperty sound;

        protected override void init()
        {
            obj = (MouseEventProcess)target;
            injections = obj.injections;

            serObj = new SerializedObject(target);
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
                        luaScriptPath.stringValue = Tool.AC.FieldText(luaScriptPath.stringValue.Replace(@"\", "/").ToString(), GStyleTool.textArea, GUILayout.Height(60));
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
            Undo.RecordObject(target, "Undo UIMouseLuaEventHandlerEditor");
        }

        protected override void change()
        {
            EditorUtility.SetDirty(target);
        }
    }
}