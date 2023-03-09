namespace platform.poker
{
    /// <summary> 提示出牌 </summary>
    public class DDZHintShowHandProcess : MouseClickSlideProcess
    {
        public static int num = 1;

        public override void mouseClick()
        {
            int[] cards = DDZMatch.match.tips.getTip(num);
            if (cards == null || cards.Length == 0)
            {
                num = 1;
                cards = DDZMatch.match.tips.getTip(num);
            }
            PKRoomPanel.Panel.gameView.hand.tipsCards(cards);
            num++;
        }
    }
}
