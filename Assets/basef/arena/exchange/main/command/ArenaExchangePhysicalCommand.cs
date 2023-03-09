using basef.arena;
using cambrian.common;
using cambrian.game;
using scene.game;

/// <summary> 奖品兑换 实物奖励 </summary>
public class ArenaExchangePhysicalCommand : UserCommand
{
    /// <summary> 商品ID </summary>
    int sid;

    /// <summary> 详细地址 </summary>
    string info;

    /// <summary> 用户名 </summary>
    string username;

    /// <summary> 手机号码 </summary>
    string tel;

    public ArenaExchangePhysicalCommand(int sid,string info,string tel,string username)
    {
        this.id = CommandConst.PORT_ARENA_MEMBER_AWARD_EXCHANGE;
        this.sid = sid;
        this.info = info;
        this.tel = tel;
        this.username = username;
    }

    public override void bytesRead(ByteBuffer data)
    {
        callbackobj = data.readBoolean();
    }

    public override void bytesWrite(ByteBuffer data)
    {
        data.writeLong(Arena.arena.getId());
        data.writeInt(sid);
        data.writeUTF(username);
        data.writeUTF(tel);
        data.writeUTF(info);
    }

}
