using basef.lobby;
using platform.spotred.playback;

namespace platform.mahjong
{
    public class MJReplayControlProcess : MouseClickProcess
    {
        public int type; //1 减速 2暂停/播放 3加速 4结束

        public override void execute()
        {
            MJReplayTimer timer = this.getRoot<ReplayMahjongRoomPanel>().rcview.timer;

            if (type == ReplayTimer.FALLBACK)
            {
                if (!timer.getPause())
                {
                    UnrealCheckBox button = this.transform.parent.Find("pause").GetComponent<UnrealCheckBox>();
                    button.setState(UnrealCheckObject.ACTIVED);
                    getRoot<ReplayMahjongRoomPanel>().controlReplay(ReplayTimer.PAUSE);
                }

                getRoot<ReplayMahjongRoomPanel>().controlReplay(this.type);
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

                getRoot<ReplayMahjongRoomPanel>().controlReplay(this.type);
            }

            if (type == ReplayTimer.STOP)
            {
                Room.clear();
                root.setVisible(false);

                if (root.getLastPanel() != null)
                {
                    root.getLastPanel().setVisible(true);
                    return;
                }

                UnrealRoot.root.getDisplayObject<PlayBackDetailPanel>().setVisible(true);
                ShowLobbyPanel.showLobbyPanel(false);
            }
        }
    }
}
