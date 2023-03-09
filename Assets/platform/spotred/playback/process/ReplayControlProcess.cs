using basef.lobby;

namespace platform.spotred.playback
{
    public class ReplayControlProcess : MouseClickProcess
    {
        public int type; //1 减速 2暂停/播放 3加速 4结束
        public override void execute()
        {
            ReplayTimer timer = this.getRoot<ReplaySpotRoomPanel>().rcview.timer;

            if (type == ReplayTimer.FALLBACK)
            {
                if (!timer.getPause())
                {
                    UnrealCheckBox button = this.transform.parent.Find("pause").GetComponent<UnrealCheckBox>();
                    button.setState(UnrealCheckBox.ACTIVED);
                    this.getRoot<ReplaySpotRoomPanel>().controlReplay(ReplayTimer.PAUSE);
                }

                this.getRoot<ReplaySpotRoomPanel>().controlReplay(this.type);
            }
            else
            {
                if (type == ReplayTimer.PAUSE)
                {
                    UnrealCheckBox button = this.transform.GetComponent<UnrealCheckBox>();
                    if (button.state == UnrealCheckObject.ACTIVED)
                        button.setState(UnrealCheckObject.NORMAL);
                    else
                        button.setState(UnrealCheckObject.ACTIVED);
                }
                this.getRoot<ReplaySpotRoomPanel>().controlReplay(this.type);
            }



            if (type == ReplayTimer.STOP)
            {
                Room.clear();
                this.root.setVisible(false);

                if (this.root.getLastPanel() != null)
                {
                    this.root.getLastPanel().setVisible(true);
                    return;
                }
                UnrealRoot.root.getDisplayObject<PlayBackDetailPanel>().setVisible(true);
                ShowLobbyPanel.showLobbyPanel(false);
            }
        }
    }
}
