using cambrian.common;
using basef.player;

/// <summary> 领取实名认证奖励 </summary>
namespace basef.authname
{
    public class TakeSmrzAwardProcess : MouseClickProcess
    {
        public override void execute()
        {
            CommandManager.addCommand(new TakeSMRZAwardCommand(), callback);
        }

        public void callback(object obj)
        {
            if (obj == null) return;
            ByteBuffer data = (ByteBuffer)obj;
            Player.player.status = data.readInt();
            this.getRoot<UpdatePasswordPanel>().refresh();
        }
    }
}
