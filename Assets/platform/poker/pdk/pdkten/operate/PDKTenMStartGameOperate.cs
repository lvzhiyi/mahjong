using cambrian.common;
using platform.bean;

namespace platform.poker
{
    public class PDKTenMStartGameOperate : PKBaseOperate
    {
        public int tray;   //局数
        public int banker; //庄家

        public PDKTenMStartGameOperate(OperateData data) : base(data)
        {
            PDKTenMatch.match = new PDKTenMatch();
        }

        public override void bytesRead(ByteBuffer data)
        {
            tray = data.readInt();  
            banker = data.readInt();
            PDKTenMatch.match.recorder.bytesRead(data);
        }
    }
}
