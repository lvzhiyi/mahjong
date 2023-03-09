using cambrian.common;
using cambrian.game;
using scene.game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace basef.arena
{
    public class GetAuxiliaryScoreSettingCommand : UserCommand
    {
        /// <summary>
        /// 目标id
        /// </summary>
        private long destid;
        /// <summary>
        /// 辅分
        /// </summary>
        private long fufeng;
        /// <summary>
        /// 辅分预警
        /// </summary>
        private long warning;

        public GetAuxiliaryScoreSettingCommand(long destid, long fufeng, long warning)
        {
            id = CommandConst.PORT_ARENA_AUXILIARY_SCORE_SETTING;
            this.destid = destid;
            this.fufeng = fufeng;
            this.warning = warning;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(Arena.arena.getId());
            data.writeLong(destid);
            data.writeLong(fufeng);
            data.writeLong(warning);
        }

        public override void bytesRead(ByteBuffer data)
        {
            callbackobj = data.readBoolean();
        }
    }
}