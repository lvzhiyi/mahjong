using cambrian.common;
using cambrian.game;
using UnityEngine;
using UnityEngine.EventSystems;

namespace platform.mahjong
{
    /// <summary>
    /// 选择牌
    /// </summary>
    public class MJSelectCardProcess : MouseEventProcess
    {
        MJPlayerHandBar bar;

        MahjongRoomPanel panel;

        MJMatch match;

        int operate;

        public RectTransform canvas; //得到canvas的ugui坐标

        /// <summary>
        /// 是否是出了牌，用于记录托管，时，是否需要移除该牌
        /// </summary>
        public static bool isChuCard = false;


        void Start()
        {
            canvas = transform.parent.parent.parent.parent.parent.GetComponent<RectTransform>();
        }

        public override void OnDrag(PointerEventData eventData)
        {
            if (bar == null || !MJOperate.isDisCard(operate) || playerCard == null || panel.tangOperate > -1) return;
            if (!playerCard.isDingType(bar.getCardValue()) && playerCard.hasDingCard()) return;
            if (MJCard.isSigned(bar.cardValue, MJCard.SIGN_TANG))
                return;
            Vector2 mouseDrag = eventData.position; //当鼠标拖动时的屏幕坐标
            Vector2 uguiPos = new Vector2(); //用来接收转换后的拖动坐标
            //和上面类似
            bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mouseDrag,
                eventData.enterEventCamera, out uguiPos);

            if (isRect)
                barRect.anchoredPosition = offset + uguiPos; //设置图片的ugui坐标与鼠标的ugui坐标保持不变
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.position.y < mouseDown.y)
            {
                barRect.anchoredPosition = init_postion;
                bar = null;
                return;
            }

            if (playerCard == null || bar == null || (!playerCard.isDingType(bar.getCardValue()) && playerCard.hasDingCard())) return;

            if (MJCard.isSigned(bar.cardValue, MJCard.SIGN_TANG))
                return;

