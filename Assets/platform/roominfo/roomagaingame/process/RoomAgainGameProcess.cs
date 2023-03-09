using cambrian.common;
using platform.mahjong;
using platform.poker;
using platform.spotred.over;

namespace platform
{
    public class RoomAgainGameProcess : MouseClickProcess
    {
        /// <summary>
        /// 绵阳麻将，贵州麻将，扑克,长牌
        /// </summary>
        const int MY_MJ = 0, PK = 2, CP = 3;

        /// <summary>
        /// 是哪个游戏的总结算
        /// </summary>
        public int panelType;

        public override void execute()
        {
            switch (panelType)
            {
                case MY_MJ:
                    myMJ();
                    break;
                case PK:
                    pk();
                    break;
                case CP:
                    myCP();
                    break;
            }
        }

        public void myCP()
        {
            AllOverPanel panel = getRoot<AllOverPanel>();
            var room = panel.room;
            int cacheId = room.cacheId;
            int roomType = room.roomType;
            if (roomType == 0)//普通房间
            {
                CommandManager.addCommand(new AgainNormalRoomCommand(cacheId), back3);
            }
            else if (roomType == 1) //茶馆房间
            {
                CommandManager.addCommand(new AgainClubRoomCommand(cacheId), back3);
            }
            else if (roomType == 2)
            {
                CommandManager.addCommand(new AgainArenaRoomCommand(cacheId), back3);
            }
            else
            {
                Alert.show("该房间类型不支持再来一局");
            }
        }


        public void back3(object obj)
        {
            if (obj == null) return;
        }

        public void myMJ()
        {
            MJAllOverPanel panel = getRoot<MJAllOverPanel>();
            var room = panel.room;
            int cacheId = room.cacheId;
            int roomType = room.roomType;
            if (roomType == 0)//普通房间
            {
                CommandManager.addCommand(new AgainNormalRoomCommand(cacheId), back);
            }
            else if (roomType == 1) //茶馆房间
            {
                CommandManager.addCommand(new AgainClubRoomCommand(cacheId), back);
            }
            else if (roomType == 2)
            {
                CommandManager.addCommand(new AgainArenaRoomCommand(cacheId), back4);
            }
            else
            {
                Alert.show("该房间类型不支持再来一局");
            }
        }

        public void back(object obj)
        {
            if (obj == null) return;
        }

       
        public void back1(object obj)
        {
            if (obj == null) return;
        }

        public void pk()
        {
            PKAllOverPanel panel = getRoot<PKAllOverPanel>();
            var room = panel.room;
            int cacheId = room.cacheId;
            int roomType = room.roomType;
            if (roomType == 0)//普通房间
            {
                CommandManager.addCommand(new AgainNormalRoomCommand(cacheId), back2);
            }
            else if (roomType == 1) //茶馆房间
            {
                CommandManager.addCommand(new AgainClubRoomCommand(cacheId), back2);
            }
            else if (roomType == 2)
            {
                CommandManager.addCommand(new AgainArenaRoomCommand(cacheId), back4);
            }
            else
            {
                Alert.show("该房间类型不支持再来一局");
            }

        }
        public void back2(object obj)
        {
            if (obj == null) return;
        }

        public void back4(object obj)
        {
            if (obj == null) return;
        }
    }
}
