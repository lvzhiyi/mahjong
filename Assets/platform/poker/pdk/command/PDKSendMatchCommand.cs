using cambrian.common;
using scene.game;

namespace platform.poker
{
    public class PDKSendMatchCommand : SendCommand
    {
        /// <summary> 类型 </summary>
        private int type;

        /// <summary> 数量 </summary>
        private int count;

        /// <summary> 步数 </summary>
        private int step;

        /// <summary> 牌信息 </summary>
        private PDKCardInfo info;

        /// <summary> 出牌 </summary>
        public PDKSendMatchCommand(int type, int step, PDKCardInfo info)
        {
            this.id = CommandConst.COMMAND_ROOM_MATCH_OPTION;
            this.type = type;
            this.step = step;
            this.info = info;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(type);
            data.writeInt(step);
            switch (type)
            {
                case PKSendMatch.DISCARD:
                    info.bytesWrite(data);
                    break;
                case PKSendMatch.CHE_PAI:
                    info.bytesWrite1(data);
                    
                    break;
                case PKSendMatch.CANCEL:
                    break;
            }
        }

        public override void bytesRead(ByteBuffer data)
        {

        }
    }
}
