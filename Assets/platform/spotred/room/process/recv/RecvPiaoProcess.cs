namespace platform.spotred.room
{
    /// <summary>
    /// 接收飘操作
    /// </summary>
    public class RecvPiaoProcess:Process
    {
        PiaoOperate operate;

        public RecvPiaoProcess(PiaoOperate operate)
        {
            this.operate = operate;
        }

        public override void execute()
        {
            SpotRoomPanel panel= UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            panel.headView.showPiao(RoomPanel.getPlayerIndex(operate.index),true);
            operate.isOver = true;
        }
    }
}
