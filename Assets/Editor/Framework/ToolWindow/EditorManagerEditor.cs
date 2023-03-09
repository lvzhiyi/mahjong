///* * * * * * * * * * * * * * * * * * * * * * * *
//* Copyright(C) 2020 by XN
//* All rights reserved. 
//* FileName:         Editors.ToolWindow
//* Author:           XiNan 
//* Version:          0.1 
//* UnityVersion:     2019.2.18f1 
//* Date:             2020-05-21
//* Time:             00:00:42
//* E-Mail:           1398581458@qq.com
//* Description:        
//* History:          
//* * * * * * * * * * * * * * * * * * * * * * * * */

//namespace Framework.ToolWindow
//{
//    using Framework.Tool;
//    using UnityEditor;
//    using UnityEngine;
//    using Tool = EditorTool;

//    public class EditorManagerEditor : BaseWindowEditor
//    {
//        private static EditorWindow windows;

//        private EditorBean bean;

//        [MenuItem("Tool/Editor Manager %#g")]
//        protected static void ShowWindow()
//        {
//            windows = windows ?? GetWindow<EditorManagerEditor>();
//            windows.Show();
//        }

//        public EditorManagerEditor()
//        {
//            titleContent = new GUIContent("Editor Manager");
//            minSize = new Vector2(300, 300);
//        }


//        private bool SystemOF, SystemInfoOF;

//        private bool ScreenOF, ScreenInfoOF;

//        protected override void onGUI()
//        {
//            if (bean == null) { Tool.AC.ButtonRepeat(load, "Load", GStyleTool.GetName("button")); return; }      /* ���� */

//            vector = Tool.BE.ScrollView(() =>
//            {
//                Tool.BE.Horizontal(() =>
//                {
//                    Tool.AC.LabelPrefix("������");
//                    bean.Author = Tool.AC.FieldText(bean.Author);
//                });

//                Tool.BE.Vertical(() => { SystemOF = Tool.BE.FoldoutHeaderGroup(System_Level_1, SystemOF, "Ediotr System"); }, GStyleTool.content);

//                Tool.BE.Vertical(() => { SystemInfoOF = Tool.BE.FoldoutHeaderGroup(SystemInfo_Level_1, SystemInfoOF, "Ediotr System Info"); }, GStyleTool.content);

//                Tool.BE.Vertical(() => { ScreenOF = Tool.BE.FoldoutHeaderGroup(Screen_Level_1, ScreenOF, "Ediotr Screen"); }, GStyleTool.content);

//                Tool.BE.Vertical(() => { ScreenInfoOF = Tool.BE.FoldoutHeaderGroup(ScreenInfo_Level_1, ScreenInfoOF, "Ediotr Screen Info"); }, GStyleTool.content);

//            }, vector, GStyleTool.scrollView);

//            Tool.BE.Vertical(() =>   /* ���� */
//            {
//                Tool.AC.ButtonRepeat(save, "Save", GStyleTool.GetName("button"));
//            });
//        }

//        private void System_Level_1()
//        {
//            Tool.BE.Vertical(() =>
//            {
//                Tool.BE.Horizontal(() =>
//                {
//                    bean.DeveloperMode = Tool.AC.Toggle("������ģʽ", bean.DeveloperMode);
//                });
//            });
//        }

//        private void SystemInfo_Level_1()
//        {
//            Tool.BE.Vertical(() =>
//            {
//                Tool.BE.Horizontal(() =>
//                {
//                    Tool.AC.LabelPrefix("����ƽ̨");
//                    Tool.AC.HelpBox(Application.platform.ToString(), MessageType.None);
//                });
//            });
//        }

//        private void Screen_Level_1()
//        {
//            Tool.BE.Vertical(() =>
//            {
//                Tool.BE.Vertical(() =>
//                {
//                    Tool.AC.Label("��Ļ��ת", GStyleTool.title);

//                    Tool.BE.Horizontal(() =>
//                    {
//                        Tool.AC.LabelPrefix("��Ļ����:");
//                        bean.PlayerSettingOrientation = Tool.AC.PopupEnum(bean.PlayerSettingOrientation);
//                    });

//                    bean.AutorotateToLandscapeLeft = Tool.AC.Toggle("������ת", bean.AutorotateToLandscapeLeft);
//                    bean.AutorotateToLandscapeRight = Tool.AC.Toggle("������ת", bean.AutorotateToLandscapeRight);
//                    bean.AutorotateToPortrait = Tool.AC.Toggle("������ת", bean.AutorotateToPortrait);
//                    bean.AutorotateToPortraitUpsideDown = Tool.AC.Toggle("������ת", bean.AutorotateToPortraitUpsideDown);

//                });

//                Tool.EA.Space();

//                Tool.BE.Vertical(() =>
//                {
//                    Tool.AC.Label("�ֱ���", GStyleTool.title);

//#if UNITY_ANDROID || UNITY_IPHONE || UNITY_IOS
//#elif UNITY_EDITOR_WIN || UNITY_EDITOR 
//                    Tool.BE.Horizontal(() =>
//                    {
//                        Tool.AC.LabelPrefix("����");
//                        bean.ScreenHeight = Tool.AC.SliderInt(bean.ScreenHeight, 600, 1080);
//                    });

//                    Tool.BE.Horizontal(() =>
//                    {
//                        Tool.AC.LabelPrefix("����");
//                        bean.ScreenWidth = Tool.AC.SliderInt(bean.ScreenWidth, 1200, 1920);
//                    });
//#endif
//                    Tool.BE.Horizontal(() =>
//                    {
//                        bean.ScreenFull = Tool.AC.Toggle("ȫ��", bean.ScreenFull);
//                        Tool.AC.LabelSelectable("ȫ��ģʽ�ʹ���ģʽ֮���л�", GStyleTool.helpBox, GUILayout.Height(20));
//                    });
//                });

