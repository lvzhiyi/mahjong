namespace platform.poker
{
    public class DDZMFlowGameRecording : Process
    {
        private DDZMFlowGameOperate operate;

        public DDZMFlowGameRecording(object operate)
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