using cambrian.common;
using cambrian.game;

namespace platform.poker
{
    public class PPDKRecorderView : PKRecorderView
    {
        protected override void xinit()
        {
            base.xinit();
            ByteBuffer data = FileCachesManager.loadPathData("pkrecorder", true);
            if (data == null) isShow = true;
            else isShow = data.readBoolean();
        }

        protected override void xrefresh()
        {
            if (Room.room == null) return;
            if (Room.room.roomRule.rule.getRuleAtribute("record"))
            {
                if (!Room.room.isStatus(Room.STATE_MATCH) || cardscount == null)
                {
                    claerCards();
                }
                for (int i = 0; i < cards.Length; i++)
                {
                    cards[i].text = cardscount[i].ToString();
                }
                setVisible(isShow);
            }
            else setVisible(false);
        }

        public override void setData(int[] cardscount)
        {
            this.cardscount = cardscount;
            xrefresh();
        }

        public override void showHideOff()
        {
            isShow = !isShow;
            ByteBuffer data = new ByteBuffer();
            data.writeBoolean(isShow);
            FileCachesManager.savePathData("pkrecorder", true, data);
        }
    }
}
