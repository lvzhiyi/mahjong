using basef.award;
using cambrian.common;

public class ArenaExchangePhysicalRecordData : BytesSerializable
{
    ///<summary> 未发货  </summary>
    public const int UNDO = 0;
    ///<summary> 已发货  </summary>
    public const int SUCCESS = 1;
    ///<summary> 已收货  </summary>
    public const int FAILURE = 2;

    /// <summary> 订单状态 </summary>
    public int state;

    /// <summary> 流水号 </summary>
    public long uuid;

    /// <summary> 记录类型 </summary>
    public long userid;

    /// <summary> 记录类型 </summary>
    public int type;

    /// <summary> 物品id </summary>
    public int awardid;

    /// <summary> 消耗奖章 </summary>
    public long value;

    /// <summary> 消耗其他 </summary>
    public long money;

    /// <summary> 所属竞赛场 </summary>
    public long arena;

    /// <summary> 创建时间 </summary>
    public long time;

    /// <summary> 附加信息 </summary>
    public string info;

    /// <summary> 商品价格 </summary>
    public string price;

    /// <summary> 商品名字 </summary>
    public string title;

    public override void bytesRead(ByteBuffer data)
    {
        this.uuid = data.readLong();
        this.userid = data.readLong();
        this.type = data.readInt();
        this.awardid = data.readInt();
        this.value = data.readLong();
        this.money = data.readLong();
        this.arena = data.readLong();
        this.time = data.readLong();
        data.readUTF();//用户名
        data.readUTF();//手机号码
        data.readUTF();//地址
        state = data.readInt();//目前实物商品状态全部为已发货
        this.info = data.readUTF();
        Award award = (Award)Sample.factory.newSample(awardid);
        price = award.getMoney() + " 奖章 + " + award.getToken()+" 元";
        title = award.getName();
    }
}