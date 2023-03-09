using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 操作视图
    /// </summary>
    public class MJOperateView:UnrealLuaSpaceObject
    {
        /// <summary>
        /// 胡
        /// </summary>
        protected MJHuOperateView hu;
        /// <summary>
        /// 杠
        /// </summary>
        protected MJKongOperateView gang;
        /// <summary>
        /// 碰
        /// </summary>
        protected MJPongOperateView pong;
        /// <summary>
        /// 过
        /// </summary>
        protected MJCancelOperateView cancel;
        /// <summary>
        /// 躺
        /// </summary>
        protected MJTangOperateView tang;
        /// <summary>
        /// 报听
        /// </summary>
        protected MJBaoTingOperateView baoting;
        /// <summary>
        /// 报杠
        /// </summary>
        protected MJBaoKongOperateView baoKong;

        protected override void xinit()
        {
            hu = transform.Find("hu").GetComponent<MJHuOperateView>();
            hu.init();
            gang = transform.Find("gang").GetComponent<MJKongOperateView>();
            gang.init();
            pong = transform.Find("pong").GetComponent<MJPongOperateView>();
            pong.init();
            cancel = transform.Find("guo").GetComponent<MJCancelOperateView>();
            cancel.init();
            if(transform.Find("tang")!=null)
            {
                tang = transform.Find("tang").GetComponent<MJTangOperateView>();
                tang.init();
            }
            if (transform.Find("bao") != null)
            {
                baoting = transform.Find("bao").GetComponent<MJBaoTingOperateView>();
                baoting.init();
            }
            if (transform.Find("baokong") != null)
            {
                baoKong = transform.Find("baokong").GetComponent<MJBaoKongOperateView>();
                baoKong.init();
            }
        }

        protected override void xrefresh()
        {
            hide();
        }

        protected virtual void hide()
        {
            hu.setVisible(false);
            gang.setVisible(false);
            pong.setVisible(false);
            cancel.setVisible(false);
            if (tang != null)
                tang.setVisible(false);
            if (baoting != null)
                baoting.setVisible(false);
            if (baoKong != null)
                baoKong.setVisible(false);
        }  

        public virtual void showOperate(MJOperateInfoBean[] bean)
        {
            hide();

            ArrayList<UnrealLuaSpaceObject> list = new ArrayList<UnrealLuaSpaceObject>();

            for (int i=0;i<bean.Length;i++)
            {
                UnrealLuaSpaceObject btn = getShowButton(bean[i]);
                if (btn != null) list.add(btn);
            }

            float btn_x = 0;

            for (int i = list.count - 1; i >= 0; i--)
            {
                UnrealLuaSpaceObject btn = list.get(i);
                float width = btn.getWidth();
                DisplayKit.setLocalXY(btn.transform, -btn_x, 0);
                btn.setVisible(true);
                btn_x += width;
            }
            setVisible(list.count>0);
        }

        public virtual UnrealLuaSpaceObject getShowButton(MJOperateInfoBean bean)
        {
            int type = bean.operateType;
            switch (type)
            {
                case MJOperate.CAN_CANCEL:
                    return cancel;
                case MJOperate.CAN_PONG:
                    pong.setCard(bean);
                    pong.refresh();
                    return pong;
                case MJOperate.CAN_KONG:
                    gang.setBean(bean);
                    gang.refresh();
                    return gang;
                case MJOperate.CAN_HU:
                    hu.setBean(bean);
                    hu.refresh();
                    return hu;
                case MJOperate.CAN_TANG:
                    return tang;
                case MJOperate.CAN_BAOPAI:
                    return baoting;
                case MJOperate.CAN_BAO_KONG:
                    return baoKong;
            }
            return null;
        }
    }
}
