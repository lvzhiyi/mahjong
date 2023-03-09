using cambrian.common;
using cambrian.game;
using cambrian.uui;
using System.Text;
using basef.activity;

namespace basef.rank
{
    public class OpenActivityRulePanelProcess : MouseClickProcess
    {

        /// <summary>
        /// 文本信息索引
        /// </summary>
        public int index;

        public override void execute()
        {
            string url = ServerInfos.server.getHttPServerUrl() + TeaRankListPanel.NAMES[index];
            Loader.getLoader().loadBytes( url+ "?" + MathKit.random(1, 10000), onCommand);
        }

        public void onCommand(byte[] data)
        {
            string text = Encoding.UTF8.GetString(data);
            ActivityRulePanel panel = UnrealRoot.root.getDisplayObject<ActivityRulePanel>();
            panel.setValue(text);
            panel.refresh();
            panel.setVisible(true);
        }
    }
}
