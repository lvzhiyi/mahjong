using scene.game;
using System;

namespace platform.spotred.room
{
    /// <summary>
    /// 重连进入房间
    /// </summary>
    public class ReconnectRoomProcess : Process
    {
        Action<object> callback;

        public ReconnectRoomProcess(Action<object> callback)
        {
            this.callback = callback;
        }

        public override void execute()
        {
            if (Room.room == null)
            {
                callback(false);
                return;
            }

            SpotRedRoot.dataLoading.hidden();

            if (Room.room.isStatus(Room.STATE_MATCH))
            {
                this.callback(true);
                return;
            }
            this.callback(true);
            new ShowConnectWaiteRoomCallBackProcess().execute();
        }
    }
}
