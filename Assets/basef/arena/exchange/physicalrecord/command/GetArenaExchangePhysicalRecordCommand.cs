using basef.arena;
using cambrian.common;
using cambrian.game;
using scene.game;

/// <summary> 获取实物商品兑换记录 </summary>
public class GetArenaExchangePhysicalRecordCommand : UserCommand
{
    public long uuid;

    public GetArenaExchangePhysicalRecordCommand(long uuid)
    {
        this.id = CommandConst.PORT_ARENA_AWARD_EXCHANGE_RECORD;
        this.uuid = uuid;
    }

    public override void bytesRead(ByteBuffer data)
    {
        ArenaExchangePhysicalRecordData recorddata;
        ArrayList<ArenaExchangePhysicalRecordData> list = new ArrayList<ArenaExchangePhysicalRecordData>();
        int len = data.readInt();

        for (int i = 0; i < len; i++)
        {
            recorddata = new ArenaExchangePhysicalRecordData();
            recorddata.bytesRead(data);
            list.add(recorddata);
        }
        callbackobj = list;
        recorddata = null;
    }

    public override void bytesWrite(ByteBuffer data)
    {
        data.writeLong(Arena.arena.getId());    //竞赛场id
        data.writeInt(102);                     //固定死201 奖章兑换实物
        data.writeLong(uuid);                   //UUID
    }
}