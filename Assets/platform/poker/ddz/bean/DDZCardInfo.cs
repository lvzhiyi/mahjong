using cambrian.common;
using UnityEngine;

namespace platform.poker
{
    /// <summary> 出牌信息 </summary>
    public class DDZCardInfo : BytesSerializable
    {
        /// <summary> 出牌者 </summary>
        public int index;

        /// <summary> 牌类型 </summary>
        public int type;

        /// <summary> 牌等级 </summary>
        public int level;

        /// <summary> 保存桌面牌 </summary>
        public int[] cards;

        public DDZCardInfo(int index)
        {
            this.index = index;
        }

        public DDZCardInfo clone()
        {
            DDZCardInfo bean = new DDZCardInfo(index);
            bean.type = type;
            bean.level = level;
            bean.cards = new int[cards.Length];
            for (int i = 0; i < cards.Length; i++)
            {
                bean.cards[i] = cards[i];
            }
            return bean;
        }

        public DDZCardInfo(int index, int[] cards)
        {
            this.index = index;
            this.cards = cards;
            DDZCardType.checkTypeAndLevel(this);
        }

        public int[] getCards()
        {
            return cards;
        }

        /// <summary> 重连 出牌 读取的数据 </summary>
        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            cards = new int[len];
            for (int i = 0; i < len; i++)
            {
                cards[i] = data.readInt();
            }
            DDZCardType.checkTypeAndLevel(this);
        }

        public void showBytesRead(ByteBuffer data)
        {
            type = data.readInt();
            level = data.readInt();
            int len = data.readInt();
            cards = new int[len];
            for (int i = 0; i < len; i++)
            {
                cards[i] = data.readInt();
            }          
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(type);
            data.writeInt(level);
            data.writeInt(cards.Length);
            for (int i = 0; i < cards.Length; i++)
            {
                data.writeInt(cards[i]);
            }
        }

        /// <summary> 获取当前牌类型的名字 </summary> 已知类型的情况下
        public string getCardTypeNmae()
        {
            return DDZCardType.getCardTypeName(type);
        }

        /// <summary> 是否是炸弹 </summary>
        public bool isBoom()
        {
            return (type == PKCardType.TYPE_CARDS_BOMB || type == PKCardType.TYPE_ZHA_WANG || type == PKCardType.TYPE_ZHA_LIAN);
        }

        /// <summary> 牌型比较大小 </summary>
        public bool compareTo(DDZCardInfo dest)
        {
            if (dest == null) return false;
            if (type != dest.type)
            {
                if (!dest.isBoom()) return false;
                if (type < dest.type) return true;
                if (type > dest.type) return false;
            }
            if (type == PKCardType.TYPE_ZHA_LIAN)
            {
                if (dest.cards.Length > cards.Length) return true;
            }
            if (cards.Length != dest.cards.Length) return false;
            return level < dest.level;
        }
    }
}
