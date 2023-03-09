using cambrian.common;
using cambrian.game;

namespace basef.bin
{
    /// <summary> 绑定用户 </summary>
    public class BinAccountCommand : UserCommand
    {
        public const int BIN_ACCOUNT_COMMAND = 813;

        /// <summary> 上家 </summary>
        private long promoter;

        public BinAccountCommand(long promoter)
        {
            this.id = BIN_ACCOUNT_COMMAND;
            this.promoter = promoter;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(promoter);
        }

        public override void bytesRead(ByteBuffer data)
        {
            if (data.length == 0) return;

            this.callbackobj = data.readBoolean();
        }
    }
}
