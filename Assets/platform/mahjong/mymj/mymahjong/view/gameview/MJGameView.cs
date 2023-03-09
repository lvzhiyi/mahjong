namespace platform.mahjong
{
    /// <summary>
    /// 游戏中的视图
    /// </summary>
    public class MJGameView : MJBaseGameView
    {
        /// <summary>
        /// 换三张视图
        /// </summary>
        MJHuanSZView huanszView;
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
        private MJPiaoStatusView piaoStatusView;
        /// <summary>
        /// 选飘视图
        /// </summary>
        private MJPiaoView selectPiaoView;
        /// <summary>
        /// 躺牌视图
        /// </summary>
        private MJLieView mjLieView;
        /// <summary>
        /// 玩家是否已经躺牌的视图
        /// </summary>
        private MJTangView tangViewImg;


        protected override void xinit()
        {
            base.xinit();

            if(transform.Find("huansanzhang")!=null)
            {
                huanszView = transform.Find("huansanzhang").GetComponent<MJHuanSZView>();
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
                piaoStatusView = transform.Find("piaostatus").GetComponent<MJPiaoStatusView>();
                piaoStatusView.init();
            }
          
            if (transform.Find("selectpiao") != null)
            {
                selectPiaoView = transform.Find("selectpiao").GetComponent<MJPiaoView>();
                selectPiaoView.init();
            }
           

            if (transform.Find("lieview") != null)
            {
                mjLieView = transform.Find("lieview").GetComponent<MJLieView>();
                mjLieView.init();
            }

            if (transform.Find("tang") != null)
            {
                tangViewImg = transform.Find("tang").GetComponent<MJTangView>();
                tangViewImg.init();
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
           

            if (mjLieView != null)
            {
                mjLieView.refresh();
                mjLieView.setVisible(false);
            }

            if (tangViewImg != null)
            {
                tangViewImg.refresh();
                tangViewImg.setVisible(false);
            }
        }
     

        protected override void xrefresh()
        {
            initUI();
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

     
        /// <summary>
        /// 显示躺牌的视图
        /// </summary>
        /// <param name="type"></param>
        public override void showLieView(int type)
        {
            if (!mjLieView.getVisible())
            {
                mjLieView.setVisible(true);
            }
            mjLieView.showView(type);
        }

        /// <summary>
        /// 隐藏躺牌视图
        /// </summary>
        public override void hideLieView()
        {
            mjLieView.setVisible(false);
        }

        public override void ShowTangImgView(int index,bool b)
        {
            if(!tangViewImg.getVisible())
                tangViewImg.setVisible(true);
            tangViewImg.showTang(index,b);
        }
    }
}
