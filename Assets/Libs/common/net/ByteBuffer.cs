using System;
using System.Text;

namespace cambrian.common
{
    /**
     * 类说明：字节流缓冲，最大缓存400KB<br>
     * 提供writeXXX，readXXX，setXXX,getXXX方法<br>
     * <li>writeXXX： 将XXX写入字节缓存，改变写入进度
     * <li>readXXX：从字节缓存中读取XXX，改变读取进度
     * <li>setXXX：需要传入索引，在字节缓存的指定索引处写入一个XXX，不影响缓存读写进度
     * <li>getXXX：需要传入索引，在字节缓存的指定索引处读取一个XXX，不影响缓存读写进度
     * 
     * @author HYZ <huangyz1988@qq.com>
     * @create 2017年2月20日上午5:51:00
     * @version 1.0
     */
    public class ByteBuffer
    {

        /** 默认容量：32B = 256bit */
        public const int CAPACITY = 32;
        /** 最大缓存：400KB */
        public const int MAX_DATA_LENGTH = 400 * 1024;

        /** 缓存写入进度 */
        protected int top;
        /** 缓存读取进度 */
        protected int offset;
        /** 数据缓存 */
        protected byte[] array;

        /** 构建一个默认容量的ByteBuffer */
        public ByteBuffer()
            : this(CAPACITY)
        {
        }

        /** 构建一个指定容量的ByteBuffer */
        public ByteBuffer(int capacity)
        {
            if (capacity < 1) throw new SystemException("<init>, invalid capatity:" + capacity);
            this.top = 0;
            this.offset = 0;
            this.array = new byte[capacity];
        }

        /** 构建一个指定数据的ByteBuffer */
        public ByteBuffer(byte[] bytes)
        {
            if (bytes == null) throw new SystemException("<init>, null data");
            this.top = bytes.Length;
            this.offset = 0;
            this.array = bytes;
        }

        /** 容量 */
        public int capacity()
        {
            return this.array.Length;
        }

        /** 设置指定容量 */
        public void setCapacity(int capacity)
        {
            int c = this.array.Length;
            if (capacity > c)
            {
                // 每次扩大一倍（+1是防止i==0导致死循环）
                while (c < capacity)
                    c = (c << 1) + 1;
                byte[] newArray = new byte[c];
                Array.Copy(this.array, 0, newArray, 0, this.top);
                this.array = newArray;
            }
        }

        /** 当前首位 */
        public int getTop()
        {
            return this.top;
        }

        /** 设置当前首位 */
        public void setTop(int top)
        {
            if (top < this.offset) throw new SystemException("setTop, invalid top:" + top);
            if (top > this.array.Length) setCapacity(top);
            this.top = top;
        }

        /** 当前读取游标 */
        public int getOffset()
        {
            return this.offset;
        }

        /** 当前读取游标 */
        public virtual void setOffset(int offset)
        {
            if (offset < 0 || offset > this.top) throw new SystemException("setOffset, invalid offset:" + offset);
            this.offset = offset;
        }

        /** 返回数据可读取长度 */
        public int length
        {
            get { return this.top - this.offset; }
        }

        /** 获得数据数组 */
        public byte[] getArray()
        {
            return this.array;
        }

        /** 在指定位置读取一个boolean，不改变读取进度 */
        public bool getBoolean(int index)
        {
            return this.array[index] != 0;
        }

        /** 读取一个布尔值 */
        public bool readBoolean()
        {
            return this.array[this.offset++] != 0;
        }

        /** 在指定位置读取一个byte，不改变读取进度 */
        public byte getByte(int index)
        {
            return this.array[index];
        }

        /** 读取一个字节 */
        public byte readByte()
        {
            return this.array[this.offset++];
        }

        /** 在指定位置读取一个无符号byte，不改变读取进度 */
        public int getUnsignedByte(int index)
        {
            return this.array[index] & 0xFF;
        }

        /** 读取一个无符号byte */
        public int readUnsignedByte()
        {
            return this.array[this.offset++] & 0xFF;
        }

