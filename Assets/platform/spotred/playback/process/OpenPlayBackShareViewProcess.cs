
namespace platform.spotred.playback
{
    public class OpenPlayBackShareViewProcess:MouseClickProcess
    {
        public override void execute()
        {
            PlaybackBar bar = this.transform.parent.GetComponent<PlaybackBar>();
            PlayBackPanel panel=(PlayBackPanel)this.root;
        }
    }
}
