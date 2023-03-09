using basef.lobby;
using platform.spotred.playback;

namespace platform.poker
{
    public class PDKAnYueReplayControlProcess : MouseClickProcess
    {
        /// <summary> 类型 </summary>
        public int type;

        public override void execute()
        {
            PPDKAnYueReplayTimer timer = getRoot<PPDKAnYueReplayRoomPanel>().rcview.timer;

            if (type == PPDKAnYueReplayTimer.FALLBACK)
            {
                if (!timer.getPause())
                {
                    var button = transform.parent.Find("pause").GetComponent<UnrealCheckBox>();
                    button.setState(UnrealCheckObject.ACTIVED);
                    getRoot<PPDKAnYueReplayRoomPanel>().controlReplay(PPDKAnYueReplayTimer.PAUSE);
                }
            }
            else
            {
                if (type == PPDKAnYueReplayTimer.PAUSE)
                {
                    var button = transform.GetComponent<UnrealCheckBox>();
                    if (button.state == UnrealCheckObject.ACTIVED)
                        button.setState(UnrealCheckObject.NORMAL);
                    else
                        button.setState(UnrealCheckObject.ACTIVED);
                }
            }
            getRoot<PPDKAnYueReplayRoomPanel>().controlReplay(type);

            if (type == PPDKAnYueReplayTimer.STOP)
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