        /** 在指定位置读取一个short，不改变读取进度 */
        public short getShort(int index)
        {
            return (short)getUnsignedShort(index);
        }

        /** 读取一个有符号的short */
        public short readShort()
        {
            return (short)readUnsignedShort();
        }

        /** 在指定位置读取一个无符号short，不改变读取进度 */
        public int getUnsignedShort(int index)
        {
            return ((this.array[index] & 0xFF) << 8) | (this.array[index + 1] & 0xFF);
        }

        /** 读取一个无符号short */
        public int readUnsignedShort()
        {
            int pos = this.offset;
            this.offset += 2;
            return ((this.array[pos] & 0xFF) << 8) | (this.array[pos + 1] & 0xFF);
        }

        /** 在指定位置读取一个int，不改变读取进度 */
        public int getInt(int index)
        {
            return ((this.array[index] & 0xFF) << 24) | ((this.array[index + 1] & 0xFF) << 16) | ((this.array[index + 2] & 0xFF) << 8)
                | (this.array[index + 3] & 0xFF);
        }

        /** 读取一个整形 */
        public int readInt()
        {
            int pos = this.offset;
            this.offset += 4;
            return ((this.array[pos] & 0xFF) << 24) | ((this.array[pos + 1] & 0xFF) << 16) | ((this.array[pos + 2] & 0xFF) << 8)
                | (this.array[pos + 3] & 0xFF);
        }

        public int[] readInts()
        {
            int len = readInt();
            int[] array = new int[len];
            for (int i=0;i<len;i++)
            {
                array[i] = readInt();
            }

            return array;
        }

        /** 在指定位置读取一个int，不改变读取进度 */
        public long getLong(int index)
        {
            return ((this.array[index] & 0xFFL) << 56) | ((this.array[index + 1] & 0xFFL) << 48) | ((this.array[index + 2] & 0xFFL) << 40)
                | ((this.array[index + 3] & 0xFFL) << 32) | ((this.array[index + 4] & 0xFFL) << 24) | ((this.array[index + 5] & 0xFFL) << 16)
                | ((this.array[index + 6] & 0xFFL) << 8) | (this.array[index + 7] & 0xFFL);
        }

        /** 读取一个长整形 */
        public long readLong()
        {
            int pos = this.offset;
            this.offset += 8;
            return ((this.array[pos] & 0xFFL) << 56) | ((this.array[pos + 1] & 0xFFL) << 48) | ((this.array[pos + 2] & 0xFFL) << 40)
                | ((this.array[pos + 3] & 0xFFL) << 32) | ((this.array[pos + 4] & 0xFFL) << 24) | ((this.array[pos + 5] & 0xFFL) << 16)
                | ((this.array[pos + 6] & 0xFFL) << 8) | (this.array[pos + 7] & 0xFFL);
        }

        /** 在指定位置读取一个float，不改变读取进度 */
        public float getFloat_(int index)
        {
            return BitConverter.ToSingle(this.array, index);
        }

        /** 读取一个float */
        public float readFloat_()
        {
            int pos = this.offset;
            this.offset += 4;
            return BitConverter.ToSingle(this.array, pos);
        }

        /** 在指定位置读取一个float，不改变读取进度 */
        public float getFloat(int index)
        {
            index += 3;
            byte[] bytes = new byte[4];
            for (int i = 0; i < bytes.Length; ++i)
            {
                bytes[i] = this.array[index--];
            }
            return BitConverter.ToSingle(bytes, 0);
        }

        /** 读取一个float */
        public float readFloat()
        {
            int index = this.offset + 3;
            byte[] bytes = new byte[4];
            for (int i = 0; i < bytes.Length; ++i)
            {
                bytes[i] = this.array[index--];
            }
            this.offset += 4;
            return BitConverter.ToSingle(bytes, 0);
        }

        /** 在指定位置读取一个double，不改变读取进度 */
        public double getDouble_(int index)
        {
            return BitConverter.ToDouble(this.array, index);
        }

        /** 读取一个double */
        public double readDouble_()
        {
            int pos = this.offset;
            this.offset += 8;
            return BitConverter.ToDouble(this.array, pos);
        }

