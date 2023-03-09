using basef.rule;
using DG.Tweening;
using platform.spotred.room;
using UnityEngine;
using UnityEngine.UI;

namespace platform.spotred.playback
{
    /// <summary>
    /// 川牌回放
    /// </summary>
    public class ReplaySpotRoomPanel:SpotRoomPanel
    {
        private Replay replay;

        public ReplayControlView rcview;

        private OperateView bottom_operate;

        private OperateView right_operate;

        private OperateView top_operate;

        private OperateView left_operate;


        private Rule curr_rule;


        protected override void xinit()
        {
            base.xinit();
            this.rcview = this.transform.Find("Canvas").Find("replay").GetComponent<ReplayControlView>();
            this.rcview.init();
            this.bottom_operate = this.transform.Find("Canvas").Find("bottom_0").Find("operate0").GetComponent<OperateView>();
            this.bottom_operate.init();
            this.right_operate = this.transform.Find("Canvas").Find("right_1").Find("operate1").GetComponent<OperateView>();
            this.right_operate.init();
            this.top_operate = this.transform.Find("Canvas").Find("top_2").Find("operate2").GetComponent<OperateView>();
            this.top_operate.init();
            this.left_operate = this.transform.Find("Canvas").Find("left_3").Find("operate3").GetComponent<OperateView>();
            this.left_operate.init();
        }


        public void setRePlay(Replay replay)
        {
            this.replay = replay;
        }

        public Replay getRePlay()
        {
            return replay;
        }

        protected override void xrefresh()
        {
            curr_rule = replay.rule.rule;
            base.xrefresh();
            
            this.rcview.refresh();
            this.bottom_operate.rule = curr_rule;
            this.bottom_operate.refresh();
            this.top_operate.rule = curr_rule;
            this.top_operate.refresh();

            switch (Room.room.getPlayerCount())
            {
                case 2:
                    this.right_operate.setVisible(false);
                    this.left_operate.setVisible(false);
                    break;
                case 3:
                    this.left_operate.setVisible(false);
                    this.right_operate.rule = curr_rule;
                    this.right_operate.refresh();
                    this.right_operate.setVisible(true);
                    break;
                default:
                    this.right_operate.refresh();
                    this.right_operate.setVisible(true);
                    this.left_operate.rule = curr_rule;
                    this.left_operate.refresh();
                    this.left_operate.setVisible(true);
                    break;
            }
        }

        public override void fanRoateOver(object obj)
        {
            Image opencard = boxCardView.getOpenCard();
            Image cardbg = boxCardView.getCardBg();

            int index = (int)obj;
            DisplayKit.setLocalScaleXY(cardbg.transform, 1);
            cardbg.gameObject.SetActive(false);

            DisplayKit.setLocalRoateXYZ(cardbg.transform, 0, 0, 0);

            DisplayKit.setLocalXY(opencard.transform, 0, 0);

            Vector3 pos = Vector3.zero;

            Vector3 p = new Vector3(0, 30, 0);

            float movetime = 0.3f;//移动时间
            switch (getPlayerIndex(index))
            {
                case LOC_DOWN:
                    pos = this.allHandView.selfView.getPlayCardViewLocation() - p;
                    break;
                case LOC_RIGHT:
                    float width = this.allHandView.rightView.transform.parent.parent.GetComponent<RectTransform>().rect.width;
                    p.x -= (width / 2 - 568);
                    movetime = 0.5f;
                    pos = this.allHandView.rightView.getPlayCardViewLocation() - p;
                    break;
                case LOC_TOP:
                    pos = this.allHandView.topView.getPlayCardViewLocation() - p;
                    break;
                case LOC_LEFT:
                    float w1 = this.allHandView.rightView.transform.parent.parent.GetComponent<RectTransform>().rect.width;
                    p.x += (w1 / 2 - 568);

                    movetime = 0.5f;
                    pos = this.allHandView.leftView.getPlayCardViewLocation() - p;

                    break;
            }

            Sequence s = DOTween.Sequence();
            s.Append(opencard.transform.DOLocalMove(pos, movetime).SetEase(Ease.Linear));
            s.Insert(0, opencard.transform.DOLocalRotate(new Vector3(0, 0, 90), movetime));

            s.OnComplete(() =>
            {
                fanpaimoveover(index);
            });
        }

        public override void moveheadView(int index)
        {
            Vector3 pos = Vector3.zero;
            float widthInteral = this.headView.GetComponent<RectTransform>().rect.width / 2 - 568;
            float movetime = 0.2f;//移动时间
            TouCardView touCardView = null;
            switch (getPlayerIndex(index))
            {
                case LOC_DOWN:
                    pos = this.headView.bottom.transform.localPosition;
                    pos = new Vector3(pos.x + widthInteral, pos.y, pos.z);
                    touCardView = this.allHandView.selfView.touCardView;
                    break;
                case LOC_RIGHT:
                    pos = this.headView.right.transform.localPosition;
                    pos = new Vector3(pos.x - widthInteral, pos.y, pos.z);
                    movetime = 0.2f;
                    touCardView = this.allHandView.rightView.touCardView;
                    break;
                case LOC_TOP:
                    pos = this.headView.top.transform.localPosition;
                    touCardView = this.allHandView.topView.touCardView;
                    movetime = 0.1f;
                    break;
                case LOC_LEFT:

                    pos = this.headView.left.transform.localPosition;
                    pos = new Vector3(pos.x + widthInteral, pos.y, pos.z);
                    movetime = 0.2f;
                    touCardView = this.allHandView.leftView.touCardView;
                    break;
            }

            touCardView.transform.DOLocalMove(pos, movetime).OnComplete(() => { toupaiMoveOver(index); });
        }

