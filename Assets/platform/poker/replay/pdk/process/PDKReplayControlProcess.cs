using basef.lobby;
using platform.spotred.playback;

namespace platform.poker
{
    public class PDKReplayControlProcess : MouseClickProcess
    {
        /// <summary> 类型 </summary>
        public int type;

        public override void execute()
        {
            PPDKReplayTimer timer = getRoot<PPDKReplayRoomPanel>().rcview.timer;

            if (type == PPDKReplayTimer.FALLBACK)
            {
                if (!timer.getPause())
                {
                    var button = transform.parent.Find("pause").GetComponent<UnrealCheckBox>();
                    button.setState(UnrealCheckObject.ACTIVED);
                    getRoot<PPDKReplayRoomPanel>().controlReplay(PPDKReplayTimer.PAUSE);
                }
            }
            else
            {
                if (type == PPDKReplayTimer.PAUSE)
                {
                    var button = transform.GetComponent<UnrealCheckBox>();
                    if (button.state == UnrealCheckObject.ACTIVED)
                        button.setState(UnrealCheckObject.NORMAL);
                    else
                        button.setState(UnrealCheckObject.ACTIVED);
                }
            }
            getRoot<PPDKReplayRoomPanel>().controlReplay(type);

            if (type == PPDKReplayTimer.STOP)
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
