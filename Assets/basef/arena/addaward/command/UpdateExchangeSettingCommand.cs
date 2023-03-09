using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary>
    /// 更新兑换设置
    /// </summary>
    public class UpdateExchangeSettingCommand:UserCommand
    {
        public const int ADD = 0, UPDATE = 2, DELETE = 1;

        private long areadid;

        private long gold;

        private int exchaneid;

        private int type;

        public UpdateExchangeSettingCommand(long areadid,long gold,int exchaneid, int type)
        {
            id = CommandConst.PORT_ARENA_SETTING_EXCHANGE;
            this.areadid = areadid;
            this.exchaneid = exchaneid;
            this.type = type;
            this.gold = gold;
        }


        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(areadid);
            data.writeInt(type);
            data.writeInt(exchaneid);
            data.writeLong(gold*Arena.PROPORTION);
        }

        public override void bytesRead(ByteBuffer data)
        {
            
            Arena.arena.exchangeSettings.bytesRead(data);
            callbackobj = true;
        }
    }
}
