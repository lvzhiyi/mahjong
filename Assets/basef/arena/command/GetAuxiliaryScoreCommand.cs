using cambrian.common;
using cambrian.game;
using scene.game;
namespace basef.arena
{
    public class GetAuxiliaryScoreCommand : UserCommand
    {
        /// <summary>
        /// 竞赛场id
        /// </summary>
        private long arenaid;
        /// <summary>
        /// 目标id
        /// </summary>
        private long destid;

        public GetAuxiliaryScoreCommand(long arenaid,long destid)
        {
            id = CommandConst.PORT_ARENA_AUXILIARYSCORE_INFO;
            this.destid = destid;
            this.arenaid = arenaid;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(arenaid);
            data.writeLong(destid);
        }

        public override void bytesRead(ByteBuffer data)
        {
            callbackobj = data;
        }
    }
}