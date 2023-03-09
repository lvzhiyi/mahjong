using basef.lobby;
using cambrian.common;
using scene.game;

namespace basef.player
{
    public class RecvPromoterStatusUpdateCommand : RecvPort
    {
        public RecvPromoterStatusUpdateCommand()
        {
            this.id = RecvConst.PORT_CLIENT_PROMOTER_STATUS_UPDATE;
        }

        public override void bytesRead(ByteBuffer data)
        {
            PlayerToken.instance.status = data.readInt();
            if (PlayerToken.instance.isPromoter()) Alert.show("权限已开通！");
            ShowLobbyPanel.refreshLobbyPanel();
        }
    }
}
