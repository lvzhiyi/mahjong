using System;

namespace cambrian.common
{
    /**
   * 类说明：数组工具
   * 
   * @author HYZ [huangyz1988@qq.com]
   * @version 2018年2月7日 上午8:29:37
   */
    public sealed class ArrayKit
    {
        /**
         * 指定数组是否是空的
         * 
         * @param array
         * @return 当数组为NULL或长度为0时返回TRUE
         */
        public static bool isEmpty<T>(T[] array)
        {
            return (array == null || array.Length == 0);
        }

        /**
         * 计算数组元素值总和
         * 
         * @param array 待计算数组
         * @return 元素值总和
         */
        public static int sum(int[] array)
        {
            int total = 0;
            for (int i = 0; i < array.Length; i++)
            {
                total += array[i];
            }

            return total;
        }

        /**
         * 判断指定对象是否是基本类型数组
         * <p>
         * 注意：int[][]不是基本类型数组，而是引用类型数组
         * 
         * @param obj 对象
         * @return true为基本类型数组
         */
        public static bool isPrimitiveArray(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType().IsArray)
            {
            }

            return false;
        }

        /**
         * 指定数据中是否包含指定对象，如果对象为NULL则返回数组中第一个NULL元素的索引
         * 
         * @param array 源数组
         * @param obj 查找对象
         * @return
         */
        public static int indexOf<T>(T[] array, T obj)
        {
            if (obj != null)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (obj.Equals(array[i])) return i;
                }
            }
            else
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] == null) return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 是否存在重复的
        /// </summary>
        /// <param name="array">
        /// @return </param>
        public static bool existRepeat(int[] array)
        {
            for (int i = array.Length - 1; i > -1; --i)
            {
                for (int j = 0; j < i; ++j)
                {
                    if (array[i] == array[j]) return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 是否存在重复的
        /// </summary>
        /// <param name="array">
        /// @return </param>
        public static bool existRepeat(long[] array)
        {
            for (int i = array.Length - 1; i > -1; --i)
            {
                for (int j = 0; j < i; ++j)
                {
                    if (array[i] == array[j]) return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 是否存在重复的
        /// </summary>
        /// <param name="array"> </param>
        /// <param name="action"> 判断是否是同一个
        /// @return </param>
        public static bool existRepeat<T>(T[] array, Func<T, T, bool> action)
        {
            for (int i = array.Length - 1; i > -1; --i)
            {
                for (int j = 0; j < i; ++j)
                {
                    if (action(array[i], array[i])) return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="original"> </param>
        /// <param name="value">
        /// @return </param>
        public static T[] add<T>(T[] original, T value)
        {
            T[] copy = new T[original.Length + 1];
            Array.Copy(original, 0, copy, 0, original.Length);
            copy[original.Length] = value;
            return copy;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="original"> </param>
        /// <param name="values">
        /// @return </param>
        public static T[] add<T>(T[] original, T[] values)
        {
            T[] copy = new T[original.Length + values.Length];
            if (original.Length > 0) Array.Copy(original, 0, copy, 0, original.Length);
            if (values.Length > 0) Array.Copy(values, 0, copy, original.Length, values.Length);
            return copy;
        }

        /// <summary>
        /// 移除指定数组的指定位置元素
        /// </summary>
        /// <param name="src">数组</param>
        /// <param name="index">位置索引</param>
        public static T[] remove<T>(T[] src, int index)
        {
            int len = src.Length - 1;
            T[] dst = new T[len];
            if (len > 0)
            {
                if (index > 0) Array.Copy(src, 0, dst, 0, index);
                if (index < dst.Length) Array.Copy(src, index + 1, dst, index, dst.Length - index);
            }

            return dst;
        }

        /**
         * 旋转数组：将数组头尾相接，当作成一个圆环，然后所有元素顺序向左旋转指定偏移位置量
         * 
         * @param array 源数组
         * @param offset 偏移位置量
         */
        public static void rotateLeft<T>(T[] array, int offset)
        {
            if (array.Length == 0) return;
            int m = offset % array.Length;
            if (m == 0) return;
            int n = array.Length - m, x = 0, y = m;
            T temp;
            while (m != n)
            {
                if (m < n)
                {
                    for (int i = x + m; x < i; x++, y++)
                    {
                        temp = array[y];
                        array[y] = array[x];
                        array[x] = temp;
                    }

                    m = y - x;
                    n = array.Length - y;
                }
                else
                {
                    for (int i = x + n; x < i; x++, y++)
                    {
                        temp = array[y];
                        array[y] = array[x];
                        array[x] = temp;
                    }

                    y = array.Length - n;
                    m = y - x;
                }
            }

            if (x < y)
            {
                for (int i = y; x < i; x++, y++)
                {
                    temp = array[y];
                    array[y] = array[x];
                    array[x] = temp;
                }
            }
        }

        /**
         * 旋转数组：将数组头尾相接，当作成一个圆环，然后所有元素顺序向右旋转指定偏移位置量
         * 
         * @param array 源数组
         * @param offset 偏移位置量
         */
        public static void rotateRight<T>(T[] array, int offset)
        {
            if (offset == 0 || array.Length < 2) return;
            int m = offset % array.Length;
            if (m != 0) rotateLeft(array, array.Length - m);
        }

        /// <summary>
        /// 将一组对象作为数组返回
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="objs">一系列元素</param>
        /// <returns>数组</returns>
        public static T[] asArray<T>(params T[] objs)
        {
            return objs;
        }

        /// <summary>
        /// 返回一个确保有指定容量得数组，如果源数组已满足则直接返回
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="original">源数组</param>
        /// <param name="capacity">新容量</param>
        /// <returns></returns>
        public static T[] ensureCapacity<T>(T[] original, int capacity)
        {
            if (capacity < original.Length) return original;
            T[] array = new T[capacity];
            Array.Copy(original, 0, array, 0, original.Length);
            return array;
        }

        /**
         * 打乱一个数据元素
         * 
         * @param array 数组
         */
        public static void shuffle<T>(T[] array)
        {
            T t;
            int x, y;
            for (int i = array.Length; i > 1; i--)
            {
                x = i - 1;
                y = MathKit.random(0, i);
                t = array[x];
                array[x] = array[y];
                array[y] = t;
            }
        }

        /** 禁止构建 */
        private ArrayKit()
        {
            throw new SystemException("Instantiation is not allowed");
        }
    }
}