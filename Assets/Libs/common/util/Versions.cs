using System;

namespace cambrian.common
{
    /// <summary>
    /// 版本号
    /// </summary>
    public class Versions
    {

        /** 排序比较返回常量：2大于，1大于等于，0等于，-1小于等于，-2小于 */
        public const int COMP_GT = 2, COMP_GE = 1, COMP_EQ = 0, COMP_LE = -1, COMP_LT = -2;



        /** 默认版本号：0.0.1 */
        public static int[] DEFAULT_VERSIONS = {0, 0, 1};

        /** 版本号 */
        protected int[] vers;
        /** versions */
        protected String versions;
        /** 版本描述 */
        protected String description="";

        public Versions()
        {
            this.vers = DEFAULT_VERSIONS;
        }

        public Versions(int ver)
        {
            this.vers = new int[] {ver};
        }

        public Versions(int v1, int v2)
        {
            this.vers = new int[] {v1, v2};
        }

        public Versions(int v1, int v2, int v3)
        {
            this.vers = new int[] {v1, v2, v3};
        }

        /** 以指定子版本序列构建版本 */

        public Versions(int[] vers)
        {
            if (vers == null || vers.Length == 0) new Exception("Format error, " + StringKit.toString(vers));
            this.vers = vers;
        }

        public Versions(String versions)
        {
            set(versions);
        }

        /** 获取指定子版本 */

        public int getVer(int index)
        {
            return this.vers[index];
        }

        /** 增加指定子版本号 */

        public void incrVer(int index)
        {
            this.vers[index]++;
            this.versions = null;
        }

        /** 设置版本数 */

        public void set(String versions)
        {
            int[] array = StringKit.parseInts(versions, '.');
            if (array == null || array.Length == 0) new Exception("Format error, " + versions);
            this.vers = array;
        }

        /** 获得版本号 */

        public String getVersion()
        {
            if (versions == null)
            {
                CharBuffer buffer = new CharBuffer();
                for (int i = 0; i < vers.Length; i++)
                {
                    if (i > 0) buffer.append('.');
                    buffer.append(vers[i]);
                }
                this.versions = buffer.getString();
            }
            return this.versions;
        }



        /** 比较两个子版本号 */

        protected int compare(int v1, int v2)
        {
            if (v1 == v2) return COMP_EQ;
            if (v1 > v2) return COMP_GT;
            return COMP_LT;
        }

        /**
         * 比较版本：返回当前版本和指定版本比较的结果
         * 
         * @param versions 目标版本
         * @return 比较结果值，负数小于，0等于，正数大于
         */

        public int compareTo(Versions versions)
        {
            if (versions == null) return 1;
            if (versions == this) return 0;
            int res = 0;
            if (vers.Length < versions.vers.Length)
            {
                for (int i = 0; i < vers.Length; i++)
                {
                    res = compare(vers[i], versions.vers[i]);
                    if (res != 0) return res;
                }
                return -1;
            }
            if (vers.Length > versions.vers.Length)
            {
                for (int i = 0; i < versions.vers.Length; i++)
                {
                    res = compare(vers[i], versions.vers[i]);
                    if (res != 0) return res;
                }
                return 1;
            }
            for (int i = 0; i < vers.Length; i++)
            {
                res = compare(vers[i], versions.vers[i]);
                if (res != 0) return res;
            }
            return 0;
        }


        public void bytesWrite(ByteBuffer data)
        {
            data.writeInt(this.vers.Length);
            for (int i = 0; i < vers.Length; i++)
            {
                data.writeInt(this.vers[i]);
            }
        }


        public void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            this.vers = new int[len];
            for (int i = 0; i < vers.Length; i++)
            {
                this.vers[i] = data.readInt();
            }
        }


        public override string ToString()
        {
            CharBuffer buf = new CharBuffer("Versions");
            buf.append('[').append(getVersion()).append(']').append(description);
            return buf.getString();
        }
    }
}
