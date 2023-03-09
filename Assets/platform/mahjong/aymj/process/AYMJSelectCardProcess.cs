using cambrian.common;
using cambrian.game;
using UnityEngine;
using UnityEngine.EventSystems;

namespace platform.mahjong
{
    /// <summary>
    /// 选择牌
    /// </summary>
    public class AYMJSelectCardProcess : MouseEventProcess
    {
        AYMJPlayerHandBar bar;

        AYMJRoomPanel panel;

        AYMJMatch match;

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
            if (bar == null || !MJOperate.isDisCard(operate) || playerCard == null) return;
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

        private AYMJHandDetailView detailView;

        /// <summary>
        /// 按下的位置
        /// </summary>
        private Vector2 mouseDown;

        Vector2 offset = new Vector3(); //用来得到鼠标和图片的差值

        private Vector2 init_postion;

        public override void OnPointerDown(PointerEventData eventData)
        {
            panel = getRoot<AYMJRoomPanel>();
            operate = panel.getOperate();
            if (!MJOperate.isDisCard(operate))
            {
                bar = null;
                return;
            }
            match = AYMJMatch.match;
            playerCard = match.getSelfPlayerCards<AYMJPlayerCards>();

            bar = GetComponent<AYMJPlayerHandBar>();

            if (MJCard.isSigned(bar.cardValue, MJCard.SIGN_TANG))
                return;
            if (!playerCard.isDingType(bar.getCardValue()) && playerCard.hasDingCard()) return;
            barRect = bar.GetComponent<RectTransform>();
            init_postion = barRect.anchoredPosition;
            detailView = bar.transform.parent.parent.GetComponent<AYMJHandDetailView>();
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


        private AYMJPlayerCards playerCard;

        public override void execute()
        {
            match = AYMJMatch.match;
            bar = GetComponent<AYMJPlayerHandBar>();
            panel = getRoot<AYMJRoomPanel>();

            operate = panel.getOperate();
            playerCard = match.getSelfPlayerCards<AYMJPlayerCards>();

            if (MJCard.isSigned(bar.cardValue, MJCard.SIGN_TANG))
                return;

            if (MJOperate.isCanReplace(operate))
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
            panel.refreshSingleHandCard(AYMJMatch.match.mindex);
            isMoveing = false;
            if (AYMJMatchDrawProcess.back != null)
                AYMJMatchDrawProcess.back();
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
            int step = AYMJMatch.match.step;
            CommandManager.addCommand(new SendMJMatchCommand(type, step, card));
            isChuCard = true;
        }

        /// <summary>
        /// 换张
        /// </summary>
        private void replace()
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
    }
}
