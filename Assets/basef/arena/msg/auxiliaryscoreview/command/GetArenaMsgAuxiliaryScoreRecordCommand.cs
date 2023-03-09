using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    public class GetArenaMsgAuxiliaryScoreRecordCommand : UserCommand
    {
        /// <summary>
        /// 最小ID
        /// </summary>
        long minUid;

        public GetArenaMsgAuxiliaryScoreRecordCommand(long minUid)
        {
            this.id = CommandConst.PORT_ARENA_GET_AUXILIARY_SCORE_RECORD;
            this.minUid = minUid;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(Arena.arena.getId());
            data.writeLong(minUid);
        }


        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            AuxiliaryScoreEventRecord[] records = new AuxiliaryScoreEventRecord[len];
            for (int i = 0; i < len; i++)
            {
                records[i] = new AuxiliaryScoreEventRecord();
                records[i].bytesRead(data);
            }
            callbackobj = records;
        }
    }
}