using basef.player;
using cambrian.common;
using cambrian.uui;
using platform.spotred;
using platform.spotred.waitroom;
using UnityEngine.UI;

namespace platform.mahjong
{
    public class MJOverBar:UnrealLuaSpaceObject
    {
        /// <summary>
        /// 玩家对象
        /// </summary>
        SimplePlayer player;
        /// <summary>
        /// 玩家牌
        /// </summary>
        MJPlayerCards pcard;
        /// <summary>
        /// 是否是庄家
        /// </summary>
        bool isBanker = false;
        /// <summary>
        /// 是否是自己
        /// </summary>
        bool isSelf = false;
        //-----------UI---------------
        PlayerBar playerbar;
        /// <summary>
        /// 背景图片
        /// </summary>
        //SpritesList bgimg;
        /// <summary>
        /// 手牌容器
        /// </summary>
        UnrealTableContainer handContainer;
        /// <summary>
        /// 固定牌容器
        /// </summary>
        UnrealTableContainer fixedContainer;
        /// <summary>
        /// 分数
        /// </summary>
        UnrealTextField pointwin;
        /// <summary>
        /// 分数
        /// </summary>
        UnrealTextField pointlose;
        /// <summary>
        /// 番数
        /// </summary>
        UnrealTextField fanshu;
        /// <summary>
        /// 信息
        /// </summary>
        Text info;
        /// <summary>
        /// 自己的信息
        /// </summary>
        Text selfinfo;
        /// <summary>
        /// 胡牌显示
        /// </summary>
        SpritesList huImage;

        UnrealLuaSpaceObject huCardView;

        Image hucard;

        Image hubg;

        UnrealTextField nametext;

        UnrealTextField selfname;

        Image piaoImage;

        private Room room;

        float initY;
        protected override void xinit()
        {
            info = transform.Find("info").GetComponent<Text>();
            selfinfo = transform.Find("selfinfo").GetComponent<Text>();
            handContainer = transform.Find("hand").GetComponent<UnrealTableContainer>();
            handContainer.init();
            fixedContainer = transform.Find("fixed").GetComponent<UnrealTableContainer>();
            fixedContainer.init();
            initY = fixedContainer.transform.localPosition.y;
            pointwin = transform.Find("pointwin").GetComponent<UnrealTextField>();
            pointwin.init();
            pointlose = transform.Find("pointlose").GetComponent<UnrealTextField>();
            pointlose.init();
            fanshu = transform.Find("fanshu").GetComponent<UnrealTextField>();
            fanshu.init();
            huImage = transform.Find("hu").GetComponent<SpritesList>();
            huCardView = transform.Find("hucard").GetComponent<UnrealLuaSpaceObject>();
            hucard = huCardView.transform.Find("card").GetComponent<Image>();
            hubg = huCardView.transform.Find("bg").GetComponent<Image>();
            playerbar = transform.Find("player").GetComponent<PlayerBar>();
            playerbar.init();
            nametext = transform.Find("name").GetComponent<UnrealTextField>();
            nametext.init();
            selfname = transform.Find("selfname").GetComponent<UnrealTextField>();
            selfname.init();
            piaoImage = transform.Find("piao").GetComponent<Image>();
        }

        public void setData(Room room,int index,MJPlayerCards pcard,bool isBanker,bool isSelf)
        {
            this.room = room;
            this.player = room.players[index].player;
            this.pcard = pcard;
            this.isBanker = isBanker;
            this.isSelf = isSelf;
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            hubg.sprite = MJWidgetManager.getInstance().getCardBg(1);
            //bgimg.ShowIndex(isSelf ? 1 : 0, false);
            showFixedCards();
            showHandCards();
            playerbar.setPlayer(this.player, 0);//这里并没有显示当前的积分
            playerbar.refresh();
            nametext.text = player.name;
            selfname.text = player.name;
            nametext.gameObject.SetActive(!isSelf);
            selfname.gameObject.SetActive(isSelf);
            if (isBanker) this.playerbar.showDang();
            showPoint();
            showinfo();
            piaoImage.gameObject.SetActive( pcard.hasStatus(MJPlayerCards.STATUS_PIAO));
        }

