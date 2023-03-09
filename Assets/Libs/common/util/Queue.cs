using System;

namespace cambrian.common
{
    /// <summary>
    /// 数组队列
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Queue<T>
    {
        protected T[] array;
        protected int head;
        protected int tail;
        protected int count;


        /// <summary>构建指定容量的队列</summary>
        public Queue(int capacity)
        {
            if (capacity < 1)
                throw new SystemException("Queue <init>, invalid capacity:" + capacity);
            this.array = new T[capacity];
            this.head = 0;
            this.tail = 0;
            this.count = 0;
        }

        /// <summary>队列数量</summary>
        public int size
        {
            get { return this.count; }
        }

        /// <summary>队列容量</summary>
        public int capacity
        {
            get { return this.array.Length; }
        }

        /// <summary>
        /// 剩余容量
        /// </summary>
        /// <returns></returns>
        public int remain
        {
            get
            {
                return this.capacity - this.size;
            }
        }

        /// <summary>队列是否为空</summary>
        public bool isEmpty()
        {
            return this.count <= 0;
        }

        /// <summary>队列是否已满</summary>
        public bool isFull()
        {
            return this.count >= this.array.Length;
        }

        /// <summary>指定元素在队列中的索引，如果队列不包含该元素返回-1</summary>
        public int indexOf(T t)
        {
            if (t != null)
            {
                for (int i = this.head, index = this.tail > this.head ? this.tail : this.array.Length; i < index; i++)
                    if (t.Equals(this.array[i])) return i - this.head;
                for (int i = 0, index = this.tail > this.head ? 0 : this.tail; i < index; i++)
                    if (t.Equals(this.array[i])) return i + this.count - this.tail;
            }
            else
            {
                for (int i = this.head, index = this.tail > this.head ? this.tail : this.array.Length; i < index; i++)
                    if (this.array[i] == null) return i - this.head;
                for (int i = 0, index = this.tail > this.head ? 0 : this.tail; i < index; i++)
                    if (this.array[i] == null) return i + this.count - this.tail;
            }
            return -1;
        }

        /// <summary>队列中是否包含指定元素</summary>
        public bool contain(T t)
        {
            return this.indexOf(t) != -1;
        }

        /// <summary>添加元素到队列</summary>
        public virtual bool add(T t)
        {
            if (this.isFull()) return false;
            this.array[this.tail++] = t;
            if (this.tail >= this.array.Length) this.tail = 0;
            this.count++;
            return true;
        }

        /// <summary>获取队列中第一个元素</summary>
        public T get()
        {
            return this.array[this.head];
        }

        /// <summary>获取队列中指定索引的元素</summary>
        public T get(int index)
        {
            if (index < 0 || index >= this.array.Length) throw new SystemException("index out of bounds:" + index);
            index = this.head + index;
            if (index >= this.array.Length) index = index - this.array.Length;
            return this.array[index];
        }

        /// <summary>从队列中移除第一个</summary>
        public T remove()
        {
            if (this.isEmpty()) return default(T);
            T t = this.array[this.head];
            this.array[this.head] = default(T);
            this.count--;
            if (this.count > 0)
            {
                this.head++;
                if (this.head >= this.array.Length) this.head = 0;
            }
            else
            {
                this.head = 0;
                this.tail = 0;
            }
            return t;
        }

        /// <summary>清空队列</summary>
        public void clear()
        {
            for (int i = this.head, index = this.tail > this.head ? this.tail : this.array.Length; i < index; i++)
                this.array[i] = default(T);
            for (int i = 0, index = this.tail > this.head ? 0 : this.tail; i < index; i++)
                this.array[i] = default(T);
            this.tail = 0;
            this.head = 0;
            this.count = 0;
        }

        /// <summary>转换队列为数组</summary>
        public T[] toArray()
        {
            T[] ts = new T[this.count];
            if (this.count == 0) return ts;
            Array.Copy(this.array, this.head, ts, 0, this.tail > this.head ? this.count : this.count - this.tail);
            if (this.tail <= this.head) Array.Copy(this.array, 0, ts, this.count - this.tail, this.tail);
            return ts;
        }

        public override string ToString()
        {
            return "Queue:[size=" + this.count + ", capacity=" + this.array.Length + "]";
        }
    }
}
