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
    using UnityEngine.UI;
    using XLua;
    using Gradient = cambrian.uui.Gradient;
    using Tool = EditorTool;

    [CustomEditor(typeof(UnrealTextField), false)]
    /// <summary> UI LuaSpaceObject Editor </summary>
    public class UITextFieldEditor : Base2DEditor
    {
        private UnrealTextField obj;
        private LuaTable luaTable;
        private Injection[] injections;

        private SerializedProperty luaScriptPath;
        private SerializedProperty isShow1;       //是否需要显示，不受关闭其他面板的影响
        private SerializedProperty visible;       //逻辑上是否显示。因绘制原因 getVisible和其并不同步

        private Outline[] outline;
        private Shadow[] shadow;
        private Gradient gradient;
        private Text textField;

        protected override void init()
        {
            obj = (UnrealTextField)target;
            injections = obj.injections;
            luaTable = obj.LuaTable;

            serObj = new SerializedObject(target);
            luaScriptPath = serObj.FindProperty("luaScriptPath");
            visible = serObj.FindProperty("visible");
            isShow1 = serObj.FindProperty("isShow1");

            create();

            textField = obj.transform.Find("text").GetComponent<Text>();
            outline = textField.GetComponents<Outline>();
            shadow = textField.GetComponents<Shadow>();
            gradient = textField.GetComponent<Gradient>();
        }

        private void create()
        {
            if (obj.transform.Find("text") == null)
            {
                var var = new GameObject("text");
                var rect = var.AddComponent<RectTransform>();
                var orect = obj.transform as RectTransform;
                rect.pivot = orect.pivot;
                rect.anchorMin = orect.anchorMin;
                rect.anchorMax = orect.anchorMax;
                rect.sizeDelta = orect.sizeDelta;

                var.AddComponent<Text>();
                var.transform.SetParent(obj.transform);
                var.transform.SetAsFirstSibling();
                var.transform.localScale = Vector3.one;
                var.transform.localPosition = new Vector3(0, 0, 0);

                rect.pivot = new Vector2(0.5f, 0.5f);
                rect.anchorMin = new Vector2(0, 0);
                rect.anchorMax = new Vector2(1, 1);
                rect.sizeDelta = new Vector2(0, 0);
            }
        }


        protected override void inspector()
        {
            Tool.BE.Vertical(() =>
            {
                //Tool.BE.Vertical(() =>
                //{
                //    Tool.AC.FieldLabel("<color=#87CEFA>LUA File Path</color>", GStyleTool.title, GUILayout.Height(20));
                //    Tool.BE.Horizontal(() =>
                //    {
                //        luaScriptPath.stringValue = Tool.AC.FieldText(luaScriptPath.stringValue.Replace(@"\", "/").ToString(), GStyleTool.textArea, GUILayout.Height(60));
                //    });
                //}, GStyleTool.label);

                if (luaScriptPath.stringValue.Length != 0)   //输出LuaTabel中信息
                {

                }

                Tool.BE.Vertical(() =>
                {
                    Tool.AC.FieldLabel("<color=#87CEFA>内容</color>", GStyleTool.title, GUILayout.Height(20));
                    Tool.BE.Horizontal(() =>
                    {
                        obj.text = Tool.AC.FieldText(obj.text, GStyleTool.textArea, GUILayout.Height(60));
                    });
                    textField.color = Tool.AC.FieldColor("字体颜色", textField.color);
                    if (outline != null)
                    {
                        for (int i = 0; i < outline.Length; i++)
                            outline[i].effectColor = Tool.AC.FieldColor("描边颜色", outline[i].effectColor);
                    }

                    if (shadow != null)
                    {
                        for (int i = 0; i < shadow.Length; i++)
                        {
                            if (shadow[i].GetType().Name == typeof(Shadow).Name)
                                shadow[i].effectColor = Tool.AC.FieldColor("阴影颜色", shadow[i].effectColor);
                        }
                    }

                    if (gradient != null)
                    {
                        gradient.colorTop = Tool.AC.FieldColor("渐变颜色 上", gradient.colorTop);
                        gradient.colorBottom = Tool.AC.FieldColor("渐变颜色 下", gradient.colorBottom);
                    }


                    obj.alpha = Tool.AC.Slider("透明度", obj.alpha, 0, 1);
                    Tool.AC.FieldProperty("显示", visible);
                    Tool.AC.FieldProperty("不受其他因素影响", isShow1);
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