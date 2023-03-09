using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    public class GetArenaPersonalJournalRecordCommand : UserCommand
    {

        public GetArenaPersonalJournalRecordCommand()
        {
            this.id = CommandConst.PORT_ARENA_MEMBERLOG_GET;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(Arena.arena.getId());
        }


        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            PersonalJournalEventRecord[] records = new PersonalJournalEventRecord[len];
            for (int i = 0; i < len; i++)
            {
                records[i] = new PersonalJournalEventRecord();
                records[i].bytesRead(data);
            }
            callbackobj = records;
        }
    }
}