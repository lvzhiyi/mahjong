using cambrian.uui;
using UnityEngine;
using UnityEngine.UI;

namespace platform.mahjong
{
    /// <summary>
    /// 基本的游戏视图
    /// </summary>
    public class MJBaseGameView:UnrealLuaSpaceObject
    {
        /// <summary>
        /// 中间视图
        /// </summary>
        protected MJIndexCenterView centerView;
        /// <summary>
        /// 手牌视图(包含了固定区牌)
        /// </summary>
        protected MJHandView handView;
        /// <summary>
        /// 弃牌区视图
        /// </summary>
        protected MJDisAbleView disableView;
        /// <summary>
        /// 操作视图
        /// </summary>
        protected MJOperateView operateView;
       
        /// <summary>
        /// 定缺视图
        /// </summary>
        protected MJDQView dqView;

        /// <summary>
        /// 显示玩家定缺的牌视图
        /// </summary>
        public MJDingQueCardView distypeOverView { get; set; }

        /// <summary>
        /// 听牌视图
        /// </summary>
        [HideInInspector] public MJTingCardView tingCardView;

        /// <summary>
        /// 玩家打的牌
        /// </summary>
        protected MJShowPlayCardView playerCardView;

        /// <summary>
        /// 出牌提醒
        /// </summary>
        protected UnrealLuaSpaceObject discardRedmin;

        protected AlphaTweener tweener;
        /// <summary>
        /// 剩余牌提醒
        /// </summary>
        protected SpritesList mj_sy_card_tx;

        /// <summary>
        /// 听牌灯泡
        /// </summary>
        protected Image tingdeng;

        private Vector3 tingcardPos;

        protected override void xinit()
        {
            centerView = transform.Find("indexcenter").GetComponent<MJIndexCenterView>();
            centerView.init();

            handView = transform.Find("hand").GetComponent<MJHandView>();
            handView.init();

            disableView = transform.Find("disable").GetComponent<MJDisAbleView>();
            disableView.init();

            if (transform.Find("operate") != null)
            {
                operateView = transform.Find("operate").GetComponent<MJOperateView>();
                operateView.init();
            }




            if (transform.Find("dingque") != null)
            {
                dqView = transform.Find("dingque").GetComponent<MJDQView>();
                dqView.init();
            }

            if (transform.Find("dingquecard") != null)
            {
                distypeOverView = transform.Find("dingquecard").GetComponent<MJDingQueCardView>();
                distypeOverView.init();
            }


            

         

            tingCardView = transform.Find("tingcard").GetComponent<MJTingCardView>();
            tingCardView.init();
            tingcardPos = tingCardView.transform.localPosition;

            playerCardView = transform.Find("sendCard").GetComponent<MJShowPlayCardView>();
            playerCardView.init();

            if (transform.Find("discardredmin") != null)
                discardRedmin = transform.Find("discardredmin").GetComponent<UnrealLuaSpaceObject>();
            if (transform.Find("four4") != null)
            {
                tweener = transform.Find("four4").GetComponent<AlphaTweener>();
                tweener.gameObject.SetActive(false);
                mj_sy_card_tx = transform.Find("four4").Find("img").GetComponent<SpritesList>();
            }
            if (transform.Find("tingdeng") != null)
                tingdeng = transform.Find("tingdeng").GetComponent<Image>();

        }

        public virtual void initUI()
        {
            centerView.refresh();
            handView.refresh();
          
            disableView.refresh();
          
            if (operateView != null)
            {
                operateView.refresh();
                operateView.setVisible(false);
            }
            if (dqView != null)
            {
                dqView.refresh();
                dqView.setVisible(false);
            }
            if (distypeOverView != null)
            {
                distypeOverView.setVisible(false);
                distypeOverView.refresh();
            }
           
            tingCardView.setVisible(false);
            playerCardView.setVisible(false);
           
            if (discardRedmin != null)
                discardRedmin.setVisible(false);
            if (tingdeng != null)
                tingdeng.gameObject.SetActive(false);
        }

        public virtual void showfour4(int index)
        {
            if (!tweener.gameObject.activeSelf)
                tweener.gameObject.SetActive(true);
            mj_sy_card_tx.ShowIndex(index);

            tweener.reset();
        }

