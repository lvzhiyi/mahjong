using basef.lobby;
using platform.spotred.playback;

namespace platform.poker
{
    public class DDZReplayControlProcess : MouseClickProcess
    {
        public int type;

        public override void execute()
        {
            PDDZReplayTimer timer = getRoot<PDDZReplayRoomPanel>().rcview.timer;

            if (type == PDDZReplayTimer.FALLBACK)
            {
                if (!timer.getPause())
                {
                    var button = transform.parent.Find("pause").GetComponent<UnrealCheckBox>();
                    button.setState(UnrealCheckObject.ACTIVED);
                    getRoot<PDDZReplayRoomPanel>().controlReplay(PDDZReplayTimer.PAUSE);
                }
            }
            else
            {
                if (type == PDDZReplayTimer.PAUSE)
                {
                    var button = transform.GetComponent<UnrealCheckBox>();
                    if (button.state == UnrealCheckObject.ACTIVED)
                        button.setState(UnrealCheckObject.NORMAL);
                    else
                        button.setState(UnrealCheckObject.ACTIVED);
                }
            }
            getRoot<PDDZReplayRoomPanel>().controlReplay(type);

            if (type == PDDZReplayTimer.STOP)
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
