namespace platform.mahjong
{
    public class ReplayAYMJRoomPanel : AYMJRoomPanel
    {
        private AYMJReplay replay;

        public AYMJReplayControlView rcview;

        private MJOperateView bottom_operate;

        private MJOperateView right_operate;

        private MJOperateView top_operate;

        private MJOperateView left_operate;
        /// <summary>
        /// 显示回放的按钮
        /// </summary>
        private UnrealCheckBox switchbox;


        protected override void xinit()
        {
            base.xinit();
            this.bottom_operate = content.Find("poperate").Find("down").GetComponent<MJOperateView>();
            this.bottom_operate.init();
            this.right_operate = content.Find("poperate").Find("right").GetComponent<MJOperateView>();
            this.right_operate.init();
            this.top_operate = content.Find("poperate").Find("top").GetComponent<MJOperateView>();
            this.top_operate.init();
            this.left_operate = content.Find("poperate").Find("left").GetComponent<MJOperateView>();
            this.left_operate.init();

            this.rcview = content.Find("replay").GetComponent<AYMJReplayControlView>();
            this.rcview.init();

            switchbox = content.Find("switch").GetComponent<UnrealCheckBox>();
            switchbox.init();
        }

        public void setRePlay(AYMJReplay replay)
        {
            this.replay = replay;
        }

        public AYMJReplay getRePlay()
        {
            return replay;
        }

        public void doReplay()
        {
            if (AYMJMatch.match == null) return;
            this.replay.doOrder();
        }

        public void controlReplay(int type)
        {
            rcview.controlReplay(type, this.replay);
        }

        protected override void xrefresh()
        {
            switchbox.setState(UnrealCheckObject.NORMAL);
            rcview.setVisible(true);
            lastSendCard.gameObject.SetActive(false);
            headView.refresh();
            topView.refresh();
            roomindex.text = Room.room.getRoomIndex().ToString();
            hidesOperateView();
        }

        public void hidesOperateView()
        {
            bottom_operate.setVisible(false);
            right_operate.setVisible(false);
            top_operate.setVisible(false);
            left_operate.setVisible(false);
        }

        public void showSingleOperate(int index, int operate)
        {
            MJOperateInfoBean[] bean = AYMJMatch.match.getMJOperateInfos(operate, MJCard.CNO, AYMJMatch.match.isKongSups(),index);

            switch (getPlayerIndex(index))
            {
                case LOC_DOWN:
                    bottom_operate.showOperate(bean);
                    bottom_operate.setVisible(true);
                    break;
                case LOC_RIGHT:
                    right_operate.showOperate(bean);
                    right_operate.setVisible(true);
                    break;
                case LOC_TOP:
                    top_operate.showOperate(bean);
                    top_operate.setVisible(true);
                    break;
                case LOC_LEFT:
                    left_operate.showOperate(bean);
                    left_operate.setVisible(true);
                    break;
            }
        }

        /// <summary>
        /// 显示操作
        /// </summary>
        /// <param name="operates"></param>
        /// <param name="paidui"></param>
        public void showOperates(int[] operates, int card)
        {
            hidesOperateView();
            MJOperateInfoBean[] bean = null;
            for (int i = 0; i < operates.Length; i++)
            {
                if (operates[i] > 0)
                    bean = AYMJMatch.match.getMJOperateInfos(operates[i], card, AYMJMatch.match.isKongSups(),i);
                else
                {
                    continue;
                }

                switch (getPlayerIndex(i))
                {
                    case LOC_DOWN:
                        bottom_operate.showOperate(bean);
                        bottom_operate.setVisible(true);
                        break;
                    case LOC_RIGHT:
                        right_operate.showOperate(bean);
                        right_operate.setVisible(true);
                        break;
                    case LOC_TOP:
                        top_operate.showOperate(bean);
                        top_operate.setVisible(true);
                        break;
                    case LOC_LEFT:
                        left_operate.showOperate(bean);
                        left_operate.setVisible(true);
                        break;
                }
            }
        }
    }
}
