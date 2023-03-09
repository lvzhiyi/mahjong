namespace platform.mahjong
{
    /// <summary>
    /// 游戏中的视图
    /// </summary>
    public class AYMJGameView : MJBaseGameView
    {
        /// <summary>
        /// 换三张视图
        /// </summary>
        AYMJHuanSZView huanszView;
        /// <summary>
        /// 换三张结束视图
        /// </summary>
        MJHuanSZOverView huanszOverView;
        /// <summary>
        /// 胡牌视图
        /// </summary>
        MJHuView huView;
        /// <summary>
        /// 飘状态
        /// </summary>
        private AYMJPiaoStatusView piaoStatusView;
        /// <summary>
        /// 选飘视图
        /// </summary>
        private AYMJPiaoView selectPiaoView;
        /// <summary>
        /// 报牌视图
        /// </summary>
        private AYMJTingCardView baoCardView;
        /// <summary>
        /// 报杠视图
        /// </summary>
        private AYMJBaoKongView baoKongView;


        protected override void xinit()
        {
            base.xinit();

            if(transform.Find("huansanzhang")!=null)
            {
                huanszView = transform.Find("huansanzhang").GetComponent<AYMJHuanSZView>();
                huanszView.init();
            }

            if (transform.Find("huanszover") != null)
            {
                huanszOverView = transform.Find("huanszover").GetComponent<MJHuanSZOverView>();
                huanszOverView.init();
            }
            if(transform.Find("huindexview")!=null)
            {
                huView = transform.Find("huindexview").GetComponent<MJHuView>();
                huView.init();
            }
           
            if(transform.Find("piaostatus")!=null)
            {
                piaoStatusView = transform.Find("piaostatus").GetComponent<AYMJPiaoStatusView>();
                piaoStatusView.init();
            }
          
            if (transform.Find("selectpiao") != null)
            {
                selectPiaoView = transform.Find("selectpiao").GetComponent<AYMJPiaoView>();
                selectPiaoView.init();
            }

            if (transform.Find("tingview") != null)
            {
                baoCardView = transform.Find("tingview").GetComponent<AYMJTingCardView>();
                baoCardView.init();
            }

            if (transform.Find("baokongview") != null)
            {
                baoKongView = transform.Find("baokongview").GetComponent<AYMJBaoKongView>();
                baoKongView.init();
            }

        }

        public override void initUI()
        {
            base.initUI();
            if(huanszView!=null)
            {
                huanszView.refresh();
                huanszView.setVisible(false);
            }


            if (huanszOverView != null)
            {
                huanszOverView.refresh();
                huanszOverView.setVisible(false);
            }

            if (huView != null)
            {
                huView.refresh();
                huView.setVisible(false);
            }

            if (selectPiaoView != null)
            {
                selectPiaoView.refresh();
                selectPiaoView.setVisible(false);
            }

            if (piaoStatusView != null)
            {
                piaoStatusView.refresh();
                piaoStatusView.setVisible(false);
            }

            baoCardView.refresh();

            baoKongView.setVisible(false);
        }
     

        protected override void xrefresh()
        {
            initUI();
        }

        /// <summary>
        /// 获取报杠的视图
        /// </summary>
        /// <returns></returns>
        public AYMJBaoKongView getBaoKongView()
        {
            return baoKongView;
        }


        /// <summary>
        /// 获取报牌视图
        /// </summary>
        /// <returns></returns>
        public AYMJTingCardView getBaoCardView()
        {
            return baoCardView;
        }

        /// <summary>
        /// 获取换三张视图
        /// </summary>
        /// <returns></returns>
        public override MJHuanSZView getHuanSZView()
        {
            if (!huanszView.getVisible())
                huanszView.setVisible(true);
            return huanszView;
        }

        /// <summary>
        /// 换三张结束视图
        /// </summary>
        /// <returns></returns>
        public override MJHuanSZOverView getHSZOverView()
        {
            if (!huanszOverView.getVisible())
                huanszOverView.setVisible(true);
            return huanszOverView;
        }

        /// <summary>
        /// 玩家选择飘
        /// </summary>
        /// <returns></returns>
        public override MJPiaoView getPiaoView()
        {
            if(!selectPiaoView.getVisible())
                selectPiaoView.setVisible(true);
            return selectPiaoView;
        }

        /// <summary>
        /// 获取飘的状态
        /// </summary>
        /// <returns></returns>
        public override MJPiaoStatusView getPiaoStatusView()
        {
            if(!piaoStatusView.getVisible())
                piaoStatusView.setVisible(true);
            return piaoStatusView;
        }



        /// <summary>
        /// 获取胡牌视图
        /// </summary>
        /// <returns></returns>
        public override MJHuView getHuView()
        {
            if (!huView.getVisible())
                huView.setVisible(true);
            return huView;
        }
    }
}