//                Tool.EA.Space();

//                Tool.BE.Horizontal(() =>
//                {
//                    Tool.AC.LabelPrefix("��Ļ����");
//                    bean.Brightness = Tool.AC.SliderInt(bean.Brightness, 30, 100);
//                });

//                Tool.BE.Horizontal(() =>
//                {
//                    Tool.AC.LabelPrefix("ˢ����");
//                    bean.RefreshRate = Tool.AC.PopupEnum(bean.RefreshRate);
//                });

//                Tool.BE.Horizontal(() =>
//                {
//                    Tool.AC.LabelPrefix("˯��ģʽ");
//                    bean.NeverSleep = Tool.AC.PopupEnum(bean.NeverSleep);
//                });

//                Tool.BE.Horizontal(() =>
//                {
//                    Tool.AC.LabelPrefix("��Ļģʽ");
//                    bean.ScreenFullMode = Tool.AC.PopupEnum(bean.ScreenFullMode);
//                });

//                Tool.EA.Space();
//            });
//        }

//        private bool screenInfo_resolutions;

//        private void ScreenInfo_Level_1()
//        {
//            Tool.BE.Vertical(() =>
//            {
//                Tool.BE.Horizontal(() =>
//                {
//                    Tool.AC.LabelPrefix("Screen Slef SafeArea");
//                    Tool.AC.HelpBox(string.Concat("W:", Screen.safeArea.width, " H:", Screen.safeArea.height, " X:", Screen.safeArea.x, " Y:", Screen.safeArea.y), MessageType.None);
//                });

//                Tool.BE.Horizontal(() =>
//                {
//                    Tool.AC.LabelPrefix("Screen Resolution");
//                    Tool.AC.HelpBox(string.Concat("W:", Screen.currentResolution.width, " H:", Screen.currentResolution.height), MessageType.None);
//                });

//                Tool.BE.Horizontal(() =>
//                {
//                    Tool.AC.LabelPrefix("Screen DPI");
//                    Tool.AC.HelpBox(Screen.dpi.ToString(), MessageType.None);
//                });

//                Tool.BE.Vertical(() =>
//                {
//                    Tool.BE.Horizontal(() =>
//                    {
//                        screenInfo_resolutions = Tool.AC.Toggle("Full Screen Resolution", screenInfo_resolutions);
//                        Tool.AC.LabelSelectable("Current Display Support", GStyleTool.helpBox, GUILayout.Height(20));
//                    });

//                    if (screenInfo_resolutions && Screen.resolutions.Length != 0)
//                    {
//                        for (int i = 0; i < Screen.resolutions.Length; i++)
//                        {
//                            Tool.BE.Horizontal(() =>
//                            {
//                                Tool.AC.LabelSelectable(string.Concat("NO:", (i + 1)), GStyleTool.helpBox, GUILayout.Height(20));
//                                Tool.AC.LabelSelectable(string.Concat("W:", Screen.resolutions[i].width), GStyleTool.helpBox, GUILayout.Height(20));
//                                Tool.AC.LabelSelectable(string.Concat("H:", Screen.resolutions[i].height), GStyleTool.helpBox, GUILayout.Height(20));
//                                Tool.AC.LabelSelectable(string.Concat("Hz:", Screen.resolutions[i].refreshRate), GStyleTool.helpBox, GUILayout.Height(20));
//                            });
//                        }
//                    }
//                });
//            });
//        }

//        protected override void change()
//        {
//            ScriptChange.AUTHOR = bean.Author;

//            if (EditorPrefs.GetBool("DeveloperMode") != bean.DeveloperMode)
//                EditorPrefs.SetBool("DeveloperMode", bean.DeveloperMode);

//            PlayerSettings.defaultInterfaceOrientation = (UIOrientation)bean.PlayerSettingOrientation;
//            PlayerSettings.accelerometerFrequency = bean.AccelerometerFrequency; //���ٶȼƸ���Ƶ�ʡ�
//            //web
//            PlayerSettings.defaultWebScreenHeight = bean.ScreenHeight;
//            PlayerSettings.defaultWebScreenWidth = bean.ScreenWidth;

//            PlayerSettings.defaultScreenHeight = bean.ScreenHeight;
//            PlayerSettings.defaultScreenWidth = bean.ScreenWidth;

//            PlayerSettings.fullScreenMode = (UnityEngine.FullScreenMode)bean.ScreenFullMode;
//            PlayerSettings.allowFullscreenSwitch = bean.ScreenFull;

//            Screen.SetResolution(bean.ScreenHeight, bean.ScreenHeight, (UnityEngine.FullScreenMode)bean.ScreenFullMode, (int)bean.RefreshRate);
//            Screen.brightness = bean.Brightness;
//            Screen.fullScreen = bean.ScreenFull;
//        }

//        protected override void load()
//        {
//            bean = new EditorBean();
//            bean.byteRead(new ByteBuffer(FileKit.Read(PathKit.SaveData.Config)));
//        }

//        protected override void save()
//        {
//            if (bean != null)
//            {
//                FileKit.Write(PathKit.SaveData.Config, bean.byteWrite(new ByteBuffer()).ToBytes());
//            }
//        }

//        protected override void close()
//        {
//            bean = null;
//            windows = null;
//        }
//    }
//}