            if (barRect != null && MJOperate.hasCanDisCard(operate))
            {
                barRect.anchoredPosition = init_postion;
                panel.gameView.getHandView().refreshSelfUpHandCard(-1);
                discard(bar.getCardValue());

                bar = null;
            }
        }


        private RectTransform barRect; //得到Bar的ugui坐标

        private MJHandDetailView detailView;

        /// <summary>
        /// 按下的位置
        /// </summary>
        private Vector2 mouseDown;

        Vector2 offset = new Vector3(); //用来得到鼠标和图片的差值

        private Vector2 init_postion;

        public override void OnPointerDown(PointerEventData eventData)
        {
            panel = getRoot<MahjongRoomPanel>();
            operate = panel.getOperate();
            if (!MJOperate.isDisCard(operate) || panel.tangOperate > -1)
            {
                bar = null;
                return;
            }
            match = MJMatch.match;
            playerCard = match.getSelfPlayerCards<MJPlayerCards>();

            bar = GetComponent<MJPlayerHandBar>();

            if (MJCard.isSigned(bar.cardValue, MJCard.SIGN_TANG))
                return;
            if (!playerCard.isDingType(bar.getCardValue()) && playerCard.hasDingCard()) return;
            barRect = bar.GetComponent<RectTransform>();
            init_postion = barRect.anchoredPosition;
            detailView = bar.transform.parent.parent.GetComponent<MJHandDetailView>();
            bar.setSlibingIndex(detailView.handContainer.size - 1);
            mouseDown = eventData.position; //记录鼠标按下时的屏幕坐标
            Vector2 mouseUguiPos = new Vector2(); //定义一个接收返回的ugui坐标
            bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mouseDown,
                eventData.enterEventCamera, out mouseUguiPos);
            if (isRect) //如果在
            {
                //计算图片中心和鼠标点的差值
                offset = barRect.anchoredPosition - mouseUguiPos;
            }
        }

        public override void OnPointerUp(PointerEventData eventData)
        {

        }


        private MJPlayerCards playerCard;

        public override void execute()
        {
            match = MJMatch.match;
            bar = GetComponent<MJPlayerHandBar>();
            panel = getRoot<MahjongRoomPanel>();

            operate = panel.getOperate();
            playerCard = match.getSelfPlayerCards<MJPlayerCards>();

            if (MJCard.isSigned(bar.cardValue, MJCard.SIGN_TANG))
                return;

            if (MJTangStage.isPlayStage(panel.tangOperate))//选择打牌阶段
            {
                tangCardPlay();
            }
            else if (MJTangStage.isTangCardsStage(panel.tangOperate))//选择躺牌阶段
            {
                tangCardSelect();
            }
            else if (MJOperate.isCanReplace(operate))
            {
                replace();
            }
            else if (MJOperate.isDisType(operate))
            {

            }
            else if (MJOperate.hasCanDisCard(operate))
            {
                if (!playerCard.isDingType(bar.getCardValue()) && playerCard.hasDingCard()) return;

                if (!bar.isSelect)
                {
                    panel.gameView.setTingView(bar.tinginfo);
                    panel.gameView.getHandView().refreshSelfUpHandCard(bar.index_space);
                    panel.refreshSelectCard(bar.getCardValue());
                    SoundManager.playMJEffect("card_click");
                    return;
                }
                else
                {
                    discard(bar.getCardValue());
                }
            }
            else
            {
                if (!bar.isSelect)
                {
                    panel.gameView.setTingView(null);
                    panel.gameView.getHandView().refreshSelfUpHandCard(bar.index_space);
                    panel.refreshSelectCard(bar.getCardValue());
                    SoundManager.playMJEffect("card_click");
                    return;
                }
                else
                {
                    panel.gameView.getHandView().refreshSelfUpHandCard(-1);
                    panel.refreshSelectCard(-1);
                }
            }
        }

        public void moMoveOver()
        {
            panel.refreshSingleHandCard(MJMatch.match.mindex);
            isMoveing = false;
            if (MJMatchDrawProcess.back != null)
                MJMatchDrawProcess.back();
        }

        public static bool isMoveing = false;

        public void discard(int card)
        {
            if (Room.room.getSelfMatchPlayer().isTrustee()) return;
            match.setRound(-1);
            panel.gameView.setTingView(null);
            panel.gameView.getOperateView().setVisible(false);
            panel.gameView.getHandView().refreshSelfUpHandCard(-1);
            panel.setOperate(0);
            int[] cardIndex = playerCard.getMoCardMoveToDestIndex(bar.getCardValue(), bar.index_space);
            if (cardIndex == null) //不需要移动
            {
                moMoveOver();
            }
            else
            {
                isMoveing = true;
                panel.gameView.getHandView().refreshMoMove(playerCard.handcards.toArray(), new MJMOCard(cardIndex[1]),
                    cardIndex[0], moMoveOver);
            }

            panel.gameView.showDisCardRedmine(false);

            int type = SendMJMatchCommand.DISCARD;
            int step = MJMatch.match.step;
            CommandManager.addCommand(new SendMJMatchCommand(type, step, card));
            isChuCard = true;
        }

        /// <summary>
        /// 换张
        /// </summary>
        private void replace()
        {
            var huansan = match.huangSanZhangIndex;
            var pcards = match.getPlayerCardIndex<MJPlayerCards>(match.mindex);
            var handcards = pcards.handcards;
            var b = false;

            var card1 = 0;
            var card2 = 0;
            var card3 = 0;
            if (huansan != null)
            {
                if (huansan[0] != -1 && huansan[1] != -1 && huansan[2] != -1)
                {
                    if (huansan[0] == handcards.count)
                        card1 = pcards.mocard;
                    else
                        card1 = handcards.get(huansan[0]);


                    if (huansan[1] == handcards.count)
                        card2 = pcards.mocard;
                    else
                        card2 = handcards.get(huansan[1]);


                    if (huansan[2] == handcards.count)
                        card3 = pcards.mocard;
                    else
                        card3 = handcards.get(huansan[2]);


                    var type1 = MJCard.getType(card1);
                    var type2 = MJCard.getType(card2);
                    var type3 = MJCard.getType(card3);
                    if (type1 == type2 && type1 == type3)
                        b = true;
                }
            }


            if (!bar.isSelect && b)
            {
                var index = bar.index_space;

                if (index == handcards.count)
                    card1 = pcards.mocard;
                else
                    card1 = handcards.get(index);


                var c = huansan[0];

                if (c != -1)
                {
                    if (huansan[0] == handcards.count)
                        card2 = pcards.mocard;
                    else
                        card2 = handcards.get(huansan[0]);


                    if (MJCard.getType(card1) != MJCard.getType(card2))
                        match.huangSanZhangIndex = null;

                }
                selecte();
            }
            else
                selecte();

        }

        private void selecte()
        {
            if (bar.isSelect)
            {
                match.deleteHuansanzhangIndex(bar.index_space);
            }
            else
            {
                match.addHuangSZIndex(bar.index_space);
            }

            panel.refreshHuanSZUpCard();
        }

        /// <summary>
        /// 躺牌操作
        /// </summary>
        private void tangCardPlay()
        {
            if (bar.tinginfo == null) return;
            MJPlayerCards playerCards = match.getSelfPlayerCards<MJPlayerCards>();
            playerCards.moveCardIndexToMoCard(bar.index_space);
            TingCardsInfo[] tinginfos = match.getSelfTingCards(match.mindex);
            panel.refreshHandCard(MJMatch.match.mindex, tinginfos, false);
            panel.gameView.showLieView(MJLieView.SELECT_LIE_CARD);
            panel.tangOperate = MJTangStage.SELECT_TANG_CARDS;//选择躺牌的阶段

            int[][] counts = Eliminator.getCounts(playerCards.handcards.toArray());

            //MJCard.getCardTypeCount()
            Eliminator.simplify(counts, Eliminator.checkOutListens(counts, MJCard.ALL_BITS), MJCard.ALL_BITS);
            int[] tangcard = Eliminator.getTang(counts);
            match.addTangCardsIndexs(playerCards.getTangCardIndex(tangcard));
            panel.refreshTangSelectUpCard();
        }

        private void tangCardSelect()
        {
            if (bar.index_space == match.getSelfHandCard().Length)
                return;
            if (bar.isSelect)
            {
                match.removeTangCardIndex(bar.index_space);
            }
            else
            {
                match.addTangCardIndex(bar.index_space);
            }

            panel.refreshTangSelectUpCard();
        }

    }
}
