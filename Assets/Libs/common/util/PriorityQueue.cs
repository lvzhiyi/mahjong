using System;
using System.Collections.Generic;

namespace cambrian.common
{
    /// <summary>
    /// 优先级队列
    /// </summary>
    public class PriorityQueue<T>
    {
        IComparer<T> comparer;

        T[] heap;

        public int count { get; private set; }

        public PriorityQueue() : this(null)
        {
        }

        public PriorityQueue(int capacity) : this(capacity, null)
        {
        }

        public PriorityQueue(IComparer<T> comparer) : this(16, comparer)
        {
        }

        public PriorityQueue(int capacity, IComparer<T> comparer)
        {
            this.comparer = (comparer == null) ? Comparer<T>.Default : comparer;
            this.heap = new T[capacity];
        }

        public void push(T t)
        {
            if (this.count >= this.heap.Length) Array.Resize(ref this.heap, this.count*2);
            this.heap[this.count] = t;
            this.siftUp(this.count++);
        }

        public T pop()
        {
            T t = this.top();
            this.heap[0] = this.heap[--this.count];
            if (this.count > 0) this.siftDown(0);
            return t;
        }

        public T top()
        {
            if (this.count > 0) return this.heap[0];
            throw new InvalidOperationException("priorityQueue is null.");
        }

        void siftUp(int n)
        {
            T t = this.heap[n];
            for (int n2 = n/2; n > 0 && this.comparer.Compare(t, this.heap[n2]) > 0; n = n2, n2 /= 2) 
                this.heap[n] = this.heap[n2];
            this.heap[n] = t;
        }

        void siftDown(int n)
        {
            T t = this.heap[n];
            for (int n2 = n*2; n2 < this.count; n = n2, n2 *= 2)
            {
                if (n2 + 1 < this.count && this.comparer.Compare(this.heap[n2 + 1], this.heap[n2]) > 0) n2++;
                if (this.comparer.Compare(t, this.heap[n2]) >= 0) break;
                this.heap[n] = this.heap[n2];
            }
            this.heap[n] = t;
        }
    }
}
