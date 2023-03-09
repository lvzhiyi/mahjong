using cambrian.common;
using platform.spotred.room;
using scene.game;

namespace platform
{
    /// <summary>
    /// 收到解散房间消息
    /// </summary>
    public class RecvRoomDisbandCommand:RecvPort
    {
        public RecvRoomDisbandCommand()
        {
            this.id = RecvConst.COMMAND_CLIENT_ROOM_DISBAND;
        }
        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            //long userid = data.readLong();
           // bool isAgree = data.readBoolean();
            //long time = data.readLong();
            //int disbanderindex = data.readInt();
            // true 表示当前处于投票解散状态，false 表示投标结束，
            bool b = data.readBoolean();
            Voting voting=null;
            if (b)
            {
                voting = new Voting();
                voting.bytesRead(data);
            }

            DisbandPanel dpanel = UnrealRoot.root.getDisplayObject<DisbandPanel>();
            if (b)
            {
                dpanel.recvInfo(voting);
                dpanel.show();
                dpanel.refresh();
                dpanel.setVisible(true);
            }
            else
            {
                dpanel.clear();
                dpanel.setVisible(false);
                Alert.confirmAlert.setVisible(false);
            }
        }
    }
}
