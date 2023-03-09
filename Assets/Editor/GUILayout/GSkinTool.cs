/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XN
* All rights reserved. 
* FileName:         Editors.InspectorIDE.GUILayout 
* Author:           XiNan 
* Version:          0.1 
* UnityVersion:     2019.2.18f1 
* Date:             2020-05-04
* Time:             12:40:51
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework
{
    using UnityEngine;

    public static class GSkinTool
    {
        private static GUISkin _skin;

        public static GUISkin skin
        {
            get { if (_skin == null) defualtSkin(); return _skin; }
            private set { }
        }

        private static void defualtSkin()
        {
            if (_skin == null)
            {
                _skin = (GUISkin)Resources.Load("InspectorIDESkin");
                if (_skin == null)
                {
                    Debug.LogError("error: not find EditorSkin.");
                    return;
                }
                //�ı��ֶ��й�����ɫ
                _skin.settings.cursorColor = Color.green;
                //�ı��ֶι����˸���ٶ�
                _skin.settings.cursorFlashSpeed = 1f;
                //�ı��ֶ���ѡ����ε���ɫ��
                _skin.settings.selectionColor = Color.red;
                //Ӧ��˫���ı��ֶ��е�select words
                _skin.settings.doubleClickSelectsWord = true;
                //Ӧ�����ε��ѡ���ı��ֶ��е������ı�
                _skin.settings.tripleClickSelectsLine = true;
            }
        }
    }
}