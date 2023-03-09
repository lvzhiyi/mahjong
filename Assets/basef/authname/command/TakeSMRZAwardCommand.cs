using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.authname
{
    /// <summary> 领取实名认证奖励 </summary>
    public class TakeSMRZAwardCommand : UserCommand
    {
        public TakeSMRZAwardCommand()
        {
            this.id = CommandConst.PORT_PLAYER_TAKE_SMRZ_AWARD;
        }

        public override void bytesRead(ByteBuffer data)
        {
            callbackobj = data;
        }
    }
}
