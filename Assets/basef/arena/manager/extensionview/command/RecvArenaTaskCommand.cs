using cambrian.common;
using scene.game;

namespace basef.arena
{
    /// <summary>
    /// 接收玩家的推广任务
    /// </summary>
    public class RecvArenaTaskCommand:RecvPort
    {
        public RecvArenaTaskCommand()
        {
            id = RecvConst.PORT_CLIENT_PLAYER_TASK_UPDATE;
        }

        public override void bytesRead(ByteBuffer data)
        {
            long arenaid = data.readLong();
            long task = data.readLong(); //当前的任务
            ArenaManagerPanel panel = UnrealRoot.root.checkDisplayObject<ArenaManagerPanel>();
            if (panel != null && Arena.arena.getId() == arenaid)
            {
                Arena.arena.getMember().setTask(task);
                if (panel.managerView.extensionView.selfExtensionView.getVisible())
                    panel.managerView.extensionView.refresh();
            }
        }
    }
}
