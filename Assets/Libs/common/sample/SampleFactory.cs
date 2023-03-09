using System;
using System.Reflection;
using System.Xml;
using UnityEngine;

namespace cambrian.common
{
    /// <summary>
    /// 样本工厂
    /// </summary>
    public class SampleFactory
    {

        /* static fields */
        /** 日志记录 */
        private static Logger log = LogFactory.getLogger<SampleFactory>(false);

        /** 空间大小 */
        private const int COUNT = 0xffff;
        /* properties */
        /** 条目数组 */
        Entry[] array;
        /* static methods */

        /* fields */

        /* constructors */

        public SampleFactory()
        {
            this.array = new Entry[COUNT + 1];
        }

        /* properties */

        /* init start */

        public void init(XmlNode xml)
        {

            foreach (XmlNode xmlDoc in xml.ChildNodes)
            {
                if (xmlDoc.GetType() == typeof (XmlElement) && xmlDoc.LocalName == "sample")
                {
                    XmlAttributeCollection atts = xmlDoc.Attributes;
                    string clazz = null;
                    for (int m = 0; m < atts.Count; m++)
                    {
                        XmlNode item = atts.Item(m);
                        if (item.Name == "class")
                        {
                            clazz = item.Value;
                            break;
                        }
                    }
                    if (log.isDebug)
                        Debug.Log(log.getMessage("initNodeObject," + xmlDoc + "," + clazz));
                    if (clazz == null)
                        throw new XmlException(" clazz null ");

                    Sample sample = (Sample) Assembly.GetExecutingAssembly().CreateInstance(clazz);
                    Type type = sample.GetType();
                    FieldInfo[] fields = FieldInfoKit.getFieldInfo(type);
                    MethodInfo[] methods = MethodInfoKit.getMethodInfo(type);
                    for (int m = 0; m < atts.Count; m++)
                    {
                        XmlNode item = atts.Item(m);
                        if (item.Name == "class")
                        {
                            if (item.Value != clazz)
                                throw new XmlException(item.Name + " not " + clazz);
                            continue;
                        }
                        if (item.Name.IndexOf('_') == 0)
                        {
                            if (item.Name.IndexOf('3') == 1)
                                FieldInfoKit.setField(sample, fields, item.Name.Substring(2),
                                    StringKit.parseStringsss(item.Value));
                            else if (item.Name.IndexOf('2') == 1)
                                FieldInfoKit.setField(sample, fields, item.Name.Substring(2),
                                    StringKit.parseStringss(item.Value));
                            else if (item.Name.IndexOf('1') == 1)
                                FieldInfoKit.setField(sample, fields, item.Name.Substring(2),
                                    StringKit.parseStrings(item.Value));
                            else if (item.Name.IndexOf('0') == 1)
                                FieldInfoKit.setField(sample, fields, item.Name.Substring(2),
                                    StringKit.parseInt(item.Value));
                            else if (item.Name.IndexOf('F') == 1 || item.Name.IndexOf('f') == 1)
                                continue;
                            else
                                throw new SystemException("error item.Name=" + item.Name);
                        }
                        else
                            FieldInfoKit.setField(sample, fields, item.Name, item.Value);
                    }
                    for (int m = 0; m < atts.Count; m++)
                    {
                        XmlNode item = atts.Item(m);
                        if (item.Name.IndexOf('_') == 0)
                        {
                            if (item.Name.IndexOf('F') == 1 || item.Name.IndexOf('f') == 1)
                                MethodInfoKit.setMethodField(sample, methods, item.Name.Substring(2), item.Value);
                        }
                    }
                    this.setSample(sample);
                }
            }
        }

