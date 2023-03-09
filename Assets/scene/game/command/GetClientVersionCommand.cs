using cambrian.common;
using cambrian.game;

namespace scene.game
{
    /// <summary>
    /// 从服务器端获取客户端版本号，用于游戏运行后台，不能更新
    /// </summary>
    public class GetClientVersionCommand : UserCommand
    {
        public GetClientVersionCommand()
        {
            this.id = CommandConst.PORT_VERSIONS;
        }

        public override void bytesRead(ByteBuffer data)
        {
            Versions versions = new Versions();
            versions.bytesRead(data);
            callbackobj = versions;
        }
    }
}
