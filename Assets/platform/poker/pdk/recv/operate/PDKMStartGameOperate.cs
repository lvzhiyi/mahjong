using cambrian.common;
using platform.bean;

namespace platform.poker
{
    public class PDKMStartGameOperate : PKBaseOperate
    {
        public int tray;   //局数
        public int banker; //庄家

        public PDKMStartGameOperate(OperateData data) : base(data)
        {
            UnrealRoot.root.getDisplayObject<PDKOverSinglePanel>().setVisible(false);
            PDKMatch.match = new PDKMatch();
        }

        public override void bytesRead(ByteBuffer data)
        {
            tray = data.readInt();  
            banker = data.readInt();
            PDKMatch.match.recorder.bytesRead(data);
        }
    }
}
