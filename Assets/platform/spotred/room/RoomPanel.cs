using basef.rule;
using cambrian.common;
using DG.Tweening;
using scene.game;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace platform.spotred.room
{
    public class RoomPanel:UnrealLuaPanel
    {
        /// <summary>
        /// 下,右，上,左
        /// </summary>
        public const int LOC_DOWN = 0, LOC_RIGHT = 1, LOC_TOP = 2, LOC_LEFT = 3;
        /// <summary>
        /// 顶部视图
        /// </summary>
        private TopBaseInfoView topBaseInfo;
        /// <summary>
        /// 规则view
        /// </summary>
        protected RuleView ruleview;

        protected RecvOperateTimer recvtimer;
        /// <summary>
        /// 中间的翻牌区
        /// </summary>
        protected CardBoxView cardBoxView;

        protected CardBoxCardView boxCardView;
        /// <summary>
        /// 动画视图
        /// </summary>
        protected MovieClipView movieClipView;
        /// <summary>
        /// 桌面背景
        /// </summary>
        protected Image deskbg;
        /// <summary>
        /// 等待其他玩家操作
        /// </summary>
        protected Image waitplayeroperate;
        /// <summary>
        /// 头像
        /// </summary>
        [HideInInspector] public HeadView headView;
        /// <summary>
        /// 所有人牌的操作区域
        /// </summary>
        [HideInInspector] public AllHandView allHandView;

        //---------------------data------------------------
        /// <summary>
        /// 翻的牌或者,别人打的牌
        /// </summary>
        int pm_card = Card.NO_CARD;
        /// <summary>
        /// 玩家的操作
        /// </summary>
        public int operate = Card.NO_CARD;
        /// <summary>
        /// 点的操作状态（针对于吃：1，出牌：2）
        /// </summary>
        public int stauts = -1;

        protected override void xinit()
        {
            base.xinit();
            this.topBaseInfo=this.content.Find("top").GetComponent<TopBaseInfoView>();
            this.topBaseInfo.init();

            this.ruleview = this.content.Find("ruleview").GetComponent<RuleView>();
            this.ruleview.init();
            this.ruleview.setVisible(false);

            this.cardBoxView = this.content.Find("hand").Find("indexcenter").GetComponent<CardBoxView>();
            this.cardBoxView.init();
            this.boxCardView = this.content.Find("hand").Find("centercard").GetComponent<CardBoxCardView>();
            this.boxCardView.init();

            this.deskbg = this.content.Find("bg").GetComponent<Image>();

            this.movieClipView = this.content.Find("movieclip").GetComponent<MovieClipView>();
            this.movieClipView.init();
           
            if (this.content.Find("hand").Find("loc0").Find("text_info") != null)
                this.waitplayeroperate = this.content.Find("hand").Find("loc0").Find("text_info").GetComponent<Image>();
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            this.topBaseInfo.refresh();

            this.cardBoxView.refresh();
            this.boxCardView.refresh();

            this.movieClipView.setVisible(false);

            if (this.waitplayeroperate != null)
                this.waitplayeroperate.gameObject.SetActive(false);
        }

        /// <summary>
        /// 刷新牌的数量
        /// </summary>
        public void refreshCardNum()
        {
            this.cardBoxView.setCardNum(CPMatch.match.paidui);
        }

        public virtual void refreshFuShu()
        {
            CPMatch match = CPMatch.match;
            if (match == null) return;
            Rule rule = match.getRoomRule().rule;
            for (int i = 0; i < Room.room.players.Length; i++)
            {
                switch (getPlayerIndex(i))
                {
                    case LOC_DOWN:
                        if (rule.sid == 1005) //巴中打大
                            this.headView.bottom.setFushu(match.getSelfPlayerCards<CPPlayerCards>().getFuShuDaDa(match.isXiaoJia()));
                        else
                            this.headView.bottom.setFushu(match.getSelfPlayerCards<CPPlayerCards>().getFushu(match.isXiaoJia(), rule.sid, rule.getRuleAtribute("is5red5black")));
                        break;
                    case LOC_RIGHT:
                        if (rule.sid == 1005) //巴中打大
                            this.headView.right.setFushu(match.getPlayerCardIndex<CPPlayerCards>(i).getFixedCardsFuShuDaDa(match.isXiaoJia(i)));
                        else
                            this.headView.right.setFushu(match.getPlayerCardIndex<CPPlayerCards>(i).getFixedCardsFushu(match.isXiaoJia(i),rule.sid));
                        break;
                    case LOC_TOP:
                        if (rule.sid == 1005) //巴中打大
                            this.headView.top.setFushu(match.getPlayerCardIndex<CPPlayerCards>(i).getFixedCardsFuShuDaDa(match.isXiaoJia(i)));
                        else
                            this.headView.top.setFushu(match.getPlayerCardIndex<CPPlayerCards>(i).getFixedCardsFushu(match.isXiaoJia(i), rule.sid));
                        break;
                    case LOC_LEFT:
                        if (rule.sid == 1005) //巴中打大
                            this.headView.left.setFushu(match.getPlayerCardIndex<CPPlayerCards>(i).getFixedCardsFuShuDaDa(match.isXiaoJia(i)));
                        else
                            this.headView.left.setFushu(match.getPlayerCardIndex<CPPlayerCards>(i).getFixedCardsFushu(match.isXiaoJia(i), rule.sid));
                        break;
                }
            }
        }

        /// <summary>
        /// 显示报牌图标
        /// </summary>
        /// <param name="index"></param>
        public void refreshBaoPaiIndex(int index)
        {
            this.allHandView.refreshBaoPai(getPlayerIndex(index));
        }
        /// <summary>
        /// 重连的时候刷新
        /// </summary>
        public void refreshBaoPai()
        {
            PlayerCards[] cards = CPMatch.match.getPlayerCardss<PlayerCards>();
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i].hasStatus(CPPlayerCards.STATUS_BAOPAI))
                {
                    refreshBaoPaiIndex(i);
                }
            }
        }

        /// <summary>
        /// 重连时刷新飘
        /// </summary>
        public void refreshPiao()
        {
            PlayerCards[] cards = CPMatch.match.getPlayerCardss<PlayerCards>();
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i].hasStatus(CPPlayerCards.STATUS_PIAO))
                {
                    this.headView.showPiao(getPlayerIndex(i),true);
                }
                else
                {
                    this.headView.showPiao(getPlayerIndex(i), false);
                }
            }
        }


        /// <summary>
        /// 刷新闹钟位置
        /// </summary>
        /// <param name="index"></param>
        public void refreshClock(int index)
        {
            this.allHandView.refreshClock(getPlayerIndex(index));
        }

        /// <summary>
        /// 刷新玩家的出牌区
        /// </summary>
        /// <param name="index"></param>
        public void refreshDisCard(int index)
        {
            if (index < 0) return;
            int[] cards = CPMatch.match.getPlayerCardss<CPPlayerCards>()[index].getDisableCard().toArray();
            this.allHandView.refreshDisCard(getPlayerIndex(index), cards);
        }

        /// <summary>
        /// 刷新自己的手牌
        /// </summary>
        /// <param name="disablecards"></param>
        public void refreshHandCard(int[] disablecards)
        {
            this.allHandView.selfView.getHandView().showHandCard(CPMatch.match.getSelfHandCard(), disablecards);
        }

        /// <summary>
        /// 刷新玩家的固定牌区
        /// </summary>
        /// <param name="index"></param>
        public void refreshFixed(int index)
        {
            if (index < 0) return;
            int len = Room.room.getPlayerCount();
            for (int i = 0; i < len; i++)
            {
                FixedCards[] cards = CPMatch.match.getPlayerCardss<CPPlayerCards>()[index].fixCards.toArray();
                this.allHandView.refreshFixed(getPlayerIndex(index), cards);
                index = getIndex(index);
            }
        }
        /// <summary>
        /// 刷新玩家需要移动的固定牌
        /// </summary>
        /// <param name="index"></param>
        public void refreshMoveFixed(int index, Action obj)
        {
            if (index < 0) return;
            FixedCards[] cards = CPMatch.match.getPlayerCardss<CPPlayerCards>()[index].fixCards.toArray();
            this.allHandView.refreshMoveFixed(getPlayerIndex(index), cards, obj);
        }
        /// <summary>
        /// 推挡
        /// </summary>
        /// <param name="index"></param>
        public void swtichDangIcon(int index)
        {
            this.headView.bottom.hideDang();

            this.headView.right.hideDang();

            this.headView.top.hideDang();

            this.headView.left.hideDang();

            switch (getPlayerIndex(index))
            {
                case LOC_DOWN:
                    this.headView.bottom.showDang();
                    break;
                case LOC_RIGHT:
                    this.headView.right.showDang();
                    break;
                case LOC_TOP:
                    this.headView.top.showDang();
                    break;
                case LOC_LEFT:
                    this.headView.left.showDang();
                    break;
            }
        }

        public void showTextinfo(bool b)
        {
            this.waitplayeroperate.gameObject.SetActive(b);
        }

        /// <summary>
        /// 显示打,吃,招碰,招吃,吃吐...
        /// </summary>
        public void showCard(int index, int card, int type)
        {
            this.allHandView.showCard(getPlayerIndex(index), card, type);
        }

        /// <summary>
        /// 显示某人打的牌
        /// </summary>
        /// <param name="index"></param>
        /// <param name="card"></param>
        public void showPlayCard(int index, int card, bool isAnamition)
        {
            this.allHandView.showPlayCard(getPlayerIndex(index), card, isAnamition);
        }

        /// <summary>
        /// 隐藏某人打的牌
        /// </summary>
        /// <param name="index"></param>
        public void hidePlayCard(int index)
        {
            this.allHandView.hidePlayCard(getPlayerIndex(index));
        }

        /// <summary>
        /// 隐藏所有人打的牌
        /// </summary>
        public void hideAllPlayCard()
        {
            this.allHandView.hideAllPlayCard();
        }
        /// <summary>
        /// 显示胡
        /// </summary>
        /// <param name="index"></param>
        public void showHu(int index)
        {
            this.allHandView.showHu(getPlayerIndex(index));
        }
        /// <summary>
        /// 显示比赛玩家信息
        /// </summary>
        public void ShowMatchPlayerInfo()
        {
            CPMatch match = CPMatch.match;
            this.headView.ShowAllMatchPlayer(Room.room, match.dangjia, match.xiaojia);
        }
        /// <summary>
        /// 隐藏胡
        /// </summary>
        public void hideHu()
        {
            this.allHandView.hideHu();
        }

        /// <summary>
        /// 隐藏玩家的出牌区最后一张牌
        /// </summary>
        /// <param name="index"></param>
        public void hideDisLastCard(int index)
        {
            if (index < 0) return;
            int[] cards = CPMatch.match.getPlayerCardss<CPPlayerCards>()[index].getDisableCard().toArray();
            this.allHandView.hideDisLastCard(getPlayerIndex(index), cards);
        }

        /// <summary>
        /// 关闭自己的闹钟
        /// </summary>
        public void closeSelfClock()
        {
            this.allHandView.selfView.isShow(false,false);
        }

        //--------------------data---------------------
        /// <summary>
        /// 获取自己的手牌
        /// </summary>
        /// <returns></returns>
        public int[] getSelfHandCard()
        {
            return CPMatch.match.getSelfHandCard();
        }
        public bool havOperate(int type)
        {
            if ((operate & type) == type)
                return true;
            return false;
        }

        public virtual void setOperate(int operate)
        {
            this.operate = operate;
            if (havOperate(OperateView.CAN_DISCARD))
                this.stauts = PlayCardProcess_1.PLAY;
        }

        /// <summary>
        /// 设置出牌或者翻牌
        /// </summary>
        /// <param name="pm_card"></param>
        public void setPMCard(int pm_card)
        {
            this.pm_card = pm_card;
        }

        public int getPMCard()
        {
            return this.pm_card;
        }
        /// <summary>
        /// 增加固定牌
        /// </summary>
        /// <param name="index"></param>
        /// <param name="type"></param>
        /// <param name="card"></param>
        public void addFixed(int index, int type, int[] card)
        {
            CPMatch.match.getPlayerCardss<CPPlayerCards>()[index].addFixCard(type,card);
        }

        /// <summary>
        /// 增加操作
        /// </summary>
        /// <param name="operate"></param>
        public void addRecvOperate(BaseOperate operate)
        {
            recvtimer.addOperate(operate);
        }

        public void setOperateTimer(RecvOperateTimer.ActionBack action, ByteBuffer data)
        {
            recvtimer.setAction(action, data);
        }

        /// <summary>
        /// 清空接收操作队列
        /// </summary>
        public void clearBaseOperate()
        {
            if (this.recvtimer != null)
                this.recvtimer.clearBaseOperate();
        }

        /// <summary>
        /// 获取相对于自己位置玩家的索引
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int getPlayerIndex(int index)
        {
            int len = Room.room.roomRule.rule.playerCount;
            int pos = ((index + len) - Room.room.indexOf()) % len;
            switch (len)
            {
                case 4:
                    break;
                case 3:
                    if (pos == 2)
                        pos = LOC_LEFT;
                    break;
                case 2:
                    if (pos == 1)
                        pos = LOC_TOP;
                    break;
            }
            return pos;
        }


        protected int getIndex(int index)
        {
            if (index == (Room.room.getPlayerCount() - 1))
                index = 0;
            else
                index++;
            return index;
        }

        /// <summary>
        /// 隐藏翻开的牌
        /// </summary>
        public void hideFanCard()
        {
            boxCardView.getOpenCard().gameObject.SetActive(false);
        }

        /// <summary>
        /// 显示规则详细信息
        /// </summary>
        public void showRuleView()
        {
            this.ruleview.refresh();
            this.ruleview.setVisible(true);
        }

        /// <summary>
        /// 显示电量
        /// </summary>
        /// <param name="str"></param>
        public void showDianliang(string str)
        {
            this.topBaseInfo.showDianliang(str);
        }

        /// <summary>
        /// 设置信号强度  1-4
        /// </summary>
        /// <param name="num"></param>
        public void showXinhao(string str)
        {
           this.topBaseInfo.showXinhao(str);
        }

        /// <summary>
        /// 播放动画
        /// </summary>
        /// <param name="index"></param>
        /// <param name="type"></param>
        public void playAnimation(int index, int type)
        {
            this.movieClipView.setVisible(true);
            switch (getPlayerIndex(index))
            {
                case LOC_DOWN:
                    this.movieClipView.playMovieClip(type, LOC_DOWN);
                    break;
                case LOC_RIGHT:
                    this.movieClipView.playMovieClip(type, LOC_RIGHT);
                    break;
                case LOC_TOP:
                    this.movieClipView.playMovieClip(type, LOC_TOP);
                    break;
                case LOC_LEFT:
                    this.movieClipView.playMovieClip(type, LOC_LEFT);
                    break;
            }
        }

        //--------------------动画播放----------------------------------------
        //偷拍动画
        public delegate void TouCardOverAction(bool b);

        TouCardOverAction toucardOverAction;

        private int toucard;
        /// <summary>
        /// 显示偷牌
        /// </summary>
        /// <param name="card"></param>
        /// <param name="index"></param>
        public void showTouCardAnimation(int card, int index, TouCardOverAction action)
        {
            this.toucardOverAction = action;
            this.toucard = card;
            Image backcard = boxCardView.getbackCard();
            Image cardbg = boxCardView.getCardBg();
            backcard.gameObject.SetActive(false);


            DisplayKit.setLocalRoateXYZ(backcard.transform, 0, 0, 180);

            DisplayKit.setLocalRoateXYZ(cardbg.transform, 0, 0, 0);

            DisplayKit.setLocalScaleXY(cardbg.transform, 0.4f);
            cardbg.gameObject.SetActive(true);

            cardbg.transform.DOScale(new Vector3(1, 1), 0.3f).OnComplete(() => { toupaiScale(index); });
        }

        public void toupaiScale(object obj)
        {
            this.boxCardView.getbackCard().gameObject.SetActive(true);
            touRoateOver(obj);
        }

        /// <summary>
        /// 偷拍旋转后移动时间
        /// </summary>
        float touPaiMoveTime = 0.15f;

        public void touRoateOver(object obj)
        {
            Image backcard = boxCardView.getbackCard();
            Image cardbg = boxCardView.getCardBg();

            int index = (int)obj;
            DisplayKit.setLocalScaleXY(cardbg.transform, 1);
            cardbg.gameObject.SetActive(false);

            DisplayKit.setLocalRoateXYZ(cardbg.transform, 0, 0, 0);

            DisplayKit.setLocalXY(backcard.transform, 0, 0);

            Vector3 pos = Vector3.zero;
            Vector3 p = new Vector3(0, 90, 0);
            //float movetime = 0.3f;

            switch (getPlayerIndex(index))
            {
                case LOC_DOWN:

                    pos = this.allHandView.selfView.getTouCardViewLocation() - p;
                    break;
                case LOC_RIGHT:
                    float width = this.allHandView.rightView.transform.parent.parent.GetComponent<RectTransform>().rect.width;
                    p.x -= (width / 2 - 568);
                   // movetime = 0.3f;
                    pos = this.allHandView.rightView.getTouCardViewLocation() - p;
                    break;
                case LOC_TOP:
                    pos = this.allHandView.topView.getTouCardViewLocation() - p;
                    break;
                case LOC_LEFT:
                    float w1 = this.allHandView.rightView.transform.parent.parent.GetComponent<RectTransform>().rect.width;
                    p.x += (w1 / 2 - 568);
                   // movetime = 0.3f;
                    pos = this.allHandView.leftView.getTouCardViewLocation() - p;

                    break;
            }

            Sequence s = DOTween.Sequence();
            s.Append(backcard.transform.DOLocalMove(pos, touPaiMoveTime).SetEase(Ease.Linear));
            s.Insert(0, backcard.transform.DOLocalRotate(new Vector3(0, 0, 90), touPaiMoveTime));

            s.OnComplete(() =>
            {
                toupaimoveover(index);
            });
        }

        public void toupaimoveover(object obj)
        {
            showTouCard((int)obj);
            this.boxCardView.getbackCard().gameObject.SetActive(false);
        }

        /// <summary>
        /// 显示偷的牌
        /// </summary>
        /// <param name="index"></param>
        /// <param name="card"></param>
        public void showTouCard(int index)
        {
            this.allHandView.hideTouCard();
            this.allHandView.setTouCard(getPlayerIndex(index), toucard, index, moveheadView);
        }

        public virtual void moveheadView(int index)
        {
            Vector3 pos = Vector3.zero;
            float widthInteral = this.headView.GetComponent<RectTransform>().rect.width / 2 - 568;
            float movetime = 0.15f;//移动时间
            TouCardView touCardView = null;
            switch (getPlayerIndex(index))
            {
                case LOC_DOWN:
                    pos = this.allHandView.selfView.getHandView().transform.localPosition;
                    pos = new Vector3(0, pos.y, 0);
                    touCardView = this.allHandView.selfView.touCardView;
                    break;
                case LOC_RIGHT:
                    pos = this.headView.right.transform.localPosition;
                    pos = new Vector3(pos.x - widthInteral, pos.y, pos.z);
                    movetime = 0.15f;
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
                    movetime = 0.15f;
                    touCardView = this.allHandView.leftView.touCardView;
                    break;
            }
            touCardView.transform.DOLocalMove(pos, movetime).OnComplete(() => { toupaiMoveOver(index); });
        }

        /// <summary>
        /// 偷拍移动结束
        /// </summary>
        protected void toupaiMoveOver(object obj)
        {
            this.allHandView.hideTouCard();
            toucardOverAction(true);
        }

        //翻牌动画
        public delegate void ShowFanCardOverAction(bool b);
        ShowFanCardOverAction showFancardOverAction;
        /// <summary>
        /// 显示翻牌
        /// </summary>
        /// <param name="card"></param>
        /// <param name="index"></param>
        public void showFanCard(int card, int index, ShowFanCardOverAction action)
        {
            this.showFancardOverAction = action;

            Image opencard = boxCardView.getOpenCard();
            Image cardbg = boxCardView.getCardBg();
            opencard.sprite = WidgetManager.getInstance().getHandCard(card);
            opencard.gameObject.SetActive(false);

            DisplayKit.setLocalRoateXYZ(opencard.transform, 0, 0, 180);

            DisplayKit.setLocalRoateXYZ(cardbg.transform, 0, 0, 0);

            DisplayKit.setLocalScaleXY(cardbg.transform, 0.4f);
            cardbg.gameObject.SetActive(true);

            cardbg.transform.DOScale(new Vector3(1, 1), 0.1f).OnComplete(() => { fanPaiScale(index); });
        }

        public void fanPaiScale(object obj)
        {
            this.boxCardView.getOpenCard().gameObject.SetActive(true);
            fanRoateOver(obj);
        }
        /// <summary>
        /// 翻牌选择后移动时间
        /// </summary>
        float fanPaiMoveTime = 0.15f;
        public virtual void fanRoateOver(object obj)
        {
            Image opencard = boxCardView.getOpenCard();
            Image cardbg = boxCardView.getCardBg();

            int index = (int)obj;
            DisplayKit.setLocalScaleXY(cardbg.transform, 1);
            cardbg.gameObject.SetActive(false);

            DisplayKit.setLocalRoateXYZ(cardbg.transform, 0, 0, 0);

            DisplayKit.setLocalXY(opencard.transform, 0, 0);

            Vector3 pos = Vector3.zero;

            Vector3 p = new Vector3(0, 90, 0);

            switch (getPlayerIndex(index))
            {
                case LOC_DOWN:
                    pos = this.allHandView.selfView.getPlayCardViewLocation() - p;
                    break;
                case LOC_RIGHT:
                    float width = this.allHandView.rightView.transform.parent.parent.GetComponent<RectTransform>().rect.width;
                    p.x -= (width / 2 - 568);
                   // movetime = 0.5f;
                    pos = this.allHandView.rightView.getPlayCardViewLocation() - p;
                    break;
                case LOC_TOP:
                    pos = this.allHandView.topView.getPlayCardViewLocation() - p;
                    break;
                case LOC_LEFT:
                    float w1 = this.allHandView.rightView.transform.parent.parent.GetComponent<RectTransform>().rect.width;
                    p.x += (w1 / 2 - 568);

                   // movetime = 0.5f;
                    pos = this.allHandView.leftView.getPlayCardViewLocation() - p;

                    break;
            }

            Sequence s = DOTween.Sequence();
            s.Append(opencard.transform.DOLocalMove(pos, fanPaiMoveTime).SetEase(Ease.Linear));
            s.Insert(0, opencard.transform.DOLocalRotate(new Vector3(0, 0, 90), fanPaiMoveTime));

            s.OnComplete(() =>
            {
                fanpaimoveover(index);
            });
        }

        /// <summary>
        /// 翻牌动画结束
        /// </summary>
        /// <param name="obj"></param>
        public void fanpaimoveover(object obj)
        {
            showPlayCard((int)obj, getPMCard(), false);
            hideFanCard();
            if (showFancardOverAction != null)
                showFancardOverAction(true);
        }

        //-----------------将牌移动到出牌区-------------------------
        private Action<bool> moveShowCardDisBack;
        /// <summary>
        /// 移动到弃牌区时间
        /// </summary>
        float toDisableMoveTime = 0.15f;

        /// <summary>
        /// 移动显示的牌到不可出牌区
        /// </summary>
        /// <param name="index"></param>
        public void moveShowCardToDisable(int index, Action<bool> action)
        {
            PlayCardView imtr = null;
            Vector3 roate = Vector3.zero;//旋转的角度
            Vector3 targetpos = Vector3.zero;
            switch (getPlayerIndex(index))
            {
                case LOC_DOWN:
                    imtr = this.allHandView.selfView.playCardView;
                    roate = new Vector3(0, 0, -145);
                    targetpos = this.allHandView.selfView.disableView.getLastCardPos();
                    break;
                case LOC_RIGHT:
                    imtr = this.allHandView.rightView.playCardView;
                    roate = new Vector3(0, 0, -24);
                    targetpos = this.allHandView.rightView.disableView.getLastCardPos();
                    break;
                case LOC_TOP:
                    imtr = this.allHandView.topView.playCardView;
                    roate = new Vector3(0, 0, 24);
                    targetpos = this.allHandView.topView.disableView.getLastCardPos();
                    break;
                case LOC_LEFT:
                    imtr = this.allHandView.leftView.playCardView;
                    roate = new Vector3(0, 0, 126);
                    targetpos = this.allHandView.leftView.disableView.getLastCardPos();
                    break;
            }
            moveShowCardDisBack = action;
            imtr.setVisible(true);
            Sequence s = DOTween.Sequence();
            s.Append(imtr.transform.DOLocalMove(targetpos, toDisableMoveTime).SetEase(Ease.Linear));
            s.Insert(0, imtr.transform.DOLocalRotate(roate, toDisableMoveTime));
            s.Insert(0, imtr.transform.DOScaleX(0.6f, toDisableMoveTime));
            s.Insert(0, imtr.transform.DOScaleY(0.4f, toDisableMoveTime));

            s.OnComplete(() =>
            {
                cardMoveOver(index);
            });
        }

        public void cardMoveOver(object obj)
        {
            int index = (int)obj;
            if (index < 0) return;
            hidePlayCard(index);
            this.allHandView.refreshDisable(getPlayerIndex(index));
            moveShowCardDisBack(true);
        }
    }
}