        /** 在指定位置读取一个double，不改变读取进度 */
        public double getDouble(int index)
        {
            index += 7;
            byte[] bytes = new byte[8];
            for (int i = 0; i < bytes.Length; ++i)
            {
                bytes[i] = array[index--];
            }
            return BitConverter.ToDouble(bytes, 0);
        }

        /** 读取一个double */
        public double readDouble()
        {
            int index = this.offset + 7;
            byte[] bytes = new byte[8];
            for (int i = 0; i < bytes.Length; ++i)
            {
                bytes[i] = array[index--];
            }
            this.offset += 8;
            return BitConverter.ToDouble(bytes, 0);
        }

        /** 从缓存中读取len个字节，并插入到bytes，从pos开始插入 */
        public void read(byte[] bytes)
        {
            read(bytes, 0, bytes.Length);
        }

        /** 从缓存中读取len个字节，并插入到bytes，从pos开始插入 */
        public void read(byte[] bytes, int pos, int len)
        {
            Array.Copy(this.array, this.offset, bytes, pos, len);
            this.offset += len;
        }

        /// <summary>
        /// 读取一个长度（0至512M）<br>
        /// 原理：以第一个字节二进制前三位来决定长度值占用的字节数（x表示0或1）<br>
        /// <li>1xx_开头：长度值占1个字节，且值只能是剩下的 7=(8-1) 位能表示的范围，即：0~(2^7-1)
        /// <li>01x_开头：长度值占2个字节，且值只能是剩下的 14=(16-2) 位能表示的范围，即：0~(2^14-1)
        /// <li>001_开头：长度值占4个字节，且值只能是剩下的 29=(32-3) 位能表示的范围，即：0~(2^29-1)
        /// </summary>
        public int readLength()
        {
            int n = array[offset] & 0xFF;// 判定字节
            // 第一个字节若小于 0x20否则该字节不能表示为长度
            if (n < 0x20) throw new SystemException("readLength, invalid number:" + n);
            // 第一个字节若大于 0x20（二进制为：0010 0000）则减去 0x20<<(8*3)（补3个字节位数）即为长度值
            if (n < 0x40) return readInt() & ((1 << 29) - 1);
            // 第一个字节若大于 0x40（二进制为：0100 0000）则减去 0x40<<8（补1个字节位数）即为长度值
            if (n < 0x80) return readUnsignedShort() & ((1 << 14) - 1);
            // 第一个字节若大于 0x80（二进制为：1000 0000）则减去 0x80即为长度值
            return readUnsignedByte() & ((1 << 7) - 1);
        }

        /** 读取字节数组（先读长度） */
        public byte[] readBytes()
        {
            int len = readInt();
            if (len == 0) return new byte[0];
            if (len > MAX_DATA_LENGTH) throw new SystemException("data overflow:" + len);
            byte[] bytes = new byte[len];
            read(bytes, 0, len);
            return bytes;
        }

        /** 按指定编码格式读取一个字符串 */
        public string readString(String encoding)
        {
            int len = readInt();
            if (len < 0) return null;
            if (len == 0) return "";
            if (len > MAX_DATA_LENGTH) throw new SystemException("data overflow:" + len);
            byte[] bytes = new byte[len];
            read(bytes, 0, len);
            if (encoding == null) return Encoding.Default.GetString(bytes);
            try
            {
                return Encoding.GetEncoding(encoding).GetString(bytes);
            }
            catch (Exception e)
            {
                throw new SystemException("invalid charsetName:" + encoding, e);
            }
        }

        /** 按utf-8编码格式读取一个字符串 */
        public String readUTF()
        {
            return readString("utf-8");
        }

        /** 在缓存index处写入一个boolean，不改变写入进度 */
        public void setBoolean(bool b, int index)
        {
            this.array[index] = (byte)(b ? 1 : 0);
        }

        /** 写入一个布尔值 */
        public void writeBoolean(bool b)
        {
            if (this.array.Length < this.top + 1) setCapacity(this.top + CAPACITY);
            this.array[top++] = (byte)(b ? 1 : 0);
        }

