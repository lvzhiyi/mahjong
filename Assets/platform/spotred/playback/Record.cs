using cambrian.common;
using platform.spotred;
using basef.player;

namespace platform.spotred.playback
{
    /// <summary>
    /// 回放记录
    /// </summary>
    public class Record
    {
        public long id;
        public int roomid;
        public RoomRule rule;
        public int time; //秒
        public SimplePlayer[] players;
        public long[] totalscores;
        public long[] replayids;
        public long[][] scores;

        public void bytesRead(ByteBuffer data)
        {
            this.id = data.readLong();
            this.roomid = data.readInt();
            data.readLong();
            this.time = data.readInt();
            this.rule = new RoomRule();
            this.rule.bytesRead(data);
            int len = data.readInt();
            this.players = new SimplePlayer[len];
            this.totalscores = new long[len];
            for (int i = 0; i < len; i++)
            {
                this.players[i] = new SimplePlayer();
                this.players[i].bytesRead(data);
                this.totalscores[i] = data.readLong();
            }
            int idslen = data.readInt();
            this.replayids = new long[idslen];

            this.scores = new long[idslen][];
            for (int i = 0; i < idslen; i++)
            {
                this.replayids[i] = data.readLong();
                this.scores[i] = new long[len];
                for (int j = 0; j < len; j++)
                {
                    this.scores[i][j] = data.readLong();
                }
            }
        }
    }
}
