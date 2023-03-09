namespace platform.poker
{
    public class PDKMCancelProcess : Process
    {
        private PDKMCancelOperate operate;

        public PDKMCancelProcess(object operate)
        {
            this.operate = (PDKMCancelOperate)operate;
        }

        public override void execute()
        {
            switch (operate.type)
            {
                case PKCancelType.PASS:
                    new PDKMShowCardProcess(operate.operateData, operate.index).execute();
                    break;               
            }
            operate.playOver();
        }
    }
}
