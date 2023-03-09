using cambrian.common;
using cambrian.game;

namespace platform.poker
{
    public class PDKTenDeskCacheCard : BytesSerializable
    {
        public int[] states;

        public PDKTenCardInfo[] deskCacheCard;

        public PDKTenDeskCacheCard(int len)
        {
            states = new int[len];
            deskCacheCard = new PDKTenCardInfo[len];
            for (int i = 0; i < len; i++)
            {
                states[i] = 0;
                deskCacheCard[i] = new PDKTenCardInfo(i);
                deskCacheCard[i].cards = new int[0];
            }
        }

        public void clone(PDKTenDeskCacheCard bean)
        {
            for (int i = 0; i < bean.states.Length; i++)
            {
                states[i] = bean.states[i];
                deskCacheCard[i] = (PDKTenCardInfo)bean.deskCacheCard[i].clone();
            }
        }

        /// <summary> 添加桌面上的牌 </summary>
        public void add(PDKTenCardInfo info, bool sound = true)
        {
            deskCacheCard[info.master] = info;
            if (deskCacheCard[info.master].cards.Length > 0)//出牌
            {
                var panel = UnrealRoot.root.getDisplayObject<PDKTenRoomPanel>();
                SoundManager.playPKOther(SoundManager.showcard, 0);
                SoundManager.playPKSpecial(firstShowCard, Room.room.players[info.master].player.sex);
                states[info.master] = 1;
                deskCacheCard[info.master].cards = CardSort.desktopSortLTS(deskCacheCard[info.master]);
                panel.gameView.desktop.showCards(info.master, deskCacheCard[info.master].cards);
                PDKTenMatch.match.tips.reset(deskCacheCard[info.master], PDKTenMatch.match.getSelfHandCard());
                if (sound) panel.gameView.animator.animationPlay(info.type, info.cards, info.master);
            }
            else states[info.master] = -1;
        }

        /// <summary> 添加桌面上的牌 </summary> 回放
        public void addReplay(PDKTenCardInfo info, bool sound = true)
        {
            deskCacheCard[info.master] = info;
            if (deskCacheCard[info.master].cards.Length > 0)
            {
                var panel = UnrealRoot.root.getDisplayObject<PPDKTenReplayRoomPanel>();
                SoundManager.playPKOther(SoundManager.showcard, 0);
                SoundManager.playPKSpecial(firstShowCard, Room.room.players[info.master].player.sex);
                states[info.master] = 1;
                deskCacheCard[info.master].cards = CardSort.desktopSortLTS(deskCacheCard[info.master]);
                panel.gameView.desktop.showCards(info.master, deskCacheCard[info.master].cards);
                PDKTenMatch.match.tips.reset(deskCacheCard[info.master], PDKTenMatch.match.getSelfHandCard());
                if (sound) panel.gameView.animator.animationPlay(info.type, info.cards, info.master);
            }
            else states[info.master] = -1;
        }

        public void remove(int master)
        {
            states[master] = 0;
            deskCacheCard[master] = new PDKTenCardInfo(0);
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

        public PDKTenCardInfo getCardsInfo(int master)
        {
            return deskCacheCard[master];
        }

        /// <summary> 比较玩家选择的牌与其他玩家牌大小 </summary>
        public bool compareTo(PDKTenCardInfo userCards)
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
        public PDKTenCardInfo getLastCard()
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
            deskCacheCard = new PDKTenCardInfo[len];
            bool show = false;
            for (int i = 0; i < len; i++)
            {
                states[i] = data.readInt();
                show = data.readBoolean();
                if (show)
                {
                    deskCacheCard[i] = new PDKTenCardInfo(i);
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
