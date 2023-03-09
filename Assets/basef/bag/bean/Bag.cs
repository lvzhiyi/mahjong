using cambrian.common;
using basef.prop;

namespace basef.bag
{
    /// <summary> 背包 </summary>
    public class Bag: BytesSerializable
    {
        public static Bag bag = new Bag();

        /// <summary> 实际使用格子数 </summary>
        public int size;
        /// <summary> 行囊最大格子数 </summary>
        public int maxSize;

        public ArrayList<Prop> props;

        /// <summary> 移除指定道具 </summary>
        public void removeProp(int sid)
        {
            for (int i=0;i<props.count;i++)
            {
                if (props.get(i).sid == sid)
                {
                    props.removeAt(i);
                    break;
                }
            }
        }

        public void removePropCount(int sid,int count)
        {
            for (int i = 0; i < props.count; i++)
            {
                if (props.get(i).sid == sid)
                {
                    props.get(i).decrCount(count);
                    if (count == 0)
                    {
                        props.removeAt(i);
                    }
                    break;
                }
            }
        }

        /// <summary> 获取指定道具 </summary>
        public Prop getProp(int sid)
        {
            for (int i = 0; i < props.count; i++)
            {
                Prop prop = props.get(i);
                if (prop.sid == sid)
                {
                    return prop;
                }
            }

            return null;
        }

        /// <summary> 获取房间道具 </summary>
        public Prop[] getRoomProps()
        {
            ArrayList<Prop> list = new ArrayList<Prop>();
            for (int i=0;i<props.count;i++)
            {
                Prop prop = props.get(i);
                if (prop is RoomProp)
                {
                    list.add(prop);
                }
            }
            return list.toArray();
        }

        public override void bytesRead(ByteBuffer data)
        {
            this.maxSize = data.readInt();
            this.size = data.readInt();
            this.props = new ArrayList<Prop>();
            for (int i=0;i<size;i++)
            {
                int sid=data.readInt();
                Prop prop=(Prop)Sample.factory.newSample(sid);
                prop.bytesRead(data);
                props.add(prop);
            }
        }
    }
}
