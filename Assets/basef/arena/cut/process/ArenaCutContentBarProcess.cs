using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary> 竞赛场 赛场切换 内容 Bar 点击 </summary>
    public class ArenaCutContentBarProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaCutContentBar bar = transform.GetComponent<ArenaCutContentBar>();
            ArenaCutPanel panel = getRoot<ArenaCutPanel>();
            panel.refresh();
            getSingleArena(bar.getArena().getId());

        }
        public void getSingleArena(long id)
        {
            CommandManager.addCommand(new GetArenaInfoCommand(id), getback);
        }

        public void getback(object obj)
        {
            if (obj == null)
            {
                Alert.show("你不是该休闲场的成员");
                return;
            }

            this.root.setVisible(false);
            UnrealRoot.root.getDisplayObject<ArenaPanel>().refresh();
            ByteBuffer data = new ByteBuffer();
            data.writeLong(Arena.arena.getId());
            FileCachesManager.savePath(CacheLocalPath.APPLICATION_PATH + CacheLocalPath.LAST_ENTER_AREN_PATH, false, data);
            if (Arena.arena.getMember().isWarning)
            {
                Alert.show("积分低于预警值，请尽快联系上级。");
            }
            else if (Arena.arena.getMember().lowerFufeng)
            {
                Alert.show("你的休闲场【" + Arena.arena.getName() + "(ID:" + Arena.arena.getId() + ")】" + "积分低于设置值，名下成员被禁止游戏，请尽快联系上级。");
            }
        }
    }

}