        /// <summary>
        /// 刷新某人的手牌区
        /// </summary>
        /// <param name="index"></param>
        /// <param name="card"></param>
        public void refreshHandCards(int index, int[] cards)
        {
            switch (getPlayerIndex(index))
            {
                case LOC_DOWN:
                    this.allHandView.selfView.getReplayHandView().showHandCard(cards);
                    break;
                case LOC_RIGHT:
                    this.allHandView.rightView.getReplayHandView().showHandCard(cards);
                    break;
                case LOC_TOP:
                    this.allHandView.topView.getReplayHandView().showHandCard(cards);
                    break;
                case LOC_LEFT:
                    this.allHandView.leftView.getReplayHandView().showHandCard(cards);
                    break;
            }
        }

        public void refreshRePlayHandCards(int index, int[] cards)
        {
            switch (getPlayerIndex(index))
            {
                case LOC_DOWN:
                    this.allHandView.selfView.getReplayHandView().showHandCard(cards);
                    break;
                case LOC_RIGHT:
                    this.allHandView.rightView.getReplayHandView().showHandCard(cards);
                    break;
                case LOC_TOP:
                    this.allHandView.topView.getReplayHandView().showHandCard(cards);
                    break;
                case LOC_LEFT:
                    this.allHandView.leftView.getReplayHandView().showHandCard(cards);
                    break;
            }

        }

        public void showOperates(int[] operates,int paidui)
        {
            for (int i = 0; i < operates.Length; i++)
            {
                switch (getPlayerIndex(i))
                {
                    case LOC_DOWN:
                        this.bottom_operate.showButton(operates[i], paidui);
                        break;
                    case LOC_RIGHT:
                        this.right_operate.showButton(operates[i], paidui);
                        break;
                    case LOC_TOP:
                        this.top_operate.showButton(operates[i], paidui);
                        break;
                    case LOC_LEFT:
                        this.left_operate.showButton(operates[i], paidui); 
                        break;
                }
            }
        }

        public void hideOperates()
        {
            this.bottom_operate.hideAllBtn();
            this.right_operate.hideAllBtn();
            this.top_operate.hideAllBtn();
            this.left_operate.hideAllBtn();
        }

        /// <summary>
        /// 刷新所有玩家的手牌
        /// </summary>
        /// <param name="index"></param>
        public void refreshAllHandCards(int index)
        {
            for (int i = 0; i < Room.room.getPlayerCount(); i++)
            {
                int[] cards = CPMatch.match.getPCards()[index].handcards.toArray();

                switch (getPlayerIndex(index))
                {
                    case LOC_DOWN:
                        this.allHandView.selfView.getReplayHandView().showHandCard(cards);
                        
                        break;
                    case LOC_RIGHT:
                        this.allHandView.rightView.getReplayHandView().showHandCard(cards);
                        break;
                    case LOC_TOP:
                        this.allHandView.topView.getReplayHandView().showHandCard(cards);
                        break;
                    case LOC_LEFT:
                        this.allHandView.leftView.getReplayHandView().showHandCard(cards);
                        break;
                }

                index = getIndex(index);
            }
        }


        /// <summary>
        /// 刷新玩家的出牌区
        /// </summary>
        /// <param name="index"></param>
        public void refreshAllDisCard(int index)
        {
            for (int i = 0; i < Room.room.getPlayerCount(); i++)
            {
                int[] cards = CPMatch.match.getPCards()[index].getDisableCard().toArray();

                switch (getPlayerIndex(i))
                {
                    case LOC_DOWN:
                        this.allHandView.selfView.disableView.setCards(cards);
                        this.allHandView.selfView.disableView.refresh();
                        break;
                    case LOC_RIGHT:
                        this.allHandView.rightView.disableView.setCards(cards);
                        this.allHandView.rightView.disableView.refresh();
                        break;
                    case LOC_TOP:
                        this.allHandView.topView.disableView.setCards(cards);
                        this.allHandView.topView.disableView.refresh();
                        break;
                    case LOC_LEFT:
                        this.allHandView.leftView.disableView.setCards(cards);
                        this.allHandView.leftView.disableView.refresh();
                        break;
                }

                index = getIndex(index);
            }
        }

        public void doReplay()
        {
            if (CPMatch.match == null) return;
            this.replay.doOrder();
        }


        public override void refreshFuShu()
        {

            Rule rule = CPMatch.match.getRoomRule().rule;
            this.headView.bottom.setFushu(CPMatch.match.getSelfPlayerCards<CPPlayerCards>().getFushu(CPMatch.match.isXiaoJia(), rule.sid, rule.getRuleAtribute("is5red5black")));
        }


        public void controlReplay(int type)
        {
            rcview.controlReplay(type, this.replay);
        }

    }
}
