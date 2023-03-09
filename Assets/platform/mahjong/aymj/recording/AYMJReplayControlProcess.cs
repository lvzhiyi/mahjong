using basef.lobby;
using platform.spotred.playback;

namespace platform.mahjong
{
    public class AYMJReplayControlProcess : MouseClickProcess
    {
        public int type; //1 减速 2暂停/播放 3加速 4结束

        public override void execute()
        {
            AYMJReplayTimer timer = this.getRoot<ReplayAYMJRoomPanel>().rcview.timer;

            if (type == AYMJReplayTimer.FALLBACK)
            {
                if (!timer.getPause())
                {
                    UnrealCheckBox button = this.transform.parent.Find("pause").GetComponent<UnrealCheckBox>();
                    button.setState(UnrealCheckObject.ACTIVED);
                    getRoot<ReplayAYMJRoomPanel>().controlReplay(AYMJReplayTimer.PAUSE);
                }

                getRoot<ReplayAYMJRoomPanel>().controlReplay(this.type);
            }
            else
            {
                if (type == AYMJReplayTimer.PAUSE)
                {
                    UnrealCheckBox button = this.transform.GetComponent<UnrealCheckBox>();
                    if (button.state == UnrealCheckObject.ACTIVED)
                        button.setState(UnrealCheckObject.NORMAL);
                    else
                        button.setState(UnrealCheckObject.ACTIVED);
                }

                getRoot<ReplayAYMJRoomPanel>().controlReplay(this.type);
            }

            if (type == AYMJReplayTimer.STOP)
            {
                root.setVisible(false);

                if (root.getLastPanel() != null)
                {
                    root.getLastPanel().setVisible(true);
                    return;
                }

                UnrealRoot.root.getDisplayObject<PlayBackDetailPanel>().setVisible(true);
                ShowLobbyPanel.showLobbyPanel(false);
                Room.clear();
            }
        }
    }
}
