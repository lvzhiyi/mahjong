namespace platform.mahjong
{
    /// <summary>
    /// 房间等待视图
    /// </summary>
    public class MJWaitView : MJBaseWaitView
    {
        /// <summary>
        /// 快速开始按钮
        /// </summary>
        private UnrealTextButton fastStart;


        protected override void xinit()
        {
            base.xinit();
            if (transform.Find("fast") != null)
            {
                fastStart = transform.Find("fast").GetComponent<UnrealTextButton>();
                fastStart.init();
            }
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            Room room = Room.room;
            if (fastStart != null)
                fastStart.setVisible(false);

            if (room.roomRule.getGameNum() < 0 && !room.isType(Room.JBC))
            {
                if (fastStart != null)
                {
                    if (room.players.Length > 2)
                    {
                        if (room.players.Length == 4)
                            fastStart.text.text = "2/3人快速开局";
                        else if (room.players.Length == 3)
                            fastStart.text.text = "2人快速开局";
                        fastStart.setVisible(true);
                    }
                }
            }

            fastStart.setVisible(false);
        }

        public override void showMaster()
        {
            base.showMaster();
        }

        public override void setReady(int index, bool b)
        {
            base.setReady(index,b);
        }
    }
}