        public void init(string clazz, XmlNode xml)
        {
            Sample sample = (Sample) Assembly.GetExecutingAssembly().CreateInstance(clazz);
            Type type = sample.GetType();
            if (log.isDebug)
                Debug.Log(log.getMessage("---------------------start---------------------------" + clazz));
            FieldInfo[] fields = FieldInfoKit.getFieldInfo(type);
            MethodInfo[] methods = MethodInfoKit.getMethodInfo(type);
            foreach (XmlNode xmlDoc in xml.ChildNodes)
            {
                sample = (Sample) Activator.CreateInstance(type);
                //if (log.isDebug)
                //    log.debug(xmlDoc);
                if (xmlDoc.GetType() == typeof (XmlElement) && xmlDoc.LocalName == "sample")
                {
                    //if (log.isDebug)
                    //    log.debug("initNodeObject," + xmlDoc + "," + clazz);
                    XmlAttributeCollection atts = xmlDoc.Attributes;
                    for (int m = 0; m < atts.Count; m++)
                    {
                        XmlNode item = atts.Item(m);
                        if (item.Name == "class")
                        {
                            if (item.Value != clazz)
                                throw new XmlException(item.Name + " not " + clazz);
                            continue;
                        }
                        if (item.Name.IndexOf('_') == 0)
                        {
                            if (item.Name.IndexOf('3') == 1)
                                FieldInfoKit.setField(sample, fields, item.Name.Substring(2),
                                    StringKit.parseStringsss(item.Value));
                            else if (item.Name.IndexOf('2') == 1)
                                FieldInfoKit.setField(sample, fields, item.Name.Substring(2),
                                    StringKit.parseStringss(item.Value));
                            else if (item.Name.IndexOf('1') == 1)
                                FieldInfoKit.setField(sample, fields, item.Name.Substring(2),
                                    StringKit.parseStrings(item.Value));
                            else if (item.Name.IndexOf('0') == 1)
                                FieldInfoKit.setField(sample, fields, item.Name.Substring(2),
                                    StringKit.parseInt(item.Value));
                            else if (item.Name.IndexOf('F') == 1 || item.Name.IndexOf('f') == 1)
                                continue;
                            else
                                throw new SystemException("error item.Name=" + item.Name);
                        }
                        else
                            FieldInfoKit.setField(sample, fields, item.Name, item.Value);
                    }
                    for (int m = 0; m < atts.Count; m++)
                    {
                        XmlNode item = atts.Item(m);
                        if (item.Name.IndexOf('_') == 0)
                        {
                            if (item.Name.IndexOf('F') == 1 || item.Name.IndexOf('f') == 1)
                                MethodInfoKit.setMethodField(sample, methods, item.Name.Substring(2), item.Value);
                        }
                    }
                    this.setSample(sample);
                }
            }
            //for (int i = 0; i < sampleArray.Length; i++)
            //{
            //    if (sampleArray[i] == null) continue;
            //    if (log.isDebug)
            //        log.debug(sampleArray[i]);
            //}
            //if (log.isDebug)
            //    log.debug("=======================end=========================" + clazz);
        }

        /** 全部赋值后，进行初始化和检查 */

        public void initAndCheck()
        {
            for (int i = 0; i < this.array.Length; i++)
            {
                if (this.array[i] == null) continue;
                Entry entry = this.array[i];
                while (entry != null)
                {
                    entry.value.init();
                    entry = entry.next;
                }
            }
            for (int i = 0; i < this.array.Length; i++)
            {
                if (this.array[i] == null) continue;
                Entry entry = this.array[i];
                while (entry != null)
                {
                    entry.value.relate();
#if UNITY_EDITOR
                    entry.value.check();
#endif
                    entry = entry.next;
                }
            }
        }

        /* methods */
        /** 获取样本 */

        public Sample getSample(int sid)
        {
            Entry entry = this.array[sid & COUNT];
            while (entry != null)
            {
                if (entry.value.sid == sid) return entry.value;
                entry = entry.next;
            }
            return null;
        }

        public Sample getSample(long sid)
        {
            return this.getSample((int) sid);
        }

        /** 设置模板对象 */

        public void setSample(Sample sample)
        {
            int i = sample.sid & COUNT;
            Entry entry = this.array[i];
            if (entry == null)
            {
                this.array[i] = new Entry(sample);
            }
            else
            {
                while (true)
                {
                    if (entry.value.sid == sample.sid)
                    {
                        throw new SystemException("sampleArray[" + sample.sid + "] value=" + entry.value);
//                        entry.value = sample;
//                        return;
                    }
                    if (entry.next == null) break;
                    entry = entry.next;
                }
                entry.next = new Entry(sample);
            }
        }

        /** 获取 */

        public Sample newSample(int sid)
        {
            Sample sample = getSample(sid);
            if (sample == null) return null;
            return ((Sample) sample.clone());
        }

        public Sample newSample(long sid)
        {
            return this.newSample((int) sid);
        }
        public T[] getSamples<T>() where T : Sample
        {
            cambrian.common.ArrayList<T> list = new cambrian.common.ArrayList<T>();
            for (int i = 0; i < this.array.Length; i++)
            {
                Entry entry = this.array[i];
                while (entry != null)
                {
                    if (entry.value is T) list.add((T) entry.value);
                    entry = entry.next;
                }
            }
            return list.toArray();
        }
        /* common methods */

        public void clear()
        {
            this.array = new Entry[COUNT + 1];
        }

        class Entry
        {
            internal Entry next;
            internal Sample value;

            internal Entry(Sample value)
            {
                this.value = value;
            }
        }
    }
}