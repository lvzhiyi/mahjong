using cambrian.common;
using platform.spotred.playback;
using scene.game;

namespace basef.arena
{
    /// <summary>
    /// 
    /// </summary>
    public class OpenArenCardRePlayProcess:MouseClickProcess
    {
        public override void execute()
        {
            ArenaRecordContentBar bar = this.transform.parent.GetComponent<ArenaRecordContentBar>();
            SpotRedRoot.dataLoading.refresh();
            CommandManager.addCommand(new PlayBackCommand("?id=" + bar.data.playbackid + "&type=3"), this.onDataInit);
        }


        public void onDataInit(object obj)
        {
            SpotRedRoot.dataLoading.hidden();

            if (obj == null)
            {
                Alert.show("无数据!请重新刷新!");
                return;
            }

            ByteBuffer data = (ByteBuffer)obj;
            Record[] records = new Record[1];
            bool b = data.readBoolean();

            PlayBackPanel dloading = UnrealRoot.root.getDisplayObject<PlayBackPanel>();

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
            dloading.setVisible(true);
        }
    }
}
