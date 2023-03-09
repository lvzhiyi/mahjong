using cambrian;
using cambrian.common;
using scene.game;

namespace platform.spotred.playback
{
    public class SearchPlayBackIdProcess : MouseClickProcess
    {
        public override void execute()
        {
            PlayBackPanel panel = (PlayBackPanel) this.root;
            string text = panel.input.text;

            if ("".Equals(text) || text.Length == 0)
            {
                Alert.autoShow("请输入回放ID!");
                return;
            }

            if (!StringKit.isInt(text))
            {
                Alert.autoShow("请输入数字!");
                return;
            }

            if(text.Length>8)
            {
                Alert.autoShow("请输入正确的回放ID！");
                return;
            }

            SpotRedRoot.dataLoading.refresh();
            CommandManager.addCommand(new PlayBackCommand("?id=" + text + "&type=3"), this.onDataInit);
        }

        public void onDataInit(object obj)
        {
            SpotRedRoot.dataLoading.hidden();

            if (obj == null)
            {
                Alert.show("无数据!请重新刷新!");
                return;
            }

            ByteBuffer data = (ByteBuffer) obj;
            Record[] records = new Record[1];
            bool b = data.readBoolean();

            PlayBackPanel dloading = (PlayBackPanel) this.root;
            if (!b)
            {

                dloading.input.text = "";
                Alert.show("回放ID不正确!");
                return;
            }
            records[0] = new Record();
            records[0].bytesRead(data);
            dloading.dataInit(records);
            dloading.refresh();
            dloading.setCVisible(true);
        }
    }
}
