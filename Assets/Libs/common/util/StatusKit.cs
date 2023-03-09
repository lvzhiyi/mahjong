namespace cambrian.common
{
    public class StatusKit
    {

        /** 设置状态 */
        public static short setStatus(short source, short status, bool b)
        {
            if (b)
                source |= status;
            else
                source &= (short)((~status)&0xFFFF);
            return source;
        }

        /** 是否是指定状态（仅仅是指定状态） */
        public static bool isStatus(short source, short status)
        {
            return source == status;
        }

        /** 是否有指定状态（包含指定状态，但不限于指定状态） */
        public static bool hasStatus(short source, short status)
        {
            return (source & status) == status;
        }

        /** 设置状态 */
        public static int setStatus(int source, int status, bool b)
        {
            if (b)
                source |= status;
            else
                source &= (~status);
            return source;
        }

        /** 是否是指定状态（仅仅是指定状态） */
        public static bool isStatus(int source, int status)
        {
            return source == status;
        }

        /** 是否有指定状态（包含指定状态，但不限于指定状态） */
        public static bool hasStatus(int source, int status)
        {
            if (source < 0) return false;
            return (source & status) == status;
        }

        /** 设置状态 */
        public static long setStatus(long source, long status, bool b)
        {
            if (b)
                source |= status;
            else
                source &= (~status);
            return source;
        }

        /** 是否是指定状态（仅仅是指定状态） */
        public static bool isStatus(long source, long status)
        {
            return source == status;
        }

        /** 是否有指定状态（包含指定状态，但不限于指定状态） */
        public static bool hasStatus(long source, long status)
        {
            return (source & status) == status;
        }


        /**
	 * 删除状态
	 * 
	 * @param source 源状态
	 * @param status 操作状态
	 * @return 新状态
	 */
        public static byte del(byte source, byte status)
        {
            source = (byte)(source&(~status));
            return source;
        }
        /**
         * 删除状态
         * 
         * @param source 源状态
         * @param status 操作状态
         * @return 新状态
         */
        public static  short del(short source, short status)
        {
            source = (short)(source&(~status));
            return source;
        }
        /**
         * 删除状态
         * 
         * @param source 源状态
         * @param status 操作状态
         * @return 新状态
         */
        public static  int del(int source, int status)
        {
            source &= (~status);
            return source;
        }
        /**
         * 删除状态
         * 
         * @param source 源状态
         * @param status 操作状态
         * @return 新状态
         */
        public static  long del(long source, long status)
        {
            source &= (~status);
            return source;
        }

        /**
  * 源状态和指定状态是否有交集
  * 
  * @param source 源状态
  * @param status 操作状态
  * @return true有相交
  */
        public static  bool mix(byte source, byte status)
        {
            return (source & status) != 0;
        }
        /**
         * 源状态和指定状态是否有交集
         * 
         * @param source 源状态
         * @param status 操作状态
         * @return true有相交
         */
        public static  bool mix(short source, short status)
        {
            return (source & status) != 0;
        }
        /**
         * 源状态和指定状态是否有交集
         * 
         * @param source 源状态
         * @param status 操作状态
         * @return true有相交
         */
        public static  bool mix(int source, int status)
        {
            return (source & status) != 0;
        }
        /**
         * 源状态和指定状态是否有交集
         * 
         * @param source 源状态
         * @param status 状态
         * @return true有相交
         */
        public static  bool mix(long source, long status)
        {
            return (source & status) != 0;
        }

    }
}
