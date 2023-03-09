using System;
namespace cambrian.common
{
    public class Arrays
    {
        /* static fields */

        /* static methods */
        /// <summary>
        /// 确认数组的容量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <param name="capacity"></param>
        /// <returns></returns>
        public static T[] ensureCapacity<T>(T[] original, int capacity)
        {
            if (capacity < original.Length) return original;
            return copyOf(original, capacity);
        }
        /// <summary>
        /// 扩充容量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <param name="newLength"></param>
        /// <returns></returns>
        public static T[] copyOf<T>(T[] original, int newLength)
        {
            T[] copy = new T[newLength]; //(T[])Array.CreateInstance(original.GetType().GetElementType(), newLength);
            Array.Copy(original, 0, copy, 0, original.Length);
            return copy;
        }
        /// <summary>
        /// 是否存在重复的
        /// </summary>
        /// <param name="array">
        /// @return </param>
        public static bool existRepeat(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    if (array[i] == array[j])
                    {
                        return true;
                    }
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
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    if (array[i] == array[j])
                    {
                        return true;
                    }
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
        public static bool existRepeat<T>(T[] array, System.Func<T, T, bool> action)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    if (action(array[i], array[j]))
                    {
                        return true;
                    }
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
            int newLength = original.Length + 1;
            T[] copy = new T[newLength]; //(T[])Array.CreateInstance(original.GetType().GetElementType(), newLength);
            Array.Copy(original, 0, copy, 0, original.Length);
            copy[newLength - 1] = value;
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
            int newLength = original.Length + values.Length;
            T[] copy = new T[newLength]; //(T[])Array.CreateInstance(original.GetType().GetElementType(), newLength);
            Array.Copy(original, 0, copy, 0, original.Length);
            Array.Copy(values, 0, copy, original.Length, values.Length);
            return copy;
        }
        /// <summary>
        /// 移除指定位置的
        /// </summary>
        /// <param name="original"> </param>
        /// <param name="index">
        /// @return </param>
        public static T[] remove<T>(T[] original, int index)
        {
            int newLength = original.Length - 1;
            //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
            //ORIGINAL LINE: @SuppressWarnings("unchecked") T[] copy=(T[])Array.newInstance(original.getClass().getComponentType(),newLength);
            T[] copy = new T[newLength]; // (T[])Array.CreateInstance(original.GetType().GetElementType(), newLength);
            Array.Copy(original, 0, copy, 0, index);
            Array.Copy(original, index + 1, copy, index, newLength - index);
            return copy;
        }
    }
}
