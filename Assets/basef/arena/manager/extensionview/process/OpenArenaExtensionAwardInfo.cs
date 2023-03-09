namespace basef.arena
{
    ///<summary> 打开下级推广奖励 </summary>
    public class OpenArenaExtensionAwardInfo : MouseClickProcess
    {
        public override void execute()
        {
            Alert.show("<color=#FF0000>下级推广奖励:</color>\n推广员邀请进行比赛可获得乐豆奖励;\n<color=#FF0000>奖励获得规则:</color>\n下级推广奖励,将按达标房间类的参与玩家人数平均分配;例如一场三人绵阳承包房间达标了,下级推广奖励将分成3份,其中2人属于A推广员,1人属于B推广员,则A可以获得2分奖励,B可以获得一份奖励.");
        }
    }
}
