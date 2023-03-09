using basef.rule;

namespace platform.poker
{
    /// <summary> 提示出牌 </summary>
    public class PDKHintShowHandProcess : MouseClickSlideProcess
    {
        public static int num = 1;

        PDKMatch match;

        bool isplaneshow = false;

        public override void mouseClick()
        {
            match = PDKMatch.match;
            int[] cards = match.tips.getTip(num);


            if (cards == null || cards.Length == 0)
            {
                cards=planeLessShow();
                if (cards == null)
                {
                    num = 1;
                    cards = match.tips.getTip(num);
                }
                else
                {
                    if (!isplaneshow)
                        isplaneshow = true;
                    else
                    {
                        num = 1;
                        cards = match.tips.getTip(num);
                        isplaneshow = false;
                    }
                }
               
            }
            PKRoomPanel.Panel.gameView.hand.tipsCards(cards);
            num++;
        }

        /// <summary>
        /// 
        /// </summary>
        public int[] planeLessShow()
        {
            Rule rule = match.rule;
            PDKCardInfo cardInfo = match.deskCard.getLastCard();
            int[] cards = match.getSelfPlayerCards<PDKPlayerCards>().handcards.toArray();

            PDKCardInfo selfInfo = new PDKCardInfo(match.mindex, cards);

            bool aaaBoom = rule.getRuleAtribute("aaaisboom");

            bool hasBoom = selfInfo.counts[1] == 3 && aaaBoom;
            if (!hasBoom && !selfInfo.hasBoom()) return null;
            if (cards.Length > cardInfo.cards.Length)
                return null;


            bool chowAllForPlane = rule.getRuleAtribute("chowAllForPlane"); //飞机可少带接完
            bool chowAllForThree = rule.getRuleAtribute("chowAllForThree"); //飞机可少带接完
            if (chowAllForPlane && cardInfo.type == PKCardType.TYPE_CARDS_PLANE)
            {
                int[] array = getPlane(selfInfo.counts);
                if (array[0] * 5 > cards.Length && array[0] * 5 >= cardInfo.cards.Length)
                {
                    return cards;
                }
            }
            else if (chowAllForThree&& cardInfo.type == PKCardType.TYPE_3_2)
            {
                int[] array = getPlane(selfInfo.counts);
                if (array[0] * 5 > cards.Length && array[0] * 5 >= cardInfo.cards.Length&&cards.Length<5)
                {
                    return cards;
                }
            }

            return null;
        }

        public int[] getPlane(int[] counts)
        {
            int planenum = 0;//飞机的数量
            int planeIndex = -1;//飞机结束的索引
            int count = 0;

            bool b = false;
            for (int i = 3; i < counts.Length; i++)
            {
                if (i > 13)
                {
                    i -= 13;
                    b = true;
                }
                   
                count = counts[i];
                if (count >= 3)
                {
                    if (planeIndex == -1)
                    {
                        planeIndex = i;
                        planenum++;
                    }
                    else if (planeIndex + 1 == i||(i==1&&planeIndex==13))
                    {
                        planeIndex = i;
                        planenum++;
                    }
                    else if (planenum==1)
                    {
                        planeIndex = i;
                        planenum = 1;
                    }

                }
                if (b)
                {
                    break;
                }
            }
            return new int[] { planenum, planeIndex };
        }
    }
}
