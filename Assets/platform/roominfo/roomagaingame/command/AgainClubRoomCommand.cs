using scene.game;
using cambrian.common;
using cambrian.game;

namespace platform
{
    
     public class AgainClubRoomCommand:UserCommand
    {
        int cacheid;

        public  AgainClubRoomCommand(int cacheid)
        {
            id = CommandConst.PORT_CLUB_AGAIN_ROOM;
            this.cacheid = cacheid;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(cacheid);
        }
        public override void bytesRead(ByteBuffer data)
        {
            Room.instance();
            Room.room.bytesRead(data);
            new ShowWaiteRoomCallBackProcess().execute();
        }


    }
}
