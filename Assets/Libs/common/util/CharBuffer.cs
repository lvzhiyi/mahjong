using System;

namespace cambrian.common
{
    ///<summary>类说明：字符缓存</summary>
    public class CharBuffer
    {

        ///<summary>容量</summary>
        public const int CAPACITY = 32;

        ///<summary>空</summary>
        public const string NULL = "null";

        ///<summary>字符数组</summary>
        private char[] array;

        ///<summary>首位:下一个写入位置</summary>
        private int top;

        ///<summary>游标：下一个读取位置</summary>
        private int offset;

        ///<summary>构建一个默认的字符缓冲对象</summary>
        public CharBuffer() : this(CAPACITY)
        {
        }

        ///<summary>构建一个指定容量的字符缓冲对象</summary>
        public CharBuffer(int capacity)
        {
            if (capacity < 1)
                throw new SystemException(typeof (CharBuffer).Name + " <init>, invalid capatity:" + capacity);
            this.array = new char[capacity];
            this.top = 0;
            this.offset = 0;
        }

        ///<summary>通过一个字符数组构建一个字符缓冲对象</summary>
        public CharBuffer(char[] chars)
        {
            if (chars == null)
                throw new SystemException(typeof (CharBuffer).Name + " <init>, null data");
            this.array = chars;
            this.top = chars.Length;
            this.offset = 0;
        }

        ///<summary>通过一个字符数组，offset开始数量为len的区域构建一个字符缓冲</summary>
        public CharBuffer(char[] chars, int offset, int len)
        {
            if (chars == null)
                throw new SystemException(typeof (CharBuffer).Name + " <init>, null data");
            if ((offset < 0) || (offset > chars.Length))
                throw new SystemException(typeof (CharBuffer).Name + " <init>, invalid index:" + offset);
            if ((len < 0) || (chars.Length < offset + len))
                throw new SystemException(typeof (CharBuffer).Name + " <init>, invalid length:" + len);
            this.array = chars;
            this.top = (offset + len);
            this.offset = offset;
        }

        ///<summary>通过一个字符串构建一个字符缓冲对象</summary>
        public CharBuffer(string str)
        {
            if (str == null)
                throw new SystemException(typeof (CharBuffer).Name + " <init>, null str");
            int i = str.Length;
            this.array = new char[i + CAPACITY];
            str.CopyTo(0, this.array, 0, i);
            //str.GetChars(0,i,this.array,0);
            this.top = i;
            this.offset = 0;
        }

        ///<summary>容量</summary>
        public int capacity()
        {
            return this.array.Length;
        }

        ///<summary>设置容量</summary>
        public void ensureCapacity(int capacity)
        {
            int i = this.array.Length;
            if (capacity <= i) return;
            for (; i < capacity; i = (i << 1) + 1)
                ;
            char[] chars = new char[i];
            Array.Copy(this.array, 0, chars, 0, this.top);
            this.array = chars;
        }

        ///<summary>首位</summary>
        public int getTop()
        {
            return this.top;
        }

        ///<summary>设置首位</summary>
        public void setTop(int top)
        {
            if (top < this.offset) throw new SystemException(typeof (CharBuffer).Name + " setTop, invalid top:" + top);
            if (top > this.array.Length) this.ensureCapacity(top);
            this.top = top;
        }

        ///<summary>游标</summary>
        public int getOffset()
        {
            return this.offset;
        }

        ///<summary>设置游标</summary>
        public void setOffset(int offset)
        {
            if ((offset < 0) || (offset > this.top))
                throw new SystemException(typeof (CharBuffer).Name + " setOffset, invalid offset:" + offset);
            this.offset = offset;
        }

        /**
	 * 移动写入索引
	 * 
	 * @param v 移动参数，负数左移，正数右移，数值为偏移量
	 * @return 缓冲区本身
	 */
        public CharBuffer moveTop(int value)
        {
            value += top;
            if (value < offset) throw new IndexOutOfRangeException((value - offset).ToString());
            //ensureCapacity(value);
            ensureCapacity(value);
            top = value;
            return this;
        }

