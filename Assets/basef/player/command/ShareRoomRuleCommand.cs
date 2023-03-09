using cambrian.common;
using cambrian.game;
using scene.game;
using basef.rule;

namespace basef.player
{
    public class ShareRoomRuleCommand:UserCommand
    {

        private RoomEntry roomEntry;
        public ShareRoomRuleCommand(RoomEntry roomEntry)
        {
            this.id = CommandConst.PORT_ROOM_PROMOT_RULE_ROOM;
            this.roomEntry = roomEntry;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(roomEntry.roomid);
            data.writeLong(roomEntry.createtime);
        }

        public override void bytesRead(ByteBuffer data)
        {
            Rule rule=RuleManager.manager.newSample(data.readInt());

            rule.bytesRead(data);

            callbackobj = rule;
        }
    }
}
