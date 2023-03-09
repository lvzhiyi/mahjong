using cambrian.common;

namespace basef.arena
{
    public class ArenaRecordContentData : BytesSerializable
    {
        /// <summary> 房间ID </summary>
        public int roomid;

        /// <summary> 回放ID </summary>
        public long playbackid;

        /// <summary> 时间 </summary>
        public long time;

        /// <summary> 单据id </summary>
        public long uuid;

        /// <summary> 玩家名字 </summary>
        public string[] name;

        /// <summary> 玩家分数 </summary>
        public long[] socre;

        /// <summary> 规则名字 </summary>
        public int ruleid;

        /// <summary> 玩家id </summary>
        public long[] playerid;

        /// <summary> 玩家所属 </summary>
        public string[] mastername;
        /// <summary>
        /// 上级的id
        /// </summary>
        public long[] mastersid;
        /// <summary>
        /// 头像
        /// </summary>
        public string[] heads;

        public override void bytesRead(ByteBuffer data)
        {
            uuid = data.readLong();        //UUID
            roomid = data.readInt();       //房间ID
            ruleid = data.readInt();
            playbackid = data.readLong();  //回放id
            time = data.readLong();        //时间
            int len = data.readInt();      //长度



            name=new string[len];
            socre=new long[len];
            playerid = new long[len];
            mastername = new string[len];
            mastersid = new long[len];
            heads = new string[len];
            for (int i = 0; i < len; i++)
            {
                playerid[i] = data.readLong();
                name[i]=data.readUTF();  //玩家名
                heads[i] = data.readUTF();//头像
                socre[i]=data.readLong(); //玩家分数
                mastersid[i] = data.readLong();
                mastername[i] = data.readUTF();
            }
        }
    }
}