        /** 在缓存index处写入一个byte，不改变写入进度 */
        public void setByte(int value, int index)
        {
            this.array[index] = (byte)value;
        }

        /** 写入一个字节 */
        public void writeByte(int value)
        {
            if (this.array.Length < this.top + 1) setCapacity(this.top + CAPACITY);
            this.array[this.top++] = (byte)value;
        }

        /** 在缓存index处写入一个short，不改变写入进度 */
        public void setShort(int value, int index)
        {
            this.array[index] = (byte)(value >> 8);
            this.array[index + 1] = (byte)value;
        }

        /** 写入一个短整形 */
        public void writeShort(int value)
        {
            if (this.array.Length < this.top + 2) setCapacity(this.top + CAPACITY);
            this.array[this.top] = (byte)(value >> 8);
            this.array[this.top + 1] = (byte)value;
            this.top += 2;
        }

        /** 在缓存index处写入一个int，不改变写入进度 */
        public void setInt(int value, int index)
        {
            this.array[index] = (byte)(value >> 24);
            this.array[index + 1] = (byte)(value >> 16);
            this.array[index + 2] = (byte)(value >> 8);
            this.array[index + 3] = (byte)value;
        }



        /** 写入一个整形 */
        public void writeInt(int value)
        {
            if (this.array.Length < this.top + 4) setCapacity(this.top + CAPACITY);
            this.array[this.top] = (byte)(value >> 24);
            this.array[this.top + 1] = (byte)(value >> 16);
            this.array[this.top + 2] = (byte)(value >> 8);
            this.array[this.top + 3] = (byte)value;
            this.top += 4;
        }

        public void writeInts(int[] values)
        {
            writeInt(values.Length);
            for (int i=0;i<values.Length;i++)
            {
                writeInt(values[i]);
            }
        }


        /** 在缓存index处写入一个long，不改变写入进度 */
        public void setLong(long value, int index)
        {
            this.array[index] = (byte)(value >> 56);
            this.array[index + 1] = (byte)(value >> 48);
            this.array[index + 2] = (byte)(value >> 40);
            this.array[index + 3] = (byte)(value >> 32);
            this.array[index + 4] = (byte)(value >> 24);
            this.array[index + 5] = (byte)(value >> 16);
            this.array[index + 6] = (byte)(value >> 8);
            this.array[index + 7] = (byte)value;
        }

        /** 写入一个长整形 */
        public void writeLong(long value)
        {
            if (this.array.Length < this.top + 8) setCapacity(this.top + CAPACITY);
            this.array[top] = (byte)(value >> 56);
            this.array[top + 1] = (byte)(value >> 48);
            this.array[top + 2] = (byte)(value >> 40);
            this.array[top + 3] = (byte)(value >> 32);
            this.array[top + 4] = (byte)(value >> 24);
            this.array[top + 5] = (byte)(value >> 16);
            this.array[top + 6] = (byte)(value >> 8);
            this.array[top + 7] = (byte)value;
            this.top += 8;
        }

        /** 在缓存index处写入一个short，不改变写入进度 */
        public void setFloat_(float value, int index)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            for (int i = 0; i < bytes.Length; ++i)
            {
                this.array[index + i] = bytes[i];
            }
        }

        /** 写入一个float */
        public void writeFloat_(float value)
        {
            write(BitConverter.GetBytes(value));
        }