        protected override void xrefresh()
        {
            initUI();
        }

        public virtual MJIndexCenterView getMjIndexCenterView()
        {
            return centerView;
        }

        /// <summary>
        /// 获取手牌区视图
        /// </summary>
        /// <returns></returns>
        public virtual MJHandView getHandView()
        {
            return handView;
        }

        /// <summary>
        /// 获取弃牌区视图
        /// </summary>
        /// <returns></returns>
        public virtual MJDisAbleView getDisAbleView()
        {
            return disableView;
        }

        /// <summary>
        /// 获取操作视图
        /// </summary>
        /// <returns></returns>
        public virtual MJOperateView getOperateView()
        {
            if (operateView == null) return null;
            //if (!operateView.getVisible())
            //    operateView.setVisible(true);
            return operateView;
        }

        public virtual void hideOperateView()
        {
            if (operateView != null)
                operateView.setVisible(false);
        }

        /// <summary>
        /// 获取定缺的视图
        /// </summary>
        /// <returns></returns>
        public virtual MJDQView getDQView()
        {
            if (!dqView.getVisible())
                dqView.setVisible(true);
            return dqView;
        }

        /// <summary>
        /// 显示定缺牌的视图
        /// </summary>
        public virtual void showDistypeCardView(int[] distype)
        {
            if (distype != null)
                distypeOverView.setDistypes(distype);
            distypeOverView.setVisible(distype != null);
        }

        public virtual void showSingleDistypeCard(int index, int distype)
        {
            distypeOverView.setSingleDistype(index, distype);
            distypeOverView.setVisible(true);
        }

        public virtual void setTingView(TingCardsInfo info = null)
        {
            if (operateView.getVisible())
            {
                if (operateView.transform.GetComponentsInChildren<UnrealLuaSpaceObject>().Length > 1)
                {
                    tingCardView.transform.localPosition = new Vector3(tingcardPos.x, tingcardPos.y + 136, tingcardPos.z);
                }
            }
            else
            {
                tingCardView.transform.localPosition = tingcardPos;
            }

            if (info == null)
            {
                tingCardView.setVisible(false);
                return;
            }

            tingCardView.setTingCardsInfo(info);
            tingCardView.refresh();
            tingCardView.setVisible(true);
        }


        /// <summary>
        /// 显示玩家打的牌
        /// </summary>
        /// <param name="card"></param>
        public virtual void showPlayerCardView(int card, int index)
        {
            playerCardView.showCard(card, index);
        }

        /// <summary>
        /// 出牌提醒，是否显示
        /// </summary>
        /// <param name="b"></param>
        public virtual void showDisCardRedmine(bool b)
        {
            if (discardRedmin != null)
                discardRedmin.setVisible(b);
        }

        public virtual void showTingDeng(bool b)
        {
            tingdeng.gameObject.SetActive(b);
        }


        /// <summary>
        /// 获取换三张视图
        /// </summary>
        /// <returns></returns>
        public virtual MJHuanSZView getHuanSZView()
        {
            return null;
        }

        /// <summary>
        /// 换三张结束视图
        /// </summary>
        /// <returns></returns>
        public virtual MJHuanSZOverView getHSZOverView()
        {
            return null;
        }

        /// <summary>
        /// 玩家选择飘
        /// </summary>
        /// <returns></returns>
        public virtual MJPiaoView getPiaoView()
        {
            return null;
        }

        /// <summary>
        /// 获取飘的状态
        /// </summary>
        /// <returns></returns>
        public virtual  MJPiaoStatusView getPiaoStatusView()
        {
            return null;
        }

        /// <summary>
        /// 获取胡牌视图
        /// </summary>
        /// <returns></returns>
        public virtual MJHuView getHuView()
        {
            return null;
        }


        /// <summary>
        /// 显示躺牌的视图
        /// </summary>
        /// <param name="type"></param>
        public virtual void showLieView(int type)
        {
            
        }

        /// <summary>
        /// 隐藏躺牌视图
        /// </summary>
        public virtual void hideLieView()
        {
           
        }

        public virtual void ShowTangImgView(int index, bool b)
        {
           
        }

    }
}
