using cambrian.common;
using platform.bean;

namespace platform.poker
{
    /// <summary> 游戏开始 数据 </summary>
    public class DDZMStartGameOperate : PKBaseOperate
    {
        public int tray;   //局数
        public int banker; //庄家

        public DDZMStartGameOperate(OperateData data) : base(data)
        {
            UnrealRoot.root.getDisplayObject<DDZOverSinglePanel>().setVisible(false);
            DDZMatch.match = new DDZMatch();
        }

        public override void bytesRead(ByteBuffer data)
        {
            tray = data.readInt();   
            banker = data.readInt(); 
            DDZMatch.match.recorder.bytesRead(data);
        }
    }
}
