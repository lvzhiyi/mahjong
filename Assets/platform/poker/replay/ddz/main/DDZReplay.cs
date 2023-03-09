using cambrian.common;

namespace platform.poker
{
    public class DDZReplay : BaseReplay
    {
        /// <summary> 每一步操作集合 </summary>
        public ArrayList<DDZOrder> list { get; private set; }

        /// <summary> 场景列表 </summary>
        public ArrayList<DDZMatch> sceneList { get; private set; }

        /// <summary> 用于播放特效 </summary>
        public Queue<DDZOrder> queueOrder { get; private set; }

        /// <summary> 当前正在播放的操作 </summary>
        public DDZOrder curr_order { get; private set; }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);

            list = new ArrayList<DDZOrder>();
            sceneList = new ArrayList<DDZMatch>();
            queueOrder = new Queue<DDZOrder>(100);

            while (data.length != 0)
            {
                DDZOrder order = new DDZOrder();
                order.byteRead(data, room.indexOf());
                list.add(order);
            }
        }

        public override  void start()
        {
            index = 0;
            queueOrder.add(list.get(index));
            index++;
        }

        public override  void execute()
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

            var match = DDZMatch.match.cloneCardMatch(curr_order.executeOrder(room));

            if (index - 1 == sceneList.count)
            {
                sceneList.add(match);
            }
        }

        public override  void doOrder()
        {
            if (index >= list.count) return;
            queueOrder.add(list.get(index));
            index++;
        }

        public override  void fallback()
        {
            if (curr_order != null || !queueOrder.isEmpty()) return;

            index--;

            if (index < 0) index = 0;

            if (index >= sceneList.count)
                index = sceneList.count - 1;

            new ReplayDDZRefreshProcess(sceneList.get(index)).execute();
        }
    }
}
