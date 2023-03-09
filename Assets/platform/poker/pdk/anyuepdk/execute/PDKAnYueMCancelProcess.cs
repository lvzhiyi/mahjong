namespace platform.poker
{
    public class PDKAnYueMCancelProcess : Process
    {
        private PDKMCancelOperate operate;

        public PDKAnYueMCancelProcess(object operate)
        {
            this.operate = (PDKMCancelOperate)operate;
        }

        public override void execute()
        {
            switch (operate.type)
            {
                case PKCancelType.PASS:
                    new PDKAnYueMShowCardProcess(operate.operateData, operate.index).execute();
                    break;               
            }
            operate.playOver();
        }
    }
}
