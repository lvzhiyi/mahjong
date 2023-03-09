using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    public class ArenaMsgApplyContentEventCommand : UserCommand
    {
        /// <summary> 查询类型 </summary>
        bool agree;
        /// <summary>
        /// 事件的uuid
        /// </summary>
        long uuid;

        private int type;

        public ArenaMsgApplyContentEventCommand(bool agree,long uuid,int type)
        {
            this.id = CommandConst.PORT_ARENA_HANDLE_EVENT;
            this.agree = agree;
            this.uuid = uuid;
            this.type = type;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(Arena.arena.getId());
            data.writeInt(type);
            data.writeLong(uuid);
            data.writeBoolean(agree);
        }

        public override void bytesRead(ByteBuffer data)
        {
            callbackobj = data.readBoolean();
        }
    }
}