using System.Collections;
using XLua;

namespace cambrian.common
{
    [LuaCallCSharp]
    public class BytesSerializable
    {

        public Hashtable data;

        public void instanceData()
        {
            this.data=new Hashtable();
        }

        public void setDataValue(object key,object value)
        {
            if(this.data==null)
                this.data=new Hashtable();
            if (this.data.ContainsKey(key))
                this.data[key] = value;
            else
                this.data.Add(key, value);
        }

        private bool isExistKey(object key)
        {
            if (this.data == null || !this.data.ContainsKey(key))
                return false;
            return true;
        }


        public object getDataValue(object key)
        {
            if (!isExistKey(key))
                return null;
            return this.data[key];
        }

        public bool removeKV(object key)
        {
            if (!isExistKey(key)) return false;
            this.data.Remove(key);
            return true;
        }

        public Hashtable getHashTable()
        {
            return this.data;
        }


        public virtual void bytesRead(ByteBuffer data)
        {
        }

        public virtual void bytesWrite(ByteBuffer data)
        {
        }
    }
}
