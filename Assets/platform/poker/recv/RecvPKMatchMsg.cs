using cambrian.common;

namespace platform.poker
{
    /// <summary> 扑克 接收比赛消息 </summary>
    public class RecvPKMatchMsg : RecvMsgHandle
    {
        protected static cambrian.common.Logger log = LogFactory.getLogger<RecvPKMatchMsg>(true);

        public RecvPKMatchMsg()
        {
            gamePlatform = GamePlatform.PK_GAME;
        }

        private int type, step, stage, round, paidui, len;
        private int[] operates;

        private void readData(ByteBuffer data)
        {
            type = data.readInt();
            step = data.readInt();
            stage = data.readInt();
            round = data.readInt();
            paidui = data.readInt();
            len = data.readInt();
            operates = new int[len];
        }
   
        public override void handle(ByteBuffer data)
        {
            if (Room.room == null) { return; }
            readData(data);
            baseOperate = baseOperates();
            if (baseOperate != null)
            {
                for (int i = 0; i < len; i++)
                {
                    operates[i] = data.readInt();
                }
                PKRoomPanel.Panel.operate = operates[Room.room.indexOf()];
                addRecvOperate(baseOperate, data);
            }
        }

        private PKBaseOperate baseOperate;

        private RecvDDZMatchMsg ddzMsg;

        private RecvPDKMatchMsg pdkMsg;

        private RecvPDKTenMatchMsg pdkTenMsg;

        private RecvPDKAnYueMatchMsg pdkAnYueMsg;

      

        /// <summary> 获取操作数据 </summary>
        private PKBaseOperate baseOperates()
        {
            switch (PKGAME.RULESID)
            {
                case PKGAME.DDZ:
                    if (ddzMsg == null) ddzMsg = new RecvDDZMatchMsg();
                    return ddzMsg.recvOperate(type, step, operates, stage, round, paidui);
                case PKGAME.PDK:
                    if (pdkMsg == null) pdkMsg = new RecvPDKMatchMsg();
                    return pdkMsg.recvOperate(type, step, operates, stage, round, paidui);
                case PKGAME.PDK_10:
                    if (pdkTenMsg == null) pdkTenMsg = new RecvPDKTenMatchMsg();
                    return pdkTenMsg.recvOperate(type, step, operates, stage, round, paidui);
                case PKGAME.PDK_ANYUE:
                    if (pdkAnYueMsg == null) pdkAnYueMsg = new RecvPDKAnYueMatchMsg();
                    return pdkAnYueMsg.recvOperate(type, step, operates, stage, round, paidui);
                case PKGAME.ZSY:
                default: return null;
            }
        }

        /// <summary> 添加操作 </summary>
        public void addRecvOperate(PKBaseOperate recvOperate, ByteBuffer data)
        {
            recvOperate.bytesRead(data);
            PKRoomPanel.Panel.addRecvOperate(recvOperate);
        }
    }
}
