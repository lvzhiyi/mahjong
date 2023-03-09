using cambrian.common;
using cambrian.game;

namespace basef.award
{
    public class Award : Sample
    {
        /// <summary> 奖励类型：现实物品奖励 </summary>
        public const int TYPE_REAL = 0;

        /// <summary> 奖励类型：虚拟道具奖励 </summary>
        public const int TYPE_PROP = 1;

        /// <summary> 奖品名称 </summary>
        string name;

        /// <summary> 奖励描述 </summary>
        string description;

        /// <summary> 奖品图标 </summary>
        public string img;

        string notes;

        /// <summary> 奖励类型：如上定义的奖励类型 </summary>
        int type;

        /// <summary> 奖励游戏币 </summary>
        int money;

        /// <summary> 奖励代币 </summary>
        int token;

        /// <summary> 奖励经验 </summary>
        int exper;

        /// <summary> 奖励礼券 </summary>
        int gift = 0;

        /// <summary> 钻石 </summary>
        int diamond;

        /// <summary> 奖励道具数据：{{sid,数量},{sid,数量},{sid,数量},....} </summary>
        int[][] data;

        public string getName()
        {
            return this.name;
        }

        public string getDescription()
        {
            return description;
        }

        public string getImg()
        {
            return ServerInfos.server.getHttPServerUrl() + img + "?" + MathKit.random(1,10000);
        }

        /// <summary> 获得奖励游戏币 </summary>
        public int getMoney()
        {
            return money;
        }

        /// <summary> 获取代币奖励值 </summary>
        public int getToken()
        {
            return token;
        }

        /// <summary> 获得经验奖励值 </summary>
        public int getExper()
        {
            return exper;
        }

        /// <summary> 获得奖券奖励值 </summary>
        public int getGift()
        {
            return gift;
        }

        public string getCount()
        {
            if (money != 0)
                return "金豆X" + money;
            if (token != 0)
                return "房卡X" + token;
            if (exper != 0)
                return "经验X" + exper;
            if (gift != 0)
                return "礼卷X" + gift;
            if (diamond != 0)
                return "钻石X" + diamond;
            return "0";
        }

        /// <summary> 获得道具数据 </summary>
        public int[][] getProps()
        {
            return data;
        }

        public override void bytesRead(ByteBuffer data)
        {
            this.sid = data.readInt();
            this.name = data.readUTF();
            this.description = data.readUTF();
            this.img = data.readUTF();
        }
    }
}
