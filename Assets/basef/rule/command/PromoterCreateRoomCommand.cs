using cambrian.common;
using cambrian.game;
using scene.game;
using platform.spotred.room;

namespace basef.rule
{
    /// <summary>
    /// 创建代开房
    /// </summary>
    public class PromoterCreateRoomCommand:UserCommand
    {
        Rule rule;

        public PromoterCreateRoomCommand(Rule rule)
        {
            this.id = CommandConst.PORT_ROOM_CREATE_BY_PROMOTER;
            this.rule = rule;
        }
        public override void bytesWrite(ByteBuffer data)
        {
            this.rule.bytesWrite(data);
        }
        public override void bytesRead(ByteBuffer data)
        {
            this.callbackobj = data.readBoolean();
        }
    }
}
