using basef.player;
using basef.rule;
using cambrian.common;
using cambrian.game;
using platform;
using scene.game;

namespace basef.lobby
{
    public class CreateRuleRoomProcess:MouseClickProcess
    {
        public override void execute()
        {
            if (Room.room != null&&!Room.room.isStatus(Room.STATE_DESTORY))
            {
                Alert.show("已经在房间中，不能做该操作");
                return;
            }

            SpotRoomRulePanel panel = UnrealRoot.root.getDisplayObject<SpotRoomRulePanel>();
            ByteBuffer data = FileCachesManager.loadPathData(CacheLocalPath.RULE_INFO_PATH, true);
            Rule rule = null;
            if (data != null)
            {
                int sid=data.readInt();
                rule = RuleManager.manager.newSample(sid);
                if (rule != null)
                {
                    rule.bytesReadLocal(data);
                }
            }
            panel.setData(rule,PlayerToken.instance.isPromoter());
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
