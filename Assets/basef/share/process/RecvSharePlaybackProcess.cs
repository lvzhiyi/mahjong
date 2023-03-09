using cambrian;
using cambrian.common;
using platform.spotred.playback;
using scene.game;

namespace basef.share
{
    public class RecvSharePlaybackProcess:Process
    {
        string id;
        public void setPlaybackId(string id)
        {
            this.id = id;
        }
        public override void execute()
        {
            SpotRedRoot.dataLoading.refresh();
            CommandManager.addCommand(new PlayBackCommand("?id=" + this.id + "&type=3"), this.onDataInit);
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
            if (!b) return;
            records[0] = new Record();
            records[0].bytesRead(data);

            PlayBackPanel dloading = UnrealRoot.root.getDisplayObject<PlayBackPanel>();
            dloading.dataInit(records);
        }
    }
}
