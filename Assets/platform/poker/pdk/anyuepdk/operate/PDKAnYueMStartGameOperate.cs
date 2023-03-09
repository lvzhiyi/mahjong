using cambrian.common;
using platform.bean;

namespace platform.poker
{
    public class PDKAnYueMStartGameOperate : PKBaseOperate
    {
        public int tray;   //局数
        public int banker; //庄家

        public PDKAnYueMStartGameOperate(OperateData data) : base(data)
        {
            PDKAnYueMatch.match = new PDKAnYueMatch();
        }

        public override void bytesRead(ByteBuffer data)
        {
            tray = data.readInt();  
            banker = data.readInt();
            PDKAnYueMatch.match.recorder.bytesRead(data);
        }
    }
}
