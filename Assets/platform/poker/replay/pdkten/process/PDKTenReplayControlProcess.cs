using basef.lobby;
using platform.spotred.playback;

namespace platform.poker
{
    public class PDKTenReplayControlProcess : MouseClickProcess
    {
        /// <summary> 类型 </summary>
        public int type;

        public override void execute()
        {
            PPDKTenReplayTimer timer = getRoot<PPDKTenReplayRoomPanel>().rcview.timer;

            if (type == PPDKTenReplayTimer.FALLBACK)
            {
                if (!timer.getPause())
                {
                    var button = transform.parent.Find("pause").GetComponent<UnrealCheckBox>();
                    button.setState(UnrealCheckObject.ACTIVED);
                    getRoot<PPDKTenReplayRoomPanel>().controlReplay(PPDKTenReplayTimer.PAUSE);
                }
            }
            else
            {
                if (type == PPDKTenReplayTimer.PAUSE)
                {
                    var button = transform.GetComponent<UnrealCheckBox>();
                    if (button.state == UnrealCheckObject.ACTIVED)
                        button.setState(UnrealCheckObject.NORMAL);
                    else
                        button.setState(UnrealCheckObject.ACTIVED);
                }
            }
            getRoot<PPDKTenReplayRoomPanel>().controlReplay(type);

            if (type == PPDKTenReplayTimer.STOP)
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
