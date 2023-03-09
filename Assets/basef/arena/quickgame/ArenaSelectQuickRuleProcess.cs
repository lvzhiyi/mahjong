
using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{

    public class ArenaSelectQuickRuleProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaLockRule rule = transform.parent.GetComponent<ArenaQuickGameRuleBar>().rule;
            ArenaPanel panel = UnrealRoot.root.getDisplayObject<ArenaPanel>(); 
            panel.quickView.setData(rule);
            panel.quickView.refresh();
            ByteBuffer buffer = new ByteBuffer();
            buffer.writeInt(rule.uuid);
            FileCachesManager.savePathData(CacheLocalPath.ARENA_QUICK_GAME_INFO_PATH, true, buffer);
            this.root.setVisible(false);
        }
    }
}
