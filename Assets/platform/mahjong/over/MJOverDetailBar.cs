using cambrian.common;
using cambrian.unreal.scroll;

namespace platform.mahjong
{
    public class MJOverDetailBar : ScrollBar
    {
        /// <summary>
        /// 结算信息
        /// </summary>
        Settle settle;
        /// <summary>
        /// 玩家名称
        /// </summary>
        UnrealTextField nametext;
        /// <summary>
        /// 信息
        /// </summary>
        UnrealTextField info;
        /// <summary>
        /// 番数
        /// </summary>
        UnrealTextField point;
        /// <summary>
        /// 分数
        /// </summary>
        UnrealTextField score;


        protected override void xinit()
        {
            nametext = transform.Find("name").GetComponent<UnrealTextField>();
            info = transform.Find("info").GetComponent<UnrealTextField>();
            point = transform.Find("point").GetComponent<UnrealTextField>();
            score = transform.Find("score").GetComponent<UnrealTextField>();
        }

        public void setData(Settle settle)
        {
            this.settle = settle;
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            nametext.text = Room.room.getPlayers()[settle.dest].player.name;
            info.text = settle.getSettleName(Room.room.getRule().getBet(), Room.room.getRule());

            string pointStr = "共" + settle.point + "番";

            if (settle.type == MJSettle.SETTLE_PIAO)
                pointStr = MJSettle.getPiaoName(settle.point);
            else
            {
                if (settle.point == Room.room.roomRule.rule.maxPoint) pointStr += "（封顶）";
            }

            point.text = pointStr;
            point.gameObject.SetActive(settle.point != 0);
            score.text = settle.score > 0 ? "+" + MathKit.getRound2LongStr(settle.score) : MathKit.getRound2LongStr(settle.score) + "";
        }
    }
}
