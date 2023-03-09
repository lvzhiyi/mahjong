using basef.arena;
using cambrian.common;
using platform.spotred;

namespace platform.poker
{
    public class PKGAME
    {
        /// <summary> 位置下标 </summary>
        public const int DOWN = 0, RIGHT = 1,TOP = 2, LEFT = 3;

        /// <summary> 房间规则(斗地主,争上游，跑得快,) </summary>
        public const int DDZ = 3001, ZSY = 3002, PDK = 3003;
        /// <summary> 10张跑得快 </summary>
        public const int PDK_10 = 3006;
        /// <summary>安岳跑得快</summary>
        public const int  PDK_ANYUE=3007;

        public static int RULESID;

        /// <summary> 获取相对于自己位置玩家的索引 </summary>
        public static int GetIndex(int index)
        {
            int len = Room.room.roomRule.rule.playerCount;

            int pos = ((index + len) - Room.room.indexOf()) % len;

            switch (len)
            {
                case 4:
                    break;
                case 3:
                    if (pos == 2)
                        pos = LEFT;
                    break;
                case 2:
                    if (pos == 1)
                    {
                        pos = RIGHT;
                        if (RULESID == 3006)//
                            pos = TOP;
                    }
                    break;
            }
            return pos;
        }

        /// <summary> 创建房间 </summary>
        public static void CreatRoom()
        {
            RULESID = Room.room.getRule().sid;
            switch (RULESID)
            {
                case DDZ:
                    var ddz = UnrealRoot.root.getDisplayObject<PDDZRoomPanel>();
                    ddz.refresh();
                    ddz.refreshWaitView();
                    ddz.clearBaseOperate(); //清除队列操作
                    DDZCardType.CardRuleSet(Room.room.getRule());
                    UnrealRoot.root.showPanel<PDDZRoomPanel>();
                    break;
                case ZSY:

                    break;
                case PDK:
                    var pdk = UnrealRoot.root.getDisplayObject<PPDKRoomPanel>();
                    pdk.refresh();
                    pdk.refreshWaitView();
                    pdk.clearBaseOperate(); //清除队列操作
                    UnrealRoot.root.showPanel<PPDKRoomPanel>();
                    break;
                case PDK_10:
                    var tenpdk=UnrealRoot.root.getDisplayObject<PDKTenRoomPanel>();
                    tenpdk.refresh();
                    tenpdk.refreshWaitView();
                    tenpdk.clearBaseOperate();
                    UnrealRoot.root.showPanel<PDKTenRoomPanel>();
                    break;
                case PDK_ANYUE:
                    var anyuepdk = UnrealRoot.root.getDisplayObject<PDKAnYueRoomPanel>();
                    anyuepdk.refresh();
                    anyuepdk.refreshWaitView();
                    anyuepdk.clearBaseOperate();
                    UnrealRoot.root.showPanel<PDKAnYueRoomPanel>();
                    break;
                default: break;
            }
        }

        /// <summary> 重连 创建房间 </summary>
        public static void ReconnectCreatRoom()
        {
            RULESID = Room.room.getRule().sid;
            switch (RULESID)
            {
                case DDZ:
                    var ddz = UnrealRoot.root.getDisplayObject<PDDZRoomPanel>();
                    ddz.refresh();
                    ddz.refreshWaitView();
                    ddz.clearBaseOperate(); //清除队列操作
                    ddz.setVisible(true);
                    break;
                case ZSY:

                    break;
                case PDK:
                    var pdk = UnrealRoot.root.getDisplayObject<PPDKRoomPanel>();
                    pdk.refresh();
                    pdk.refreshWaitView();
                    pdk.clearBaseOperate(); //清除队列操作
                    pdk.setVisible(true);
                    break;
                case PDK_10:
                    var pdk_10 = UnrealRoot.root.getDisplayObject<PDKTenRoomPanel>();
                    pdk_10.refresh();
                    pdk_10.refreshWaitView();
                    pdk_10.clearBaseOperate(); //清除队列操作
                    pdk_10.setVisible(true);
                    break;
                case PDK_ANYUE:
                    var pdk_anyue = UnrealRoot.root.getDisplayObject<PDKAnYueRoomPanel>();
                    pdk_anyue.refresh();
                    pdk_anyue.refreshWaitView();
                    pdk_anyue.clearBaseOperate(); //清除队列操作
                    pdk_anyue.setVisible(true);
                    break;
                default: break;
            }
        }

        /// <summary> 重连房间 </summary>
        public static void ReconnectRoom(PKRecvOperateTimer.ActionBack action, ByteBuffer data)
        {
            var panel = PKRoomPanel.CheckPanel();
            if (panel != null)
            {
                panel.setOperateTimer(action, data);
            }
            else action(data);
        }

        /// <summary> 回放房间 </summary>
        public static void ReplayRoom(int roomid, int time, RoomRule rule, ByteBuffer data)
        {
            RULESID = rule.rule.sid;
            UnrealLuaPanel p = UnrealRoot.root.checkDisplayObject<ArenaRecordPanel>();
            switch (RULESID)
            {
                case DDZ:
                    var ddzReplay = new DDZReplay();
                    ddzReplay.setData(roomid, time, rule);
                    var ddzpanel = UnrealRoot.root.getDisplayObject<PDDZReplayRoomPanel>();
                    ddzpanel.setLastPanel(p);
                    ddzpanel.setRePlay(ddzReplay);
                    ddzReplay.bytesRead(data);
                    ddzReplay.start();
                    ddzpanel.refreshGameView();
                    UnrealRoot.root.showPanel<PDDZReplayRoomPanel>();
                    break;
                case ZSY:

                    break;
                case PDK:
                    var pdkReplay = new PDKReplay();
                    pdkReplay.setData(roomid, time, rule);
                    var pdkpanel = UnrealRoot.root.getDisplayObject<PPDKReplayRoomPanel>();
                    pdkpanel.setLastPanel(p);
                    pdkpanel.setRePlay(pdkReplay);
                    pdkReplay.bytesRead(data);
                    pdkReplay.start();
                    pdkpanel.refreshGameView();
                    UnrealRoot.root.showPanel<PPDKReplayRoomPanel>();
                    break;
                case PDK_10:
                    var pdkTenReplay = new PDKTenReplay();
                    pdkTenReplay.setData(roomid, time, rule);
                    var pdktenpanel = UnrealRoot.root.getDisplayObject<PPDKTenReplayRoomPanel>();
                    pdktenpanel.setLastPanel(p);
                    pdktenpanel.setRePlay(pdkTenReplay);
                    pdkTenReplay.bytesRead(data);
                    pdkTenReplay.start();
                    pdktenpanel.refreshGameView();
                    UnrealRoot.root.showPanel<PPDKTenReplayRoomPanel>();
                    break;
                case PDK_ANYUE:
                    var pdkAnyueReplay = new PDKAnYueReplay();
                    pdkAnyueReplay.setData(roomid, time, rule);
                    var pdkAnyuepanel = UnrealRoot.root.getDisplayObject<PPDKAnYueReplayRoomPanel>();
                    pdkAnyuepanel.setLastPanel(p);
                    pdkAnyuepanel.setRePlay(pdkAnyueReplay);
                    pdkAnyueReplay.bytesRead(data);
                    pdkAnyueReplay.start();
                    pdkAnyuepanel.refreshGameView();
                    UnrealRoot.root.showPanel<PPDKAnYueReplayRoomPanel>();
                    break;
                default: break;
            }
        }
    }
}
