using System;
using XLua;

namespace cambrian.common
{
    /// <summary>
    /// 类说明：二分插入排序列表
    /// </summary>
    public class InsertArrayList<T>
    {
        /// <summary>默认初始容量 </summary>
        public const int CAPATITY = 10;

        /// <summary>当前数量 </summary>
        private int size;
        /// <summary>列表数组 </summary>
        private T[] array;
        /// <summary>比较器 </summary>
        private Func<T,T,int> comparator;

        public int count
        {
            get { return this.size; }
        }

        public InsertArrayList(Func<T, T,int> comparator)
            : this(comparator, CAPATITY)
        {

        }

        public InsertArrayList(Func<T, T,int> comparator, int capatity)
        {
            if (comparator == null) throw new SystemException("InsertArrayList <init>, null comparator");
            this.comparator = comparator;
            if (capatity < 1) throw new SystemException("InsertArrayList <init>, invalid capatity:" + capatity);
            this.array = new T[capatity];
        }

        public void setCapacity(int value)
        {
            int i = this.array.Length;
            if (value <= i) return;
            for (; i < value; i = (i << 1) + 1)
                ;
            T[] _array = new T[i];
            Array.Copy(this.array, 0, _array, 0, this.size);
            this.array = _array;
        }

        public int capacity()
        {
            return this.array.Length;
        }

        public bool isEmpty()
        {
            return (this.size <= 0);
        }

        public bool isFull()
        {
            return false;
        }

        public T[] getArray()
        {
            return this.array;
        }

        public T get()
        {
            return this.getFirst();
        }

        public T get(int index)
        {
            return this.array[index];
        }

        public T getFirst()
        {
            if (this.size == 0) return default(T);
            return this.array[0];
        }

        public T getLast()
        {
            if (this.size == 0) return default(T);
            return this.array[(this.size - 1)];
        }

        /// <summary>是否包含指定元素 </summary>
        public bool contain(T obj)
        {
            return (this.indexOf(obj, 0) >= 0);
        }

        /// <summary>检索指定元素的索引 </summary>
        public int indexOf(T obj)
        {
            return this.indexOf(obj, 0);
        }

        /// <summary>检索指定元素的索引，从index开始向后查找 </summary>
        public int indexOf(T obj, int index)
        {
            if (index >= this.size) return -1;
            int j;
            for (j = index; j < this.size; ++j)
            {
                if (obj == null)
                {
                    if (this.array[j] == null) return j;
                }
                else
                {
                    if (obj.Equals(this.array[j])) return j;
                }
            }
            return -1;
        }

        /// <summary>检索指定元素索引，反向检索 </summary>
        public int lastIndexOf(T obj)
        {
            return this.lastIndexOf(obj, this.size - 1);
        }

        /// <summary>检索指定元素索引，从index开始检索反向检索 </summary>
        public int lastIndexOf(T obj, int index)
        {
            if (index >= this.size) return -1;
            int i;
            for (i = index; i >= 0; --i)
            {
                if (obj == null)
                {
                    if (this.array[i] == null) return i;
                }
                else
                {
                    if (obj.Equals(this.array[i])) return i;
                }
            }
            return -1;
        }

        /// <summary>添加 </summary>
        public bool add(T obj)
        {
            if (this.size >= this.array.Length) this.setCapacity(this.size + 1);
            int index = this.find(0, this.size - 1, obj);//二分查找插入点
            if (index < this.size) Array.Copy(this.array, index, this.array, index + 1, this.size - index);
            this.array[index] = obj;
            this.size++;
            return true;
        }

        /// <summary>在指定起始索引之间查找插入点索引（包含start和end） </summary>
        private int find(int start, int end, T obj)
        {
            if (start > end) return 0;
            int mid = (end + start) >>1;
            int r = this.comparator.Invoke(this.array[mid], obj);
            if (r == Comparator.COMP_GT || r == Comparator.COMP_EQ)
            {
                if (mid == end)
                {
                    return mid + 1;
                }
                else if (mid + 1 == end)
                {
                    r = this.comparator.Invoke(this.array[end], obj);
                    if (r == Comparator.COMP_GT || r == Comparator.COMP_EQ)
                        return end + 1;
                    else
                        return end;
                }
                else
                    return this.find(mid + 1, end, obj);
            }
            else
            {
                if (mid == start)
                {
                    return start;
                }
                else if (mid - 1 == start)
                {
                    r = this.comparator.Invoke(this.array[start], obj);
                    if (r == Comparator.COMP_GT || r == Comparator.COMP_EQ)
                        return mid;
                    else
                        return start;
                }
                else
                    return this.find(start, mid - 1, obj);
            }
        }

        /// <summary>移除最后一个 </summary>
        public T remove()
        {
            return this.remove(this.size - 1);
        }

        /// <summary>移除：该操作发生数组拷贝，顺序不变 </summary>
        public bool remove(T obj)
        {
            int i = this.indexOf(obj, 0);
            if (i < 0) return false;
            remove(i);
            return true;
        }

        /// <summary>移除：该操作发生数组拷贝，顺序不变 </summary>
        public T remove(int index)
        {
            if (index >= this.size) throw new SystemException("InsertArrayList remove, invalid index=" + index);
            T obj = this.array[index];
            int i = this.size - index - 1;
            if (i > 0) Array.Copy(this.array, index + 1, this.array, index, i);
            this.array[--this.size] = default(T);
            return obj;
        }

        public void clear()
        {
            this.size = 0;
            for (int i = 0; i < this.array.Length; i++)
            {
                this.array[i] = default(T);
            }
        }

        public T[] toArray()
        {
            T[] array = new T[this.size];
            Array.Copy(this.array, 0, array, 0, this.size);
            return array;
        }

        public int toArray(T[] array)
        {
            int len = (array.Length > this.size) ? this.size : array.Length;
            Array.Copy(this.array, 0, array, 0, len);
            return len;
        }

        public LuaTable toLuaTable()
        {
            if (count == 0) return null;
            LuaTable table = LuaUtil.luaEnv.NewTable();
            for (int i = 0; i < count; i++)
            {
                table.Set(i, array[i]);
            }
            return table;
        }


        public override string ToString()
        {
            return base.ToString() + "[size=" + this.size + ", capacity=" + this.array.Length + "]";
        }
    }
}
