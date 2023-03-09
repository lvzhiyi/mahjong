using cambrian.common;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace platform.mahjong
{
    /// <summary>
    /// 单局结束面板
    /// </summary>
    public class MJOverPanel:UnrealLuaPanel
    {
        /// <summary>
        /// 玩家容器
        /// </summary>
        UnrealTableContainer container;
        /// <summary>
        /// 下一局
        /// </summary>
        [HideInInspector] public UnrealTextButton button;
        /// <summary>
        /// 规则详情
        /// </summary>
        UnrealTextField ruleinfo;
        /// <summary>
        /// 房间id
        /// </summary>
        UnrealTextField roomid;
        /// <summary>
        /// 局数
        /// </summary>
        UnrealTextField jushu;
        /// <summary>
        /// 时间
        /// </summary>
        UnrealTextField time;

        public Room room;

        public MJMatch match;

        private Text count;

        private int counttime = 15;

        protected override void xinit()
        {
            base.xinit();
            container = content.Find("container").GetComponent<UnrealTableContainer>();
            container.init();
            button = content.Find("next").GetComponent<UnrealTextButton>();
            button.init();
            ruleinfo = content.Find("rule").GetComponent<UnrealTextField>();
            ruleinfo.init();
            roomid = content.Find("roomid").GetComponent<UnrealTextField>();
            roomid.init();
            jushu = content.Find("jushu").GetComponent<UnrealTextField>();
            jushu.init();
            time = content.Find("time").GetComponent<UnrealTextField>();
            time.init();
            if (content.Find("countdown") != null)
                count = content.Find("countdown").Find("time").GetComponent<Text>();
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="room"></param>
        /// <param name="match"></param>
        /// <param name="card">胡的牌</param>
        /// <param name="index">胡牌者</param>
        public virtual void refrshData(Room room,MJMatch match)
        {
            this.room = room;
            this.match = match;
            ruleinfo.text = room.getRule().getPlayRule(false);
            button.text.text = room.roomRule.isOver() ? "结算":"下一局";
            roomid.text = room.getRoomIndex()+"";
            jushu.text = room.roomRule.getNowPalyNum();
            time.text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            container.resize(room.players.Length);

            MJPlayerCards pcards;

            int slefindex = room.indexOf();
            for (int i=0;i<room.players.Length;i++)
            {
                MJOverBar bar=(MJOverBar)container.bars[i];
                pcards = (MJPlayerCards)match.getPCards()[i];
                bar.setData(room,i,pcards, i == match.banker,i== slefindex);
                bar.refresh();
            }
            counttime = 15;
            curtime = TimeKit.currentTimeMillis;
        }

        public override void setVisible(bool b)
        {
            base.setVisible(b);
        }

        private long curtime;
        protected override void xupdate()
        {
            long now = TimeKit.currentTimeMillis;
            if (curtime == 0||count==null) return;

            if (now - curtime > TimeKit.SECOND_MILLS)
            {
                curtime = now;
                this.count.text = counttime + "";
                if (counttime <= 0)
                {
                    curtime = 0;
                    new MJShowAllOverProcess().execute();
                }
                else
                {
                    counttime--;
                }
            }
        }
    }
}
