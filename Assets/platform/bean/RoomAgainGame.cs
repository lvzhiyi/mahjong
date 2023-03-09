using basef.rule;
using cambrian.common;

namespace platform
{
    public class RoomAgainGame : BytesSerializable
    {
        public long userid;

        public string name;

        public string head;

        public long createtime;

        public int roomid;

        public Rule rule;


        public override void bytesRead(ByteBuffer data)
        {
            userid = data.readLong();

            name = data.readUTF();

            head = data.readUTF();

            createtime = data.readLong();

            roomid = data.readInt();

            rule = RuleManager.manager.newSample(data.readInt());

            rule.bytesRead(data);
        }
    }
}
