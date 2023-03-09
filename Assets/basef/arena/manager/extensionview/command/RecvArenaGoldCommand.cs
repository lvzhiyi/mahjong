using cambrian.common;
using scene.game;

namespace basef.arena
{
    public class RecvArenaGoldCommand : RecvPort
    {
        public RecvArenaGoldCommand()
        {
            id = RecvConst.PORT_ARENA_PLAYER_GOLD_UPDATE;
        }

        public override void bytesRead(ByteBuffer data)
        {
            long arenaid = data.readLong();
            long gold = data.readLong();//当前的金豆

            ArenaPanel panel = UnrealRoot.root.checkDisplayObject<ArenaPanel>();
            if (panel != null && Arena.arena.getId() == arenaid)
            {
                Arena.arena.getMember().setArenaGold(gold);
                panel.topView.refresh();
            }
            ArenaAgentPanel agentPanel = UnrealRoot.root.checkDisplayObject<ArenaAgentPanel>();
            if (agentPanel != null && Arena.arena.getId() == arenaid)
            {
                Arena.arena.getMember().setArenaGold(gold);
                agentPanel.nextView.refreshScore(gold);
            }
        }
    }
}
