/* * * * * * * * * * * * * * * * * * * * * * * *
* mqipai
* FileName:         Framework.ToolWindow
* Author:           XiNan
* Version:          14.4.1
* UnityVersion:     2018.2.21f1
* Date:             2021-03-17
* Time:             15:08:06
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.ToolWindow
{
    using cambrian.common;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using UnityEditor;
    using UnityEngine;
    using Tool = EditorTool;

    public class OutResetLuaFileEditor : BaseWindowEditor
    {
        public OutResetLuaFileEditor()
        {
            titleContent = new GUIContent("Out ResetLua File");
            minSize = new Vector2(400, 700);
            maxSize = new Vector2(1600, 900);
        }

        private static new EditorWindow window;
        [MenuItem("Tool Windows/Out ResetLua File %#x")]
        public static void Open()
        {
            if (window != null)
            {
                window.Close();
                window = null;
            }
            else
            {
                window = window ?? GetWindow<OutResetLuaFileEditor>(true, "Out ResetLua File", true);//创建窗口
                window.wantsMouseMove = true;
                window.Show(true);//展示    
            }
        }

        private const int OneBtnWidth = 80;
        private int select = 0;

        private GUIContent[] floders;
        private ArrayList<string> AllPathfile;
        private bool openPathfileView = true;

        private string nomoral = "FFFFFF";

        protected override void onGUI()
        {
            Tool.BE.Horizontal(() =>
            {
                Tool.AC.Label(string.Concat("<color=#FFFF00> CFG文件夹 </color>"), GStyleTool.title);
            }, GStyleTool.helpBox);

            if (AllPathfile == null)
            {
                Tool.BE.Horizontal(() =>
                {
                    Tool.AC.Label("", GUILayout.Width(80));
                    select = Tool.AC.Popup(select, floders, GStyleTool.GetName("ExposablePopupMenu"));
                    if (Tool.AC.Button("注入", GStyleTool.button, GUILayout.Width(80)))
                    {
                        AllPathfile = load(string.Concat(Application.persistentDataPath, "/ab/", floders[select].text, '/', "xdcpasset1.0.24.cfg"));
                    }
                });
            }
            else
            {
                Tool.AC.Label(string.Concat("<color=#FFFF00> 收集文件合集 </color>"), GStyleTool.title);
                Tool.BE.Horizontal(() =>
                {
                    if (Tool.AC.Button(openPathfileView ? "隐藏列表" : "显示列表", GStyleTool.button)) openPathfileView = !openPathfileView;

                    if (Tool.AC.Button("提取注入", GStyleTool.button))
                    {
                        var time = DateTime.Now;
                        WirteFileText(WirteXluaHotfixFile(AllPathfile.toArray()), "resetlua.lua", false);
                        Debug.Log(string.Concat("注入 : <<color=#FF5B00>", floders[select].text, "</color>> 完成! 一共用时 : <color=#FF5B00>", EditorKit.ExecDateDiff(time, DateTime.Now), "</color>"));
                    }

                    if (Tool.AC.Button("关闭文件", GStyleTool.button)) { AllPathfile = null; return; }

                }, GStyleTool.content);

                if (openPathfileView && AllPathfile != null)
                {
                    vector = Tool.BE.ScrollView(() =>
                    {
                        var colors = nomoral;
                        Tool.BE.Vertical(() =>
                        {
                            foreach (var item in AllPathfile.toArray())
                            {   //判断该文件是否新增 删除 
                                Tool.BE.Horizontal(() =>
                                {
                                    if (Tool.AC.Button("追加注入", GStyleTool.button, GUILayout.Width(80)))
                                    {
                                        var time = DateTime.Now;
                                        WirteFileText(WirteXluaHotfixFile(new string[1] { item }), "resetlua.lua", true);
                                        Debug.Log(string.Concat("注入 : <<color=#FF5B00>", floders[select].text, "</color>> 完成! 一共用时 : <color=#FF5B00>", EditorKit.ExecDateDiff(time, DateTime.Now), "</color>"));
                                    }
                                    Tool.AC.Label(string.Concat("<color=#", colors, ">", item, "</color>"), GStyleTool.GetName("HeaderLabel"), GUILayout.Height(20));
                                }, GStyleTool.helpBox);
                            }
                        }, GStyleTool.content);
                    }, vector);
                }
            }
        }

        private ArrayList<string> load(string filePath)
        {
            if (!File.Exists(filePath)) throw new ArgumentException("Config file not found! " + filePath);
            using (StreamReader reader = new StreamReader(new FileStream(filePath, FileMode.Open)))
            {
                var listAll = new Dictionary<string, ArrayList<string>>();
                try
                {
                    string line = null, target = null, _target = null; // 当前标签
                    var list = new ArrayList<string>();
                    while ((line = reader.ReadLine()) != null)
                    {
                        line = line.Trim();
                        if (line.Length == 0 || line[0] == '#') continue;
                        _target = checkToken(line);
                        if (_target != null)
                        {
                            if (target == null) // 第一次出现标签
                            {
                                target = _target;
                                list.clear();
                            }
                            else // 第二次出现标签
                            {
                                if (!string.Equals(target, _target, StringComparison.CurrentCultureIgnoreCase))
                                    throw new SystemException(string.Concat("Config Error, The target [", target, "] must he end "));
                                if (list.count != 0) listAll.Add(target, new ArrayList<string>(list.toArray()));
                                target = null;
                            }
                        }   // 处于标签内值
                        else if (target != null) list.add(line);
                    }
                }
                catch (IOException e) { throw new SystemException(string.Concat("Config file load Error! file = ", filePath), e); }
                finally { try { if (reader != null) reader.Close(); } catch (IOException e) { } }
                if (listAll["luas"] != null) return listAll["luas"];
            }
            return null;
        }

        private void WirteFileText(ArrayList<string>[] arrays, string filename, bool isAppend)
        {
            var @Global = string.Concat(Application.persistentDataPath, "/ab/", floders[select].text, '/', filename);

            if (!isAppend && File.Exists(@Global)) File.WriteAllText(@Global, string.Empty); //如果是False 则清空

            using (var sw = !isAppend ? new StreamWriter(new FileStream(@Global, FileMode.OpenOrCreate, FileAccess.ReadWrite)) : new StreamWriter(@Global, true))
            {
                var title = string.Concat("开始写入", floders[select].text);
                var len = arrays[0].count;
                var error = new ArrayList<string>();
                var content = "";
                for (int i = 0; i < len; i++) //写入字符串。
                {
                    content = string.Concat("xlua.hotfix(", arrays[0].get(i), arrays[1].get(i), "nil)");
                    if (arrays[1].get(i).Contains("function")) error.add(content);
                    else sw.WriteLine(content);
                    EditorUtility.DisplayProgressBar(title, arrays[0].get(i), Mathf.CeilToInt(i / len));
                }
                if (error.count > 0)
                {
                    sw.WriteLine("-- [[ ERROR LIST : ]]--");
                    title = "开始检查错误格式";
                    for (int i = 0; i < error.count; i++) //写入字符串。
                    {
                        sw.WriteLine(error.get(i));
                        EditorUtility.DisplayProgressBar(title, error.get(i), Mathf.CeilToInt(i / error.count));
                    }
                    EditorUtility.DisplayDialog("Error", string.Concat("Format Detection Error Count : ", error.count), "Enter");
                }
                else EditorUtility.DisplayDialog("Hint", string.Concat("File Output Localtion : ", @Global), "Enter");
                sw.Flush();
                sw.Close();
            }
            EditorUtility.ClearProgressBar();
        }

        /** 检查标签格式，格式如：[xxx]，符合返回标签名，否则返回空 */
        private string checkToken(string str)
        {
            int sindex = str.IndexOf('[');
            if (sindex > 0) throw new SystemException("Malformed target format, " + str);
            int eindex = str.IndexOf(']');
            if (sindex == -1)
            {
                if (eindex != -1) throw new SystemException("Malformed target format, " + str);
                return null;
            }
            if (eindex == 1 || eindex != str.Length - 1) throw new SystemException("Malformed target format, " + str);
            return str.Substring(1, str.Length - 2);
        }

        private ArrayList<string>[] WirteXluaHotfixFile(string[] AllPath)
        {
            var keys = new ArrayList<string>();
            var values = new ArrayList<string>();
            StreamReader reader = null;
            string line, key, value, path;
            int index, second, end;
            bool read = false;
            var Global = string.Concat(Application.persistentDataPath, "/ab/");
            for (int i = 0; i < AllPath.Length; i++)
            {
                path = string.Concat(Global, AllPath[i]);
                if (!File.Exists(path))
                {
                    Debug.Log(string.Concat("<color=#FF0000> This File Not Found ！！！ Name = ", path, "</color>")); continue;
                }
                using (reader = new StreamReader(new FileStream(path, FileMode.Open)))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line == "--[[") read = true;
                        else if (line == "]]--") read = false;
                        if (read) continue; //如果是注释块 则跳过收集
                        if (line.Contains("=")) continue;
                        if (line.Contains("---@")) continue;
                        if (line.Contains("hotfix"))
                        {
                            if (line.Contains("(CS."))
                            {
                                index = line.IndexOf('(') + 1;
                                second = line.IndexOf('"');
                                end = line.IndexOf(',', second);
                                key = line.Substring(index, second - index);
                                value = line.Substring(second, end - second + 1);
                                keys.add(key); values.add(value);
                            }
                            else
                            {
                                key = reader.ReadLine().Replace("    ", "");         // CS.文件夹.文件夹.文件夹.类名
                                value = reader.ReadLine().Replace("    ", "");       // 方法名
                                reader.ReadLine(); reader.ReadLine(); reader.ReadLine(); //至少三行不需要判断
                                keys.add(key); values.add(value);
                            }
                        }
                    }
                }
            }
            return new ArrayList<string>[2] { keys, values };
        }

        protected override void onEnable()
        {
            var fd = new DirectoryInfo(string.Concat(Application.persistentDataPath, "/ab/")).GetDirectories();
            var t = new ArrayList<GUIContent>();
            foreach (var item in fd) if (item.Name.Contains("cfg")) t.add(new GUIContent(item.Name));
            floders = t.toArray();
        }

        protected override void change()
        {

        }

        protected override void close()
        {
            window = null;
        }
    }
}