        /** 移动读取游标，负数左移，正数右移，数值为偏移量 */
        public CharBuffer moveOffset(int value)
        {
            value += offset;
            if (value < 0) throw new IndexOutOfRangeException(value.ToString());
            if (value > top) throw new IndexOutOfRangeException((value - offset).ToString());
            offset = value;
            return this;
        }

        ///<summary>长度</summary>
        public int length()
        {
            return this.top - this.offset;
        }

        ///<summary>获取字符数组</summary>
        public char[] getArray()
        {
            return this.array;
        }

        ///<summary>读取一个字符</summary>
        public char read()
        {
            return this.array[this.offset++];
        }

        ///<summary>读取指定下标字符</summary>
        public char read(int index)
        {
            return this.array[index];
        }

        ///<summary>写入一个字符</summary>
        public void write(char ch)
        {
            this.array[this.top++] = ch;
        }

        ///<summary>将字符写到指定下标</summary>
        public void write(char ch, int index)
        {
            this.array[index] = ch;
        }

        ///<summary>读取len个字符到数组chars中，从下标index开始</summary>
        public void read(char[] chars, int index, int len)
        {
            Array.Copy(this.array, this.offset, chars, index, len);
            this.offset += len;
        }

        ///<summary>写入一个字符数组，冲下标offset开始，数量len</summary>
        public void write(char[] chars, int offset, int len)
        {
            if (this.array.Length < this.top + len) this.ensureCapacity(this.top + len);
            Array.Copy(chars, offset, this.array, this.top, len);
            this.top += len;
        }

        ///<summary>附加一个对象</summary>
        public CharBuffer append(object obj)
        {
            return append((obj != null) ? obj.ToString() : null);
        }

        ///<summary>附加一个字符串</summary>
        public CharBuffer append(string str)
        {
            if (str == null) str = "null";
            int len = str.Length;
            if (len <= 0) return this;
            if (this.array.Length < this.top + len) this.ensureCapacity(this.top + len);
            str.CopyTo(0, this.array, this.top, len);
            this.top += len;
            return this;
        }

        ///<summary>附加一个字符串,从字符串start索引开始添加len个字符</summary>
        public CharBuffer append(string str,int start,int len)
        {
            if (len <= 0) return this;
            if (this.array.Length < this.top + len) this.ensureCapacity(this.top + len);
            str.CopyTo(start, this.array, this.top, len);
            this.top += len;
            return this;
        }

        ///<summary>附加一个字符串,从字符串start索引开始添加len个字符</summary>
        public CharBuffer append(string str, int start)
        {
            int len = str.Length - start;
            if (len <= 0) return this;
            if (this.array.Length < this.top + len) this.ensureCapacity(this.top + len);
            str.CopyTo(start, this.array, this.top, len);
            this.top += len;
            return this;
        }

        ///<summary>附加一组字符</summary>
        public CharBuffer append(char[] chars)
        {
            if (chars == null) return this.append("null");
            return append(chars, 0, chars.Length);
        }

        ///<summary>附加一组字符，冲指定下标offset开始，附加len数量个字符</summary>
        public CharBuffer append(char[] chars, int offset, int len)
        {
            if (chars == null) return this.append("null");
            this.write(chars, offset, len);
            return this;
        }

        ///<summary>附加一个boolean</summary>
        public CharBuffer append(bool bo)
        {
            int i = this.top;
            if (bo)
            {
                if (this.array.Length < i + 4) this.ensureCapacity(i + CAPACITY);
                this.array[i] = 't';
                this.array[i + 1] = 'r';
                this.array[i + 2] = 'u';
                this.array[i + 3] = 'e';
                this.top += 4;
            }
            else
            {
                if (this.array.Length < i + 5) this.ensureCapacity(i + CAPACITY);
                this.array[i] = 'f';
                this.array[i + 1] = 'a';
                this.array[i + 2] = 'l';
                this.array[i + 3] = 's';
                this.array[i + 4] = 'e';
                this.top += 5;
            }
            return this;
        }

