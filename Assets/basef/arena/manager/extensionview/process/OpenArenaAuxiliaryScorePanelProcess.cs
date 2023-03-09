using cambrian.common;
using UnityEngine;

namespace basef.arena
{
    /// <summary> 打开辅分设置界面按钮 </summary>
    public class OpenArenaAuxiliaryScorePanelProcess : MouseClickProcess
    {

        ArenaAuxiliaryScorePanel panel;

        [HideInInspector] public long userid;

        public override void execute()
        {
            panel = UnrealRoot.root.getDisplayObject<ArenaAuxiliaryScorePanel>();
            userid = getRoot<ArenaAgentPanel>().nextView.operateAgent.extension.userid;
            CommandManager.addCommand(new GetAuxiliaryScoreCommand(Arena.arena.getId(), userid), infoback);
        }

        public void infoback(object obj)
        {
            if (obj == null)
            {
                Alert.show("你不是该赛场的成员");
                return;
            }
            ByteBuffer data = (ByteBuffer)obj;
            long moneny = data.readLong();
            long task = data.readLong();
            long totalMoney = data.readLong();
            long totalTask = data.readLong();
            long fufen = data.readLong();
            long warning = data.readLong();
            panel.userid = userid;
            panel.setData(moneny, task, totalMoney, totalTask,fufen,warning);
            panel.refresh();
            panel.setCVisible(true);
        }

    }
}