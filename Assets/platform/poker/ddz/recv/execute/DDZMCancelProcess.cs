using UnityEngine;

namespace platform.poker
{
    public class DDZMCancelProcess : Process
    {
        DDZMCancelOperate operate;

        public DDZMCancelProcess(object operate)
        {
            this.operate = (DDZMCancelOperate)operate;
        }

        public override void execute()
        {
            var match = DDZMatch.match;
            var panel = UnrealRoot.root.getDisplayObject<PDDZRoomPanel>();
            match.setStage(operate.operateData.stage);
            match.paidui = operate.operateData.paidui;
            match.step = operate.operateData.step;
            switch (operate.type)
            {
                case PKCancelType.Landlord_Call_NO:
                    new DDZMLandlordCallProcess(operate.operateData,operate.index).execute();
                    break;
                case PKCancelType.Landlord_Grab_NO:
                    new DDZMLandlordGrabProcess(operate.operateData,operate.index).execute();
                    break;
                case PKCancelType.JIABEI_NO:
                    new DDZMJiaBeiProcess(operate.operateData,operate.index).execute();
                    break;
                case PKCancelType.PASS:
                    new DDZMShowCardProcess(operate.operateData,operate.index).execute();
                    break;
                case PKCancelType.CALLSCORE:
                    new DDZMCallScoreProcess(operate.operateData,operate.index).execute();
                    break;
            }
            operate.playOver();
            panel.gameView.operate.showOperateView(panel.operate);
        }
    }
}
