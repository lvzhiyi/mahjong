using basef.player;
using cambrian.common;
using platform.spotred;

namespace platform.poker
{
    public class PDKAnYueReplay : BytesSerializable
    {
        /// <summary> 规则 </summary>
        public RoomRule rule { get; private set; }

        /// <summary> 房间 </summary>
        public Room room { get; private set; }

        /// <summary> 房间号 </summary>
        public int roomid { get; private set; }

        /// <summary> 玩家 </summary>
        public SimplePlayer[] players { get; private set; }

        /// <summary> 每一步操作集合 </summary>
        public ArrayList<PDKAnYueOrder> list { get; private set; }

        /// <summary> 场景列表 </summary>
        public ArrayList<PDKAnYueMatch> sceneList { get; private set; }

        /// <summary> 用于播放特效 </summary>
        public Queue<PDKAnYueOrder> queueOrder { get; private set; }

        /// <summary> 当前正在播放的操作 </summary>
        public PDKAnYueOrder curr_order { get; private set; }

        /// <summary> order步骤index </summary>
        public int index { get; private set; }

        public int time { get; private set; }

        public void setData(int roomid, int time, RoomRule rule)
        {
            this.roomid = roomid;
            this.time = time;
            this.rule = rule;
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = rule.rule.playerCount;
            players = new SimplePlayer[len];
            for (int i = 0; i < len; i++)
            {
                players[i] = new SimplePlayer();
                players[i].bytesRead(data);
            }
            createRoom();

            list = new ArrayList<PDKAnYueOrder>();
            sceneList = new ArrayList<PDKAnYueMatch>();
            queueOrder = new Queue<PDKAnYueOrder>(100);

            PDKAnYueOrder order = null;
            while (data.length != 0)
            {
                order = new PDKAnYueOrder();
                order.byteRead(data);
                list.add(order);
            }
        }

        public void start()
        {
            index = 0;
            queueOrder.add(list.get(index));
            index++;
        }

        public void execute()
        {
            if (queueOrder == null) return;
            long time = TimeKit.currentTimeMillis;

            if (queueOrder.isEmpty() && curr_order == null) return;
            if (curr_order != null && curr_order.operate.getWaittime() > time)
            {
                curr_order = null;
                return;
            }
            if (curr_order != null && !curr_order.operate.operateData.isOver) return;

            curr_order = queueOrder.remove();
            if (curr_order == null) return;
            curr_order.operate.operateData.waittime = curr_order.operate.getWaittime() + time;

            var match = PDKAnYueMatch.match.cloneCardMatch(curr_order.executeOrder(room));

            if (index - 1 == sceneList.count)
            {
                sceneList.add(match);
            }
        }

        private void createRoom()
        {
            Room.instance();
            room = Room.room;
            room.setIndex(roomid);
            room.players = new MatchPlayer[rule.rule.playerCount];
            for (int i = 0; i < room.players.Length; i++)
            {
                room.players[i] = new MatchPlayer();
                room.players[i].player = players[i];
            }
            room.roomRule = rule;
        }

        public void doOrder()
        {
            if (index >= list.count) return;
            queueOrder.add(list.get(index));
            index++;
        }

        public void fallback()
        {
            if (curr_order != null || !queueOrder.isEmpty()) return;
            index--;
            if (index < 0) index = 0;
            if (index >= sceneList.count)
                index = sceneList.count - 1;
            new ReplayPDKAnYueRefreshProcess(sceneList.get(index)).execute();
        }
    }
}
