using cambrian.common;
using cambrian.game;
using System.Text;

namespace basef.notice
{
    public class NewsCommand: SimpleHttpCommand
    {
        public NewsCommand()
          : base(ServerInfos.server.getHttPServerUrl() + "/news/news.txt")// base("http://112.124.110.208/qipai/scxz/news/news.txt")
        {
        }
        public override void onCommand(ByteBuffer data)
        {
            string text = Encoding.UTF8.GetString(data.toArray());
            this.callback(text);
        }
    }
}
