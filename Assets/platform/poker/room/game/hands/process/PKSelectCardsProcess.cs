using cambrian.game;

namespace platform.poker
{
    /// <summary> 玩家点击自己手牌 </summary>
    public class PKSelectCardsProcess : MouseClickSlideProcess
    {
        /// <summary> 选择状态 </summary>
        private static bool Select;

        private static int StartIndex;

        private PKSelfHandBar bar;

        private void OnEnable()
        {
            bar = !bar ? transform.GetComponent<PKSelfHandBar>() : bar;
        }

        public override void mouseEnter()
        {
            if (Select)
            {
                PKRoomPanel.Panel.claerSelectHands();
                PKRoomPanel.Panel.gameView.hand.refreshSelectHands(StartIndex, bar.index_space);
            }
        }

        public override void mouseDown()
        {
            StartIndex = bar.index_space;
            bar.mask = true;
            bar.refresh();
            Select = true;
        }

        public override void mouseUp()
        {
            Select = false;
            PKRoomPanel.Panel.gameView.hand.resetMask();
            PKRoomPanel.Panel.gameView.hand.tipsCards(PKRoomPanel.Panel.tips);
            SoundManager.playPKOther(SoundManager.cardselect, 0);
        }
    }
}
