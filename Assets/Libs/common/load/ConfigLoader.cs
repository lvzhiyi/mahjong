using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace cambrian.common
{
    //配置表解析
    public class ConfigLoader
    {
        /** 默认注释标记符号 */
        public const char DEFAULT_IGNORE_TOKEN = '#';

        public Hashtable load(string filePath)
        {
            // HashTable 不允许key或value为空，否则抛出NullPointerException
            Hashtable cfgs = new Hashtable();
            load(filePath, cfgs);
            return cfgs;
        }

        public void load(string filePath, Hashtable cfgs)
        {
            if (!File.Exists(filePath)) throw new ArgumentException("Config file not found! " + filePath);
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(new FileStream(filePath, FileMode.Open));
                string line = null;
                string target = null; // 当前标签
                ArrayList<string> list = new ArrayList<string>();
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line.Length == 0 || line[0] == DEFAULT_IGNORE_TOKEN) continue;
                    string _target = checkToken(line);
                    if (_target != null)
                    {
                        if (target == null) // 第一次出现标签
                        {
                            target = _target;
                            list.clear();
                        }
                        else // 第二次出现标签
                        {

                            if (!String.Equals(target, _target, StringComparison.CurrentCultureIgnoreCase))
                                throw new SystemException("Config Error, The target [" + target + "] must he end ");
                            int size = list.count;
                            cfgs.Add(target, size > 0 ? list.toArray() : new string[0]);
                            target = null;
                        }
                    }
                    else // 标签内或标签外
                    {
                        if (target != null) // 处于标签内值
                        {
                            list.add(line);
                        }
                        else // 标签外单项配置
                        {
                            string[] entry = StringKit.splitOnce(line, '=');
                            cfgs.Add(entry[0], entry[1]);
                        }
                    }
                }

                if (target != null) throw new SystemException("Config Error, The target [" + target + "] must he end ");
            }
            catch (IOException e)
            {
                throw new SystemException("Config file load Error! file=" + filePath, e);
            }
            finally
            {
                try
                {
                    if (reader != null) reader.Close();
                }
                catch (IOException e)
                {
                }
            }
        }

        /** 解析 */
        public void parse(string context,Hashtable cfgs)
        {
            string[] lines = StringKit.splitLine(context);
            string target = null;// 当前标签
            ArrayList<string> list = new ArrayList<string>();
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Trim();
                if (lines[i].Length==0 || lines[i][0] == DEFAULT_IGNORE_TOKEN) continue;
                string _target = checkToken(lines[i]);
                if (_target != null)
                {
                    if (target == null)// 第一次出现标签
                    {
                        target = _target;
                        list.clear();
                    }
                    else// 第二次出现标签
                    {
                        if (!String.Equals(target, _target, StringComparison.CurrentCultureIgnoreCase))
                            throw new SystemException("Config Error, The target [" + target + "] must he end ");
                        int size = list.count;
                        cfgs.Add(target, size > 0 ? list.toArray() : new string[0]);
                        target = null;
                    }
                }
                else// 标签内或标签外
                {
                    if (target != null)// 处于标签内值
                    {
                        list.add(lines[i]);
                    }
                    else// 标签外单项配置
                    {
                        string[] entry = StringKit.splitOnce(lines[i], '=');
                        cfgs.Add(entry[0], entry[1]);
                    }
                }
            }
            if (target != null) throw new SystemException("Config Error, The target [" + target + "] must he end ");
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
    }
}
