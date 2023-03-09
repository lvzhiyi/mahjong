namespace platform.mahjong
{
    /// <summary>
    /// 返回操作视图(MJOperateView)
    /// </summary>
    public class MJTangSelectReturnProcess:MouseClickProcess
    {
        public override void execute()
        {
            MJMatch match = MJMatch.match;
            MahjongRoomPanel panel = getRoot<MahjongRoomPanel>();
            panel.gameView.getOperateView()
                .showOperate(match.getMJOperateInfos(panel.getOperate(), MJMatch.match.selfMoCard, false, MJMatch.match.mindex));
            panel.tangOperate = -1;
        }
    }
}
