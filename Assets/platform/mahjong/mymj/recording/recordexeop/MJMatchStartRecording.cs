namespace platform.mahjong
{
    /// <summary>
    /// 游戏开始
    /// </summary>
    public class MJMatchStartRecording : Process
    {
        MJMatchStartOperate operate;

        int selfOperate;

        public MJMatchStartRecording(MJBaseOperate bOperate)
        {
            operate = (MJMatchStartOperate)bOperate;
            selfOperate = operate.getSelfOperate();
        }

        public override void execute()
        {
            operate.playOver();
        }
    }
}
