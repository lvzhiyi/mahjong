using cambrian.common;

namespace basef.share
{
    /// <summary>
    /// 分享到微信
    /// </summary>
    public class ShareToWeiXinProcess:MouseClickProcess
    {
        public override void execute()
        {
            int random = MathKit.random(0, 3);
            ShareManager.getInstance().shareGames(ShareManager.WEIXIN, ShareToFriendProcess.titles[random], ShareToFriendProcess.content[random]);
        }
    }
}
