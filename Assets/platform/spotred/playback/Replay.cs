﻿using cambrian.common;

namespace platform.spotred.playback
{
    /// <summary>
    /// 单局长牌游戏数据
    /// </summary>
    public class Replay:BaseReplay
    {
        /// <summary>
        /// 每一步操作集合
        /// </summary>
        ArrayList<SpotRedOrder> list;
        /// <summary>
        /// 场景列表
        /// </summary>
        ArrayList<CPMatch> sceneList;
        /// <summary>
        /// 用于播放特效，播放完成后再播放下一个
        /// </summary>
        private Queue<SpotRedOrder> queueOrder;
        /// <summary>
        /// 当前正在播放的操作
        /// </summary>
        private SpotRedOrder curr_order;
        /// <summary>
        /// 结束的索引
        /// </summary>
        int overindex;

        /// <summary>
        /// 胡的牌
        /// </summary>
        public static int hu_card;

        public override  void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            hu_card = -1;
            this.list = new ArrayList<SpotRedOrder>();
            this.sceneList = new ArrayList<CPMatch>();
            this.queueOrder = new Queue<SpotRedOrder>(50);

            int selfIndex=this.getRoom().indexOf();
            while (data.length != 0)
            {
                SpotRedOrder order = new SpotRedOrder();
                order.byteRead(data, selfIndex);
                list.add(order);
            }
        }

        /// <summary>
        /// 开始回放
        /// </summary>
        public override  void start()
        {
            this.index = 0;
            queueOrder.add(this.list.get(this.index));
            this.index++;
        }

        public override  void execute()
        {
            if (queueOrder == null) return;
            long time = TimeKit.currentTimeMillis;

            if (queueOrder.isEmpty()&&curr_order==null) return;
            if (curr_order != null && curr_order.recvOperate.getWaittime() > time)
            {
                curr_order = null;
                return;
            }
            if (curr_order != null && !curr_order.recvOperate.isOver) return; //未完成播放

            curr_order = queueOrder.remove();
            if (curr_order == null) return;
            curr_order.recvOperate.waittime= curr_order.recvOperate.getWaittime()+ time;
            CPMatch scene = (CPMatch)CPMatch.match.cloneCardScene(curr_order.executeOrder(getRoom()));
            if (this.index - 1 == this.sceneList.count)
            {
                this.sceneList.add(scene);
            }
        }

        public override  void doOrder()
        {
            if (this.index >= list.count)
            {
                return;
            }
            queueOrder.add(this.list.get(this.index));

            this.index++;
        }

        //倒退1步
        public override  void fallback()
        {
            if (curr_order != null|| !queueOrder.isEmpty()) return;
           
            this.index = this.index -1;
            if (this.index < 0) this.index = 0;

            if (index >= this.sceneList.count)
                this.index=this.sceneList.count-1;

            CPMatch scene = (CPMatch)CPMatch.match.cloneCardScene(this.sceneList.get(this.index));

            ReplayRefreshProcess process = new ReplayRefreshProcess();
            process.setScene(scene);
            process.execute();
        }
    }
}