        ///<summary>附加一个char</summary>
        public CharBuffer append(char chr)
        {
            if (this.array.Length < this.top + 1) this.ensureCapacity(this.top + CAPACITY);
            this.array[this.top++] = chr;
            return this;
        }

        ///<summary>附加一个int</summary>
        public CharBuffer append(int number)
        {
            if (number == int.MinValue)
            {
                // 这个转正数超过最大值，所以要单独处理
                this.append("-2147483648");
                return this;
            }
            int i = this.top, j = 0, k = 0, l;
            if (number < 0)
            {
                number = -number;
                for (l = number; (l /= 10) > 0; ++k)
                    ;
                j = k + 2;
                if (this.array.Length < i + j) this.ensureCapacity(i + j);
                this.array[i++] = '-';
            }
            else
            {
                for (l = number; (l /= 10) > 0; ++k)
                    ;
                j = k + 1;
                if (this.array.Length < i + j) this.ensureCapacity(i + j);
            }
            while (k >= 0)
            {
                this.array[(i + k)] = (char) ('0' + number%10);
                number /= 10;
                --k;
            }
            this.top += j;
            return this;
        }

        ///<summary>附加一个long</summary>
        public CharBuffer append(long number)
        {
            if (number == long.MinValue)
            {
                this.append("-9223372036854775808");
                return this;
            }
            int i = this.top, j = 0, k = 0;
            long l;
            if (number < 0L)
            {
                number = -number;
                for (l = number; (l /= 10L) > 0L; ++k)
                    ;
                j = k + 2;
                if (this.array.Length < i + j) this.ensureCapacity(i + j);
                this.array[i++] = '-';
            }
            else
            {
                for (l = number; (l /= 10L) > 0L; ++k)
                    ;
                j = k + 1;
                if (this.array.Length < i + j) this.ensureCapacity(i + j);
            }
            while (k >= 0)
            {
                this.array[(i + k)] = (char) (int) ('0' + number%10L);
                number /= 10L;
                --k;
            }
            this.top += j;
            return this;
        }

        ///<summary>附加一个float</summary>
        public CharBuffer append(float number)
        {
            return append(number.ToString());
        }

        ///<summary>附加一个double</summary>
        public CharBuffer append(double number)
        {
            return append(number.ToString());
        }

        ///<summary>转换为字符数组</summary>
        public char[] toArray()
        {
            char[] chars = new char[this.top - this.offset];
            Array.Copy(this.array, this.offset, chars, 0, chars.Length);
            return chars;
        }

        ///<summary>清除</summary>
        public void clear()
        {
            this.top = 0;
            this.offset = 0;
        }

        ///<summary>获取字符串</summary>
        public string getString()
        {
            return new String(this.array, this.offset, this.top - this.offset);
        }

        ///<summary>获取字符串</summary>
        public string getString(int sindex,int len)
        {
            if (len > this.top - this.offset) len = this.top - this.offset;
            return new String(this.array, this.offset+sindex, len);
        }

        ///<summary>hash码</summary>
        public override int GetHashCode()
        {
            int code = 0;
            for (int i = this.offset; i < this.top; ++i)
                code = 31*code + this.array[i];
            return code;
        }

        ///<summary>比对方法</summary>
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (!(obj is CharBuffer)) return false;
            CharBuffer charBuffer = (CharBuffer) obj;
            if (charBuffer.top != this.top) return false;
            if (charBuffer.offset != this.offset) return false;
            for (int i = this.top - 1; i >= 0; --i)
            {
                if (charBuffer.array[i] != this.array[i]) return false;
            }
            return true;
        }

        ///<summary>信息</summary>
        public override string ToString()
        {
            return base.ToString() + "[" + this.top + "," + this.offset + "," + this.array.Length + "]";
        }
    }
}