        /** 在缓存index处写入一个short，不改变写入进度 */
        public void setFloat(float value, int index)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            for (int i = 0, j = bytes.Length - 1; i < bytes.Length; ++i, --j)
            {
                this.array[index + i] = bytes[j];
            }
        }

        /** 写入一个float */
        public void writeFloat(float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            for (int i = 0, j = bytes.Length - 1; i < bytes.Length; ++i, --j)
            {
                this.array[this.top + i] = bytes[j];
            }
            this.top += 4;
        }

        /** 在缓存index处写入一个long，不改变写入进度 */
        public void setDouble_(double value, int index)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            for (int i = 0; i < bytes.Length; ++i)
            {
                this.array[index + i] = bytes[i];
            }
        }

        /// <summary> 写入一个浮点double型 </summary>
        public void writeDouble_(double value)
        {
            write(BitConverter.GetBytes(value));
        }

        /** 在缓存index处写入一个long，不改变写入进度 */
        public void setDouble(double value, int index)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            for (int i = 0, j = bytes.Length - 1; i < bytes.Length; ++i, --j)
            {
                this.array[index + i] = bytes[j];
            }
        }

        /** 写入一个double */
        public void writeDouble(double value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            for (int i = 0, j = bytes.Length - 1; i < bytes.Length; ++i, --j)
            {
                this.array[this.top + i] = bytes[j];
            }
            this.top += 8;
        }

        /**
         * 写入一个长度（0至512M）<br>
         * 原理：以第一个字节二进制前三位来决定长度值占用的字节数（x表示0或1）<br>
         * <li>1xx_开头：长度值占1个字节，且值只能是剩下的 7=(8-1) 位能表示的范围，即：0~(2^7-1)
         * <li>01x_开头：长度值占2个字节，且值只能是剩下的 14=(16-2) 位能表示的范围，即：0~(2^14-1)
         * <li>001_开头：长度值占4个字节，且值只能是剩下的 29=(32-3) 位能表示的范围，即：0~(2^29-1)
         */
        public void writeLength(int len)
        {
            // 不在0~(2^29-1)范围内
            if (len >= 0x20000000 || len < 0) throw new SystemException(" writeLength, invalid len:" + len);
            // 写入时，加上判定位表示的值
            if (len < 0x80)// 1xx范围0~(2^7-1)
                writeByte(len | 0x80);
            else if (len < 0x4000)// 01x范围0~(2^14-1)
                writeShort(len | 0x4000);
            else// 001范围0~(2^29-1)
                writeInt(len | 0x20000000);
        }

        /** 写入byte数组 */
        public void write(byte[] bytes)
        {
            write(bytes, 0, bytes.Length);
        }

        /** 写入byte数组(从position开始写入len个) */
        public void write(byte[] bytes, int pos, int len)
        {
            if (len <= 0) return;
            if (this.array.Length < this.top + len) setCapacity(this.top + len);
            Array.Copy(bytes, pos, this.array, this.top, len);
            this.top += len;
        }

        public void writeBytes(byte[] bytes)
        {
            writeInt(bytes.Length);
            write(bytes, 0, bytes.Length);
        }

        /** 写入一个指定编码类型的字符串 */
        public void writeString(String str, String encoding)
        {
            if (str == null)
                writeInt(-1);
            else if (str.Length <= 0)
                writeInt(0);
            else
            {
                byte[] bytes;
                if (encoding == null)
                    bytes = Encoding.Default.GetBytes(str);
                else
                {
                    try
                    {
                        bytes = Encoding.GetEncoding(encoding).GetBytes(str);
                    }
                    catch (Exception e)
                    {
                        throw new SystemException("invalid charsetName:" + encoding, e);
                    }
                }
                writeInt(bytes.Length);
                write(bytes, 0, bytes.Length);
            }
        }

        /** 写入一个utf-8编码类型的字符串 */
        public void writeUTF(String str)
        {
            writeString(str, "utf-8");
        }

        /** 将指定字节缓冲区数据写入当前缓存区 */
        public void write(ByteBuffer data)
        {
            write(data.getArray(), data.offset, data.length);
        }

        /** 返回：有效字节数组 */
        public byte[] toArray()
        {
            int len = this.top - this.offset;
            if (len == 0) return new byte[0];
            byte[] bytes = new byte[len];
            Array.Copy(this.array, this.offset, bytes, 0, len);
            return bytes;
        }

        /** 清空 */
        public virtual ByteBuffer clear()
        {
            this.top = 0;
            this.offset = 0;
            return this;
        }

        public String toString()
        {
            return "ByteBuffer:{offset=" + this.offset + ", top=" + this.top + ", data=" + HexKit.toHex(toArray()) + "}";
        }
    }
}