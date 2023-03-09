namespace platform.mahjong
{
    /// <summary>
    /// 更新比赛状态
    /// </summary>
    public class MJMatchUpdateStatesRecording : Process
    {
        MJMatchUpdateStateslOperate operate;

        int selfOperate;

        public MJMatchUpdateStatesRecording(MJBaseOperate operate)
        {
            this.operate=(MJMatchUpdateStateslOperate)operate;
            selfOperate = operate.getSelfOperate();
        }

        public override void execute()
        {
            operate.playOver();
        }
    }
}
