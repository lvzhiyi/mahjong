using cambrian.game;

namespace platform.spotred.playback
{
    public class OpenPlaybackProcess: MouseClickProcess
    {
        PlaybackBar bar;

        public override void execute()
        {
            bar = GetComponent<PlaybackBar>();
            if (bar == null) bar = transform.parent.GetComponent<PlaybackBar>();

            PlayBackPanel panel=(PlayBackPanel) this.root;
            Record record=panel.list.get(this.bar.index);

            PlayBackDetailPanel detailPanel= UnrealRoot.root.getDisplayObject<PlayBackDetailPanel>();
            detailPanel.showPlaybackInfo(record);
            detailPanel.refresh();
            detailPanel.setVisible(true);
            detailPanel.setLastPanel(panel);
            panel.setVisible(false);
            SoundManager.playButton();
        }
    }
}
