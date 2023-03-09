namespace platform.poker
{
    public class DDZMFlowGameProcess : Process
    {
        private DDZMFlowGameOperate operate;

        public DDZMFlowGameProcess(object operate)
        {
            this.operate = (DDZMFlowGameOperate)operate;
        }

        public override void execute()
        {
            operate.playOver();
            DDZMatch.match = null;
        }
    }
}