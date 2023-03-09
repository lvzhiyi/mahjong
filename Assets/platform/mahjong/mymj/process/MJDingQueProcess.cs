using cambrian.common;

namespace platform.mahjong
{
    public class MJDingQueProcess:MouseClickProcess
    {
        /// <summary>
        /// 筒，条，万
        /// </summary>
        public static int TONG = 0, TIAO = 1, WAN = 2;

        public int type;

        public override void execute()
        {
            int distype = 0;
            switch (type)
            {
                case 0:
                    distype = MJCard.TYPE_DOT;
                    break;
                case 1:
                    distype = MJCard.TYPE_BAM;
                    break;
                case 2:
                    distype = MJCard.TYPE_CHA;
                    break;
            }

            int step = MJMatch.match.step;

            MJMatch.match.getSelfPlayerCards<MJPlayerCards>().distype = distype;

            CommandManager.addCommand(new SendMJMatchCommand(SendMJMatchCommand.DISTYPE,step,distype));
        }
    }
}
