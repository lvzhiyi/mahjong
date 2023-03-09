using cambrian.game;
using basef.share;

namespace platform.spotred.playback
{
    public class SharePlaybackProcess : MouseClickProcess
    {
        PlaybackBar bar;
        /// <summary>
        /// 分享类型(微信,闲聊)
        /// </summary>
        public int sharetype;

        public override void execute()
        {
            PlayBackShareView shareView= this.transform.parent.GetComponent<PlayBackShareView>();
            ShareManager.getInstance().sharePlayback(shareView.id, shareView.rule, sharetype);
            SoundManager.playButton();
            shareView.setVisible(false);
        }
    }
}
