using System;
using cambrian.common;
using platform.mahjong;
using platform.poker;
using platform.spotred.room;

namespace platform
{
    /// <summary>
    /// 重连检查panle上是否有操作
    /// </summary>
    public class ReConnectCheckOperateProcess:Process
    {
        private Action<ByteBuffer> callback;

        private ByteBuffer data;

        public ReConnectCheckOperateProcess(Action<ByteBuffer> callback,ByteBuffer data)
        {
            this.callback = callback;
            this.data = data;
        }


        public override void execute()
        {
            Room room = Room.room;
            if (Room.room != null)
            {
                int type= Room.room.roomRule.rule.getPlatFormValue();
                exe(type);
            }
        }

        private void exe(int type)
        {
            if (type == GamePlatform.MJ_GAME)
            {
                MahJongPanel panel = MahJongPanel.CheckPanel();
                if (panel != null)
                {
                    panel.setOperateTimer(back,data);
                }
                else
                {
                    back(data);
                }
            }
            else if(type==GamePlatform.CP_GAME)
            {
                SpotRoomPanel panel = UnrealRoot.root.checkDisplayObject<SpotRoomPanel>();
                if (panel!=null)
                {
                    panel.setOperateTimer(back, data);//这里要看一下，与正式服有没有差异
                }
                else
                {
                    back(data);
                }
            }
            else if (type == GamePlatform.PK_GAME)
            {
                PKGAME.ReconnectRoom(back, data);
            }
        }

        public void back(ByteBuffer data1)
        {
            callback(data1);
        }
    }
}