        /// <summary>
        /// 显示输赢信息
        /// </summary>
        private void showinfo()
        {
            //先要去获取赢家的牌型 根据分数>0和胡牌类型
            Settle accos = null;
            string str = "";
            string pointStr = "";
            bool b = false;

            ArrayList<int> playerindex=new ArrayList<int>();


            for (int i = 0; i < pcard.accos.Length; i++)
            {
                accos = pcard.accos[i];
                if (accos.score > 0 && accos.type == MJSettle.SETTLE_HU)
                {
                    if(!b)
                        b = setInfo(accos, ref str, ref pointStr);
                    playerindex.add(accos.dest);
                }
                else if (accos.score > 0&&accos.type==MJSettle.SETTLE_CHECK_LISTEN)
                {
                    if (!b)
                    {
                        b = setInfo(accos, ref str, ref pointStr);
                        huImage.ShowIndex(7);
                        huImage.setVisible(true);
                    }

                    playerindex.add(accos.dest);
                }
                else if (accos.score > 0 && accos.type == MJSettle.SETTLE_MULTIPLE)
                {
                    if (!b)
                    {
                        b = setInfo(accos, ref str, ref pointStr);
                        huImage.ShowIndex(6);
                        huImage.setVisible(true);
                    }
                       
                    playerindex.add(accos.dest);
                }
            }


            CharBuffer buffer = new CharBuffer();

            if (playerindex.count > 0)
            {
                buffer.append("、玩家(");

                for (int i = 0; i < playerindex.count; i++)
                {
                    buffer.append(StringKit.substring(room.players[playerindex.get(i)].player.name,0,6));
                    if (i != playerindex.count - 1)
                        buffer.append(",");
                }

                buffer.append(")");
            }


            info.gameObject.SetActive(false);
            selfinfo.gameObject.SetActive(false);
            Text temp = isSelf ? selfinfo : info;
            string b1= buffer.getString();
            temp.text = str + b1;
            temp.gameObject.SetActive(true);
            fanshu.text = pointStr;
        }

        private bool setInfo(Settle accos, ref string str, ref string pointStr)
        {
            if (str.Equals(""))
            {
                str = accos.getSettleName(room.getRule().getBet(), room.getRule()) + "";
                pointStr = accos.point + "番";
                int maxPoint = Room.room.roomRule.rule.maxPoint;
                if (maxPoint != 0 && accos.point == maxPoint) str += "ㅤ(封顶)";

                return true;
            }

            return false;
        }

        private void showPoint()
        {
            long point = pcard.point;
            pointwin.gameObject.SetActive(point>=0);
            pointlose.gameObject.SetActive(point<0);

            if (point > 0)
                pointwin.text = "+" + MathKit.getRound2LongStr(point);
            else if (point < 0)
                pointlose.text = MathKit.getRound2LongStr(point);
            else
                pointwin.text = MathKit.getRound2LongStr(point);
        }

        /// <summary>
        /// 显示手牌
        /// </summary>
        private void showHandCards()
        {
            float fixedWidth = fixedContainer.getWidth();
            ArrayList<int> list= pcard.handcards;

            int mocard = pcard.mocard;

            handContainer.cols = list.count;
            handContainer.resize(list.count);
            for (int i=0;i< list.count;i++)
            {
                MJOverHandBar bar=(MJOverHandBar)handContainer.bars[i];
                bar.index_space = i;
                bar.setCard(list.get(i));
                bar.refresh();
            }

            handContainer.resizeDelta();

            float x = fixedContainer.transform.localPosition.x + fixedWidth;

            DisplayKit.setLocalX(handContainer, x);
            DisplayKit.setLocalY(handContainer, initY);

            bool ishu = pcard.hasStatus(MJPlayerCards.STATUS_HU);

            huCardView.setVisible(ishu);

            if (ishu)
            {
                if (MJCard.isSigned(mocard, MJCard.SIGN_HU))
                    mocard = MJCard.cancelSign(mocard, MJCard.SIGN_HU);
                hucard.sprite = MJWidgetManager.getInstance().getCard(mocard);
                huImage.ShowIndex(MJHuView.getIndex(pcard.huIndex, pcard.hutype));
            }

            DisplayKit.setLocalX(huCardView,handContainer.transform.localPosition.x+handContainer.getWidth()+ huCardView.getWidth());
            DisplayKit.setLocalY(huCardView, initY);
            huImage.setVisible(ishu);
        }

        private void showFixedCards()
        {
            ArrayList<FixedCards> list = pcard.fixCards;
            fixedContainer.cols = list.count;
            fixedContainer.resize(list.count);
            for (int i = 0; i < list.count; i++)
            {
                MJOverFixedBar bar = (MJOverFixedBar)fixedContainer.bars[i];
                bar.setFixedCards((MJFixedCards)list.get(i));
                bar.refresh();
            }

            fixedContainer.resizeDelta();
            DisplayKit.setLocalY(fixedContainer, initY);
        }
    }
}
