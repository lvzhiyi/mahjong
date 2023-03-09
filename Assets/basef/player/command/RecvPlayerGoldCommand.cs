using cambrian.common;
using scene.game;

namespace basef.player
{
    public class RecvPlayerGoldCommand: RecvPort
    {
        public delegate void updateGold();

        private static updateGold updateEvent;
        public RecvPlayerGoldCommand()
        {
            this.id = RecvConst.PORT_CLIENT_PLAYER_GOLD_UPDATE;
        }

        public static void addEvent(updateGold action)
        {
            updateEvent += action;
        }

        public static void removeEvent(updateGold action)
        {
            updateEvent -= action;
        }

        public override void bytesRead(ByteBuffer data)
        {
            Player.player.money=data.readLong();
            if(updateEvent!=null)
                updateEvent();
        }
    }
}
