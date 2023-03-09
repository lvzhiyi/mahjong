using System;
using UnityEngine;
using XLua;

namespace cambrian.common
{
    [Hotfix]
    public class ArrayList<T>
    {
        private const int CAPACITY = 10;
        T[] array;
        int size;
        /// <summary>
        /// 预留字段
        /// </summary>
        public int luck;
        /// <summary>
        /// 预留字段
        /// </summary>
        public int luckX;
        /// <summary>
        /// 预留字段
        /// </summary>
        public int luckY;
        /// <summary>
        /// 预留字段
        /// </summary>
        public long luckL;

        public ArrayList()
            : this(10)
        {
        }

        public ArrayList(int paramInt)
        {
            if (paramInt < 0)
                throw new SystemException("ArrayList <init>, invalid capatity:" + paramInt);
            this.array = new T[paramInt];
        }

        public ArrayList(T[] array)
            : this(array, (array != null) ? array.Length : 0)
        {
        }

        public ArrayList(T[] array, int paramInt)
        {
            if (array == null) this.array=new T[CAPACITY];
            else
            {
                if (paramInt > array.Length)
                    throw new SystemException("ArrayList <init>, invalid length:" + paramInt);
                this.array = array;
            }
            this.size = paramInt;
        }

        public int count
        {
            get
            {
                return this.size;
            }
        }

        public int getCount(T value)
        {
            int count = 0;
            for (int i = 0; i < size; i++)
            {
                if (value.Equals(array[i]))
                    count++;
            }

            return count;
        }

        public int capacity()
        {
            return this.array.Length;
        }

        public T[] getArray()
        {
            return this.array;
        }

        private void setCapacity(int paramInt)
        {
            T[] arrayOfObject1 = this.array;
            int i = arrayOfObject1.Length;
            if (paramInt <= i) return;
            for (; i < paramInt; i = (i << 1) + 1)
                ;
            T[] arrayOfObject2 = new T[i];
            Array.Copy(arrayOfObject1, 0, arrayOfObject2, 0, this.size);
            this.array = arrayOfObject2;
        }

        public T get(int paramInt)
        {
            return this.array[paramInt];
        }
        public T getLast()
        {
            if (this.size == 0) return default(T);
            return this.array[this.size-1];
        }
        public bool contain(T paramObject)
        {
            return (this.indexOf(paramObject, 0) >= 0);
        }

        public int indexOf(T paramObject)
        {
            return this.indexOf(paramObject, 0);
        }

        private int indexOf(T paramObject, int paramInt)
        {
            int i = this.size;
            if (paramInt >= i) return -1;
            T[] arrayOfObject = this.array;
            int j;
            if (paramObject == null)
            {
                for (j = paramInt; j < i; ++j)
                {
                    if (arrayOfObject[j] == null) return j;
                }

            }
            else
            {
                for (j = paramInt; j < i; ++j)
                {
                    if (paramObject.Equals(arrayOfObject[j])) return j;
                }
            }
            return -1;
        }

        public T set(T paramObject, int paramInt)
        {
            if (paramInt >= this.size)
                throw new SystemException("ArrayList set, invalid index=" + paramInt);
            T localObject = this.array[paramInt];
            this.array[paramInt] = paramObject;
            return localObject;
        }

        public bool add(T paramObject)
        {
            if (this.size >= this.array.Length) this.setCapacity(this.size + 1);
            this.array[(this.size++)] = paramObject;
            return true;
        }

        public bool add(T t,int count)
        {
            for (int i = 0; i < count; i++)
            {
                add(t);
            }

            return true;
        }


        public bool add(T[] paramObjects)
        {
            if (this.size + paramObjects.Length > this.array.Length) this.setCapacity(this.size + paramObjects.Length);
            for (int i = 0; i < paramObjects.Length; i++)
            {
                this.array[(this.size++)] = paramObjects[i];
            }
            return true;
        }
        /// <summary>
        /// 加到开头
        /// </summary>
        /// <param name="paramObjects"></param>
        public void addFirst(T[] paramObjects)
        {
            this.array = Arrays.add(paramObjects, this.array);
            this.size += paramObjects.Length;
        }
        public bool addAt(T paramObject, int paramInt)
        {
            if (this.size >= this.array.Length) this.setCapacity(this.size + 1);
            int i = this.size - paramInt;
            if (i > 0)
                Array.Copy(this.array, paramInt, this.array,
                    paramInt + 1, i);
            this.set(paramObject, paramInt);
            this.size++;
            return true;
        }
        public bool remove(T paramObject)
        {
            int i = this.indexOf(paramObject, 0);
            if (i < 0) return false;
            this.removeAt(i);
            return true;
        }

        public T removeAt(int paramInt)
        {
            if (paramInt >= this.size)
                throw new SystemException("ArrayList remove, invalid index=" + paramInt);
            T[] arrayOfObject = this.array;
            T localObject = arrayOfObject[paramInt];
            int i = this.size - paramInt - 1;
            if (i > 0)
                Array.Copy(arrayOfObject, paramInt + 1, arrayOfObject,
                    paramInt, i);
            arrayOfObject[(--this.size)] = default(T);
            return localObject;
        }
        /// <summary>
        /// 移除最后一个
        /// </summary>
        /// <param name="paramInt"></param>
        /// <returns></returns>
        public T removeLast()
        {
            if (this.size==0)
                throw new SystemException("ArrayList remove, invalid size=" + this.size);
            T localObject = this.array[this.size - 1];
            this.array[(--this.size)] = default(T);
            return localObject;
        }
        public T[] removeAll()
        {
            T[] arrayOfObject = new T[this.size];
            Array.Copy(this.array, 0, arrayOfObject, 0, this.size);
            this.clear();
            return arrayOfObject;
        }
        public void clear()
        {
            T[] arrayOfObject = this.array;
            for (int i = this.size - 1; i >= 0; --i)
                arrayOfObject[i] = default(T);
            this.size = 0;
            this.luck = -1;
            this.luckL = 0;
            this.luckX = 0;
            this.luckY = 0;
        }

        public T[] toArray()
        {
            T[] arrayOfObject = new T[this.size];
            Array.Copy(this.array, 0, arrayOfObject, 0, this.size);
            return arrayOfObject;
        }
    }
}
