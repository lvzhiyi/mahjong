using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary>
    /// 获取竞赛场列表
    /// </summary>
    public class GetArenaListProcess:MouseClickProcess
    {
        public override void execute()
        {
            CommandManager.addCommand(new GetArenaListCommand(),back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            ArrayList<Arena> list = (ArrayList<Arena>) obj;
            if (list.count == 0)
            {
                ArenaNoJoinPanel panel = UnrealRoot.root.getDisplayObject<ArenaNoJoinPanel>();
                panel.refresh();
                UnrealRoot.root.showPanel<ArenaNoJoinPanel>();
            }
            else
            {
                getSingleArena(enterArena(list));
            }
        }

        public long enterArena(ArrayList<Arena> list)
        {
            ByteBuffer data = FileCachesManager.loadPath(CacheLocalPath.APPLICATION_PATH + CacheLocalPath.LAST_ENTER_AREN_PATH, false);
            long arena = 0;
            if (data != null)
            {
                arena=data.readLong();
            }

            if (arena != 0)
            {
                for (int i = 0; i < list.count; i++)
                {
                    if (arena == list.get(i).getId())
                        return arena;
                }
            }

            return list.get(0).getId();
        }

        public void getSingleArena(long id)
        {
            CommandManager.addCommand(new GetArenaInfoCommand(id),getback);
        }

        public void getback(object obj)
        {
            if (obj == null)
                return;
            ArenaPanel panel = UnrealRoot.root.getDisplayObject<ArenaPanel>();
            panel.refresh();
            UnrealRoot.root.showPanel<ArenaPanel>();
           if(Arena.arena.getMember().isWarning)
            {
                Alert.show("积分低于预警值，请尽快联系上级。");
            }
            else if(Arena.arena.getMember().lowerFufeng)
            {
                Alert.show("你的赛场【"+ Arena.arena.getName()+"(ID:"+Arena.arena.getId()+")】"+"积分低于设置值，名下成员被禁止游戏，请尽快联系上级。");
            }
        }
    }
}
