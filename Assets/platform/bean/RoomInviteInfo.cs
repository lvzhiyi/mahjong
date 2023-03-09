using cambrian.common;
using basef.teahouse;

namespace platform
{
    /// <summary>
    /// 房间邀请者信息
    /// </summary>
    public class RoomInviteInfo : BytesSerializable
    {
        public long userid;

        public string name;

        public string head;

        public long createtime;

        public long clubid;

        public string clubname;

        public int roomid;

        public ClubLockRule rule;


        public override void bytesRead(ByteBuffer data)
        {
            userid = data.readLong();

            name = data.readUTF();

            head = data.readUTF();

            createtime = data.readLong();

            clubid = data.readLong();

            clubname = data.readUTF();

            roomid = data.readInt();

            rule = new ClubLockRule();

            rule.bytesRead(data);
        }
    }
}
