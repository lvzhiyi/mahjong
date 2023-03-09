using cambrian.common;
using platform.mahjong.guizhou;

namespace platform.mahjong
{
    /// <summary>
    /// 前台-后台发送-接收销毁房间的消息
    /// </summary>
    public class RecvMJRoomDestoryMsg : RecvMsgHandle
    {
        public RecvMJRoomDestoryMsg()
        {
            this.gamePlatform = GamePlatform.MJ_GAME;
        }

        public override void handle(ByteBuffer data)
        {
            int gametype = GamePanelData.getInstance().getGamePanel(Room.room.getRule().sid);

            switch (gametype)
            {
                case GamePanelData.MY_GAME:
                    new RecvMYMJDestoryMsg(data).execute();
                    break;
                case GamePanelData.AY_GAME:
                    new RecvAYMJDestoryMsg(data).execute();
                    break;
            }
        }
    }
}
