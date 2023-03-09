using cambrian.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace platform.mahjong
{
    /// <summary>
    /// 服务器端主动踢人
    /// </summary>
    public class MJKickPlayerOperate:MJBaseOperate
    {
        /// <summary>
        /// 被踢者玩家id
        /// </summary>
        public long userid;

        public MJKickPlayerOperate(long userid, int selfIndex, int type= RecvMJMatchMsg.MSG_ROOM_REMOVE_PLAYER,  bool isreplay = false) : base(type, selfIndex, isreplay)
        {
            this.userid = userid;
        }
    }
}
