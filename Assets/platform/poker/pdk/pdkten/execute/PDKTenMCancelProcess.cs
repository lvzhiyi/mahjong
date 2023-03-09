namespace platform.poker
{
    public class PDKTenMCancelProcess : Process
    {
        private PDKMCancelOperate operate;

        public PDKTenMCancelProcess(object operate)
        {
            this.operate = (PDKMCancelOperate)operate;
        }

        public override void execute()
        {
            switch (operate.type)
            {
                case PKCancelType.PASS:
                    new PDKTenMShowCardProcess(operate.operateData, operate.index).execute();
                    break;               
            }
            operate.playOver();
        }
    }
}
