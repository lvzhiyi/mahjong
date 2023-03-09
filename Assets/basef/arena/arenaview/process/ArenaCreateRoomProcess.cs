using basef.player;
using cambrian.common;
using cambrian.game;
using platform;

namespace basef.arena
{
    public class ArenaCreateRoomProcess : MouseClickProcess
    {
        public override void execute()
        {
            Arena arena = Arena.arena;
            ArrayList<ArenaLockRule> clubrules = getRoot<ArenaPanel>().deskView.getLockedRule();
            ArenaDeskBar bar = GetComponent<ArenaDeskBar>();
            int deskIndex = bar.index_space;
            int len = clubrules.count; //锁定的规则数量

            if (len == 0)
            {
                Alert.show("主人未设置规则！");
                return;
            }

            if (deskIndex >= len)
            {
                Alert.show("请点击最左边的对应的规则，创建房间后才能入座");
                return;
            }
            if (!Arena.arena.getMember().isMaster() || !Arena.arena.getMember().isAgent())
            {
                if (Arena.arena.getMember().lowerFufeng)
                {
                    Alert.show("你被禁止与房间中玩家同桌，请联系上级。");
                    return;
                }
            }

            WXManager.getInstance().getGPSLocation("Root", "call_back_gps");
            CommandManager.addCommand(new ArenaCreateRoomCommand(arena.getId(), deskIndex, bar.getRule()), back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            new ShowWaiteRoomCallBackProcess().execute();
        }
    }
}