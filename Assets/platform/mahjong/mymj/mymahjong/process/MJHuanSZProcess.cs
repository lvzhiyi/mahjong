using cambrian.common;

namespace platform.mahjong
{
    public class MJHuanSZProcess:MouseClickProcess
    {
        public UnrealLuaSpaceObject space;

        public override void execute()
        {
            MJMatch match= MJMatch.match;
            int[] cards=match.getHuanSanZhangCards();

            if (cards==null||!match.replaceCardValid(cards)) return;
            
            MJPlayerCards playerCards=match.getSelfPlayerCards<MJPlayerCards>();

            ArrayList<int> handcards=new ArrayList<int>();
            handcards.add(playerCards.handcards.toArray());
            if (playerCards.mocard != MJCard.CNO)
            {
                handcards.add(playerCards.mocard);
            }

            bool b=false;
            for (int i = 0; i < cards.Length; i++)
            {
                b = false;
                for (int j = 0; j < handcards.count; j++)
                {
                    if (cards[i] == handcards.get(j))
                    {
                        handcards.removeAt(j);
                        b = true;
                        break;
                    }
                }

                if (!b)
                {
                    break;
                }
            }

            if (!b)
            {
                Alert.autoShow("请重新选中换牌");
                match.huangSanZhangIndex = null;
                MahjongRoomPanel panel= getRoot<MahjongRoomPanel>();
                panel.refreshHuanSZ(match.mindex);
                panel.refreshHuanSZUpCard();
                return;
            }

            int type = SendMJMatchCommand.REPLACE;
            int step = MJMatch.match.step;
            CommandManager.addCommand(new SendMJMatchCommand(type,step,cards));
            space.setVisible(false);
        }
    }
}
