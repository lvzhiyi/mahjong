using cambrian.common;
using basef.rule;
using basef.share;

namespace basef.player
{
    public class SharePromoterRoomProcess:MouseClickProcess
    {
        public override void execute()
        {

            OpenRoomListBar bar = this.transform.parent.GetComponent<OpenRoomListBar>();

            if (bar.share.state == UnrealButton.DISABLE) return;

            CommandManager.addCommand(new ShareRoomRuleCommand(bar.roomEntry), callback);
        }

        public void callback(object obj)
        {
            if (obj == null) return;
            Rule rule = (Rule)obj;

            OpenRoomListBar bar = this.transform.parent.GetComponent<OpenRoomListBar>();
            string str = "房主支付元宝 " + rule.getPlayRule(true);
            ShareManager.getInstance().sharePromoterRoom(bar.roomEntry.roomid, str);
        }
    }
}
