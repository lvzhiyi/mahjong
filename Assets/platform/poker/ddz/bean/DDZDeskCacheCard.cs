using cambrian.common;
using cambrian.game;
using UnityEngine;

namespace platform.poker
{
    public class DDZDeskCacheCard : BytesSerializable
    {
        public int[] states;

        public DDZCardInfo[] deskCacheCard;

        public DDZDeskCacheCard(int len)
        {
            states = new int[len];
            deskCacheCard = new DDZCardInfo[len];
            for (int i = 0; i < len; i++)
            {
                states[i] = 0;
                deskCacheCard[i] = new DDZCardInfo(i);
                deskCacheCard[i].cards = new int[0];
            }
        }

        public void clone(DDZDeskCacheCard bean)
        {
            for (int i = 0; i < bean.states.Length; i++)
            {
                states[i] = bean.states[i];
                deskCacheCard[i] = bean.deskCacheCard[i].clone();
            }
        }

        /// <summary> 添加桌面上的牌 </summary>
        public void add(DDZCardInfo info, bool sound = true)
        {
            deskCacheCard[info.index] = info;
            if (deskCacheCard[info.index].cards.Length > 0)//出牌
            {
                var panel = UnrealRoot.root.getDisplayObject<PDDZRoomPanel>();
                SoundManager.playPKOther(SoundManager.showcard, 0);
                SoundManager.playPKSpecial(firstShowCard, Room.room.players[info.index].player.sex);
                states[info.index] = 1;
                deskCacheCard[info.index].cards = CardSort.desktopSortLTS(deskCacheCard[info.index]);
                panel.gameView.desktop.showCards(info.index, deskCacheCard[info.index].cards);
                DDZMatch.match.tips.reset(deskCacheCard[info.index], DDZMatch.match.getSelfHandCard());
                if (sound) panel.gameView.animator.animationPlay(info.type, info.cards, info.index);
            }
            else states[info.index] = -1;
        }

        /// <summary> 添加桌面上的牌 </summary> 回放
        public void addReplay(DDZCardInfo info, bool sound = true)
        {
            deskCacheCard[info.index] = info;
            if (deskCacheCard[info.index].cards.Length > 0)
            {
                var panel = UnrealRoot.root.getDisplayObject<PDDZReplayRoomPanel>();
                SoundManager.playPKOther(SoundManager.showcard, 0);
                SoundManager.playPKSpecial(firstShowCard, Room.room.players[info.index].player.sex);
                states[info.index] = 1;
                deskCacheCard[info.index].cards = CardSort.desktopSortLTS(deskCacheCard[info.index]);
                panel.gameView.desktop.showCards(info.index, deskCacheCard[info.index].cards);
                DDZMatch.match.tips.reset(deskCacheCard[info.index], DDZMatch.match.getSelfHandCard());
                if (sound) panel.gameView.animator.animationPlay(info.type, info.cards, info.index);
            }
            else states[info.index] = -1;
        }

        /// <summary> 移除桌面的牌 </summary>
        public void remove(int index)
        {
            states[index] = 0;
            deskCacheCard[index] = new DDZCardInfo(0);
            deskCacheCard[index].cards = new int[0];
        }

        public bool firstShowCard
        {
            get
            {
                for (int i = 0; i < states.Length; i++)
                {
                    if (states[i] != 0) return false;
                }
                return true;
            }
        }

        public void statesClaer()
        {
            for (int i = 0; i < states.Length; i++)
            {
                states[i] = 0;
            }
        }

        public int getStates(int index)
        {
            return states[index];
        }

        public DDZCardInfo getCardsInfo(int index)
        {
            return deskCacheCard[index];
        }

        public DDZCardInfo lastCards
        {
            get
            {
                int last = Room.room.indexOf();
                for (int i = 0; i < Room.room.getPlayerCount() - 1; i++)
                {
                    last = (last - 1 >= 0) ? last - 1 : Room.room.getPlayerCount() - 1;
                    if (states[last] > 0)
                    {
                        return deskCacheCard[last];
                    }
                }
                return null;
            }
            private set { }
        }

        /// <summary> 比较玩家选择的牌与其他玩家牌大小 </summary>
        public bool compareTo(DDZCardInfo userCards)
        {
            int last = Room.room.indexOf();
            for (int i = 0; i < Room.room.getPlayerCount() - 1; i++)
            {
                last = (last - 1 >= 0) ? last - 1 : Room.room.getPlayerCount() - 1;
                if (states[last] > 0)
                {
                    return deskCacheCard[last].compareTo(userCards);
                }
            }
            return true;
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            states = new int[len];
            deskCacheCard = new DDZCardInfo[len];
            for (int i = 0; i < len; i++)
            {
                states[i] = data.readInt();
                bool show = data.readBoolean();
                if (show)
                {
                    deskCacheCard[i] = new DDZCardInfo(i);
                    deskCacheCard[i].bytesRead(data);
                }
                if (show && (deskCacheCard[i].cards == null || deskCacheCard[i].cards.Length == 0))
                {
                    states[i] = -1;
                }
                else states[i] = show ? 1 : 0;
            }
        }
    }
}
