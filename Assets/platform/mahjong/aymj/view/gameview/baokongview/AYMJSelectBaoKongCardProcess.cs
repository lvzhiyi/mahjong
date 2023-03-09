using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 选择报杠的牌
    /// </summary>
    public class AYMJSelectBaoKongCardProcess:MouseClickProcess
    {
        AYMJBaoKongView kongview;
        public override void execute()
        {
            AYMJBaoKongBar bar= GetComponent<AYMJBaoKongBar>();
            bool isSelect=bar.box.getState();

            kongview= transform.parent.parent.GetComponent<AYMJBaoKongView>();

            if (isSelect)
            {
                //取消
                kongview.removeBaoCard(bar.card);
                bar.setAction(!isSelect);
            }
            else
            {
                //选中
                if (isBaoKong(bar.card))
                {
                    bar.setAction(!isSelect);
                }
            }
        }

        public bool isBaoKong(int card)
        {
            ArrayList<int> list = kongview.selectCard;
            if (kongview.selectCard.count == 0)
            {
                kongview.selectCard.add(card);
                return true;
            }
            else
            {
                list.add(card);
                if (!AYMJMatch.match.isBaoKong(list.toArray()))
                {
                    kongview.removeBaoCard(card);
                    return false;
                }
                else
                {
                    return true;
                }

            }
        }
    }
}
