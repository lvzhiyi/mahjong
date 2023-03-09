using cambrian.common;
using scene.game;

namespace basef.player
{
    /// <summary>
    /// 接收房卡
    /// </summary>
    public class RecvPlayerCardCommand: RecvPort
    {
        public delegate void updateCard();


        private static updateCard updateEvent;

        public RecvPlayerCardCommand()
        {
            this.id = RecvConst.COMMAND_CLIENT_PLAYER_ROOOMCARDS;
        }

        public static void addEvent(updateCard action)
        {
            updateEvent += action;
        }

        public static void removeEvent(updateCard action)
        {
            updateEvent -= action;
        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            long num = data.readLong();
            if (num == int.MinValue)
                return;

            PlayerToken.instance.token = num;

            if (updateEvent != null)
                updateEvent();
        }
    }
}
