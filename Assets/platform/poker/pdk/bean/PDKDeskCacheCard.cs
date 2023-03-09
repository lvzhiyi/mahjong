using cambrian.common;
using cambrian.game;

namespace platform.poker
{
    public class PDKDeskCacheCard : BytesSerializable
    {
        public int[] states;

        public PDKCardInfo[] deskCacheCard;

        public PDKDeskCacheCard(int len)
        {
            states = new int[len];
            deskCacheCard = new PDKCardInfo[len];
            for (int i = 0; i < len; i++)
            {
                states[i] = 0;
                deskCacheCard[i] = new PDKCardInfo(i);
                deskCacheCard[i].cards = new int[0];
            }
        }

        public void clone(PDKDeskCacheCard bean)
        {
            for (int i = 0; i < bean.states.Length; i++)
            {
                states[i] = bean.states[i];
                deskCacheCard[i] = bean.deskCacheCard[i].clone();
            }
        }

        /// <summary> 添加桌面上的牌 </summary>
        public void add(PDKCardInfo info, bool sound = true)
        {
            deskCacheCard[info.master] = info;
            if (deskCacheCard[info.master].cards.Length > 0)//出牌
            {
                var panel = UnrealRoot.root.getDisplayObject<PPDKRoomPanel>();
                SoundManager.playPKOther(SoundManager.showcard, 0);
                SoundManager.playPKSpecial(firstShowCard, Room.room.players[info.master].player.sex);
                states[info.master] = 1;
                deskCacheCard[info.master].cards = CardSort.desktopSortLTS(deskCacheCard[info.master]);
                panel.gameView.desktop.showCards(info.master, deskCacheCard[info.master].cards);
                PDKMatch.match.tips.reset(deskCacheCard[info.master], PDKMatch.match.getSelfHandCard());
                if (sound) panel.gameView.animator.animationPlay(info.type, info.cards, info.master);
            }
            else states[info.master] = -1;
        }

        /// <summary> 添加桌面上的牌 </summary> 回放
        public void addReplay(PDKCardInfo info, bool sound = true)
        {
            deskCacheCard[info.master] = info;
            if (deskCacheCard[info.master].cards.Length > 0)
            {
                var panel = UnrealRoot.root.getDisplayObject<PPDKReplayRoomPanel>();
                SoundManager.playPKOther(SoundManager.showcard, 0);
                SoundManager.playPKSpecial(firstShowCard, Room.room.players[info.master].player.sex);
                states[info.master] = 1;
                deskCacheCard[info.master].cards = CardSort.desktopSortLTS(deskCacheCard[info.master]);
                panel.gameView.desktop.showCards(info.master, deskCacheCard[info.master].cards);
                PDKMatch.match.tips.reset(deskCacheCard[info.master], PDKMatch.match.getSelfHandCard());
                if (sound) panel.gameView.animator.animationPlay(info.type, info.cards, info.master);
            }
            else states[info.master] = -1;
        }

        public void remove(int master)
        {
            states[master] = 0;
            deskCacheCard[master] = new PDKCardInfo(0);
            deskCacheCard[master].cards = new int[0];
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

        public int getStates(int master)
        {
            return states[master];
        }

        public PDKCardInfo getCardsInfo(int master)
        {
            return deskCacheCard[master];
        }

        /// <summary> 比较玩家选择的牌与其他玩家牌大小 </summary>
        public bool compareTo(PDKCardInfo userCards)
        {
            int last = Room.room.indexOf();
            for (int i = 0; i < Room.room.getPlayerCount() - 1; i++)
            {
                last = (last - 1 >= 0) ? last - 1 : Room.room.getPlayerCount() - 1;
                if (states[last] > 0)
                {
                    return userCards.compareTo(deskCacheCard[last]);
                }
            }
            return true;
        }

        /// <summary>
        /// 获取上一家人的桌面牌
        /// </summary>
        /// <returns></returns>
        public PDKCardInfo getLastCard()
        {
            int last = Room.room.indexOf();
            int mine = last;
            for (int i = 0; i < Room.room.getPlayerCount() - 1; i++)
            {
                last = (last - 1 >= 0) ? last - 1 : Room.room.getPlayerCount() - 1;
                if (mine!=last&&states[last] > 0)
                {
                    return deskCacheCard[last];
                }
            }

            return null;
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            states = new int[len];
            deskCacheCard = new PDKCardInfo[len];
            bool show = false;
            for (int i = 0; i < len; i++)
            {
                states[i] = data.readInt();
                show = data.readBoolean();
                if (show)
                {
                    deskCacheCard[i] = new PDKCardInfo(i);
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
