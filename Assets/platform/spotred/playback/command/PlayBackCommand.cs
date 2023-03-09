using cambrian.common;
using cambrian.game;

namespace platform.spotred.playback
{
    public class PlayBackCommand: SimpleHttpCommand
    {
        public PlayBackCommand(string query) : base(ServerInfos.server.getReplayUrl()+ "/record" + query)
        {

        }
    }
}
