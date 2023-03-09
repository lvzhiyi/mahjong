namespace platform.poker
{
    /// <summary> 提示出牌 </summary>
    public class PDKTenHintShowHandProcess : MouseClickSlideProcess
    {
        public static int num = 1;

        PDKTenMatch match;

        public override void mouseClick()
        {
            match = PDKTenMatch.match;
            int[] cards = match.tips.getTip(num);
            if (cards == null || cards.Length == 0)
            {
                if (cards == null)
                {
                    num = 1;
                    cards = match.tips.getTip(num);
                }
            }
            PKRoomPanel.Panel.gameView.hand.tipsCards(cards);
            num++;
        }
    }
}
