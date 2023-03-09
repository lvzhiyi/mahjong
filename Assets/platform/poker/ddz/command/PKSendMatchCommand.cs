using cambrian.common;
using scene.game;

namespace platform.poker
{
    /// <summary> 斗地主 客户端向服务端发送消息 </summary>
    public class DDZSendMatchCommand : SendCommand
    {
        /// <summary> 类型 </summary>
        private int type;

        /// <summary> 数量 </summary>
        private int count;

        /// <summary> 步数 </summary>
        private int step;

        /// <summary> 分数 </summary>
        private int score;

        /// <summary> 牌信息 </summary>
        private DDZCardInfo info;

        /// <summary> 出牌 </summary>
        public DDZSendMatchCommand(int type, int step, DDZCardInfo info)
        {
            this.id = CommandConst.COMMAND_ROOM_MATCH_OPTION;
            this.type = type;
            this.step = step;
            this.info = info;
        }

        /// <summary> 叫分 </summary>
        public DDZSendMatchCommand(int type, int step, int score)
        {
            this.id = CommandConst.COMMAND_ROOM_MATCH_OPTION;
            this.type = type;
            this.step = step;
            this.score = score;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(type);
            data.writeInt(step);
            switch (type)
            {
                case PKSendMatch.JIAOFEN:
                    data.writeInt(score);
                    break;
                case PKSendMatch.DISCARD:
                    info.bytesWrite(data);
                    break;
                case PKSendMatch.JIAOZHUANG:
                case PKSendMatch.QIANGZHUANG:
                case PKSendMatch.JIABEI:
                case PKSendMatch.CANCEL:
                case PKSendMatch.CANCEL_MING:
                case PKSendMatch.MING:
                    break;
            }
        }

        public override void bytesRead(ByteBuffer data)
        {

        }
    }
}
