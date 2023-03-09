using cambrian.common;
using basef.rule;
using UnityEngine;
using cambrian.uui;

namespace platform.spotred.room
{
    /// <summary>
    /// 操作区(碰,过,吃,胡,吃吐......)
    /// </summary>
    public class OperateView:UnrealLuaSpaceObject
    {

        // ===============可操作状态值（数值越大优先级越高）================
        /** 状态：可以过 */
        public const  int CAN_CANCEL = 1 << 0;
        /** 状态：可以出牌：附带不可出的牌 */
        public const int CAN_DISCARD = 1 << 1;
        /** 状态：可以报牌 */
        public const int CAN_BAOPAI = 1 << 2;
        /** 状态：可以昭碰 */
        public const int CAN_ZHAOPONG = 1 << 3;
        /** 状态：可以昭吃 */
        public const int CAN_ZHAOCHOW = 1 << 4;
        /** 状态：可以吃吐 */
        public const int CAN_CHOWTU = 1 << 5;
        /** 状态：可以吃 */
        public const int CAN_CHOW = 1 << 6;
        /** 状态：可以碰（扯，滑） */
        public const int CAN_PONG = 1 << 7;
        /** 状态： 可以巴杠牌 */
        public const int CAN_KONG = 1 << 8;
        /** 状态：可以杠4张 */
        public const int CAN_KONG4 = 1 << 9;
        /** 状态：可以胡 */
        public const int CAN_HU = 1 << 10;
        /** 状态：可以滑牌（偷牌） */
        public const int CAN_SLIP = 1 << 11;
        /** 状态：可以挡(挡) */
        public const int CAN_DANG = 1 << 12;
        /** 状态：可以单张偷牌 */
        public const int CAN_SINGLE = 1 << 13;
        /// <summary>
        /// 状态：可以飘
        /// </summary>
        public const int CAN_PIAO = 1 << 14;

        /// <summary>
        /// 对
        /// </summary>
        private UnrealScaleButton dui;
        /// <summary>
        /// 对按钮图片
        /// </summary>
        private SpritesList dui_img;

        /// <summary>
        /// 招吃
        /// </summary>
        private UnrealScaleButton zaochi;
        /// <summary>
        /// 胡
        /// </summary>
        private UnrealScaleButton hu;
        /// <summary>
        /// 过
        /// </summary>
        private UnrealScaleButton guo;
        /// <summary>
        /// 吃
        /// </summary>
        private UnrealScaleButton chi;
        /// <summary>
        /// 招碰
        /// </summary>
        private UnrealScaleButton zaopeng;
        /// <summary>
        /// 吃吐
        /// </summary>
        private UnrealScaleButton chitu;

        /// <summary>
        /// 自己手动滑牌
        /// </summary>
        private UnrealScaleButton hua_1;
        /// <summary>
        /// 滑按钮图片
        /// </summary>
        private SpritesList hua_img;

        /// <summary>
        /// 巴牌
        /// </summary>
        private UnrealScaleButton ba;
        /// <summary>
        /// 巴按钮图片
        /// </summary>
        private SpritesList ba_img;

        /// <summary>
        /// 报牌
        /// </summary>
        private UnrealScaleButton bao;
        /// <summary>
        /// 挡
        /// </summary>
        private UnrealScaleButton dang;
        /// <summary>
        /// 单张偷牌
        /// </summary>
        private UnrealScaleButton single_tou;
        /// <summary>
        /// 飘
        /// </summary>
        private UnrealScaleButton piao;

        /// <summary>
        /// 按钮列表
        /// </summary>
        protected ArrayList<UnrealScaleButton> list;

        public Rule rule;
        /// <summary>
        /// 操作类型
        /// </summary>
        [HideInInspector] public static int[] operatesType = null;

        protected override void xinit()
        {
            base.xinit();
            this.dui = this.transform.Find("dui").GetComponent<UnrealScaleButton>();
            this.dui_img = this.dui.transform.Find("normal").GetComponent<SpritesList>();

            this.zaochi = this.transform.Find("zaochi").GetComponent<UnrealScaleButton>();
            this.hu = this.transform.Find("hu").GetComponent<UnrealScaleButton>();
            this.guo = this.transform.Find("guo").GetComponent<UnrealScaleButton>();
            this.chi = this.transform.Find("chi").GetComponent<UnrealScaleButton>();
            this.zaopeng = this.transform.Find("zaopeng").GetComponent<UnrealScaleButton>();
            this.chitu = this.transform.Find("chitu").GetComponent<UnrealScaleButton>();

            this.hua_1 = this.transform.Find("hua_1").GetComponent<UnrealScaleButton>();
            this.hua_img = this.hua_1.transform.Find("normal").GetComponent<SpritesList>();

            this.ba = this.transform.Find("kong").GetComponent<UnrealScaleButton>();
            this.ba_img = this.ba.transform.Find("normal").GetComponent<SpritesList>();

            this.bao = this.transform.Find("bao").GetComponent<UnrealScaleButton>();
            this.dang = this.transform.Find("tuidang").GetComponent<UnrealScaleButton>();
            this.single_tou = this.transform.Find("singletou").GetComponent<UnrealScaleButton>();
            this.piao = this.transform.Find("piao").GetComponent<UnrealScaleButton>();
            operatesType = new[]
            {
                CAN_PIAO, CAN_SINGLE, CAN_DANG, CAN_KONG, CAN_HU, CAN_BAOPAI, CAN_PONG, CAN_CHOW, CAN_ZHAOPONG,
                CAN_ZHAOCHOW,
                CAN_CHOWTU, CAN_SLIP, CAN_CANCEL
            };
        }

        protected override void xrefresh()
        {
            hideAllBtn();
        }

        public void hideAllBtn()
        {
            this.dui.setVisible(false);
            this.zaochi.setVisible(false);
            this.hu.setVisible(false);
            this.guo.setVisible(false);
            this.chi.setVisible(false);
            this.zaopeng.setVisible(false);
            this.chitu.setVisible(false);
            this.hua_1.setVisible(false);
            this.ba.setVisible(false);
            this.bao.setVisible(false);
            this.dang.setVisible(false);
            this.single_tou.setVisible(false);
            this.piao.setVisible(false);
        }

        /// <summary>
        /// 操作状态,剩余牌数
        /// </summary>
        /// <param name="operate"></param>
        /// <param name="paidui"></param>
        /// <returns></returns>
        public virtual int showButton(int operate,int paidui)
        {
            hideAllBtn();

            if (paidui <= 4) //黄四张
            {
                if (rule.getRuleAtribute("4four")) 
                    paidui = 0;
            }

            if (paidui <= 2 && rule.sid == 1016) // 安岳长牌
            {
                paidui = 0;
            }

            if (paidui <= 3 && rule.sid == 1017)// 金堂考考
            {
                paidui = 0;
            }

            if (paidui <= 1) //广元长牌
            {
                if (rule.getRuleAtribute("huang1"))
                    paidui = 0;
            }

            if (operate == 0 || (StatusKit.isStatus(operate, CAN_CANCEL) && paidui > 0)) 
                return 0;

            ArrayList<int> ll = new ArrayList<int>();
            for (int i = 0; i < operatesType.Length; i++)
            {
                if ((operate & operatesType[i]) == operatesType[i])
                {
                    ll.add(operatesType[i]);
                }
            }

            list = new ArrayList<UnrealScaleButton>();

            for (int i = 0; i < ll.count; i++)
            {
                UnrealScaleButton btn = getShowButton(ll.get(i));
                if (btn != null) list.add(btn);
            }

            float btn_x = 0;

            for (int i = list.count - 1; i >= 0; i--)
            {
                UnrealScaleButton btn = list.get(i);
                float width = btn.getWidth();
                DisplayKit.setLocalXY(btn.transform, -btn_x, 0);
                btn.setVisible(true);
                btn_x += width;
            }

            float w = getWidth();
            if (list.count > 1)
            {
                float w_1 = w/2;
                if (list.count%2 != 0)
                    w_1 = 0;
                DisplayKit.setLocalX(this, w*(list.count/2) - w_1);
            }
            else
            {
                DisplayKit.setLocalX(this, w/2);
            }
            return list.count;
        }

        /// <summary>
        /// 显示有哪些按钮
        /// </summary>
        /// <param name="ll"></param>
        /// <returns></returns>
        public int showButtonList(ArrayList<int> ll)
        {
            hideAllBtn();

            list = new ArrayList<UnrealScaleButton>();
            for (int i = 0; i < ll.count; i++)
            {
                UnrealScaleButton btn = getShowButton(ll.get(i));
                if (btn != null) list.add(btn);
            }

            float btn_x = 0;

            for (int i = list.count - 1; i >= 0; i--)
            {
                UnrealScaleButton btn = list.get(i);
                float width = btn.getWidth();
                DisplayKit.setLocalXY(btn.transform, -btn_x, 0);
                btn.setVisible(true);
                btn_x += width;
            }

            float w = getWidth();
            if (list.count > 1)
            {
                float w_1 = w / 2;
                if (list.count % 2 != 0)
                    w_1 = 0;
                DisplayKit.setLocalX(this, w * (list.count / 2) - w_1);
            }
            else
            {
                DisplayKit.setLocalX(this, w/2);
            }
            return list.count;
        }

        /// <summary>
        /// 小家需要显示的按钮
        /// </summary>
        /// <param name="operate"></param>
        /// <returns></returns>
        public static ArrayList<int> getShowOperate(int operate)
        {
            ArrayList<int> ll = new ArrayList<int>();
            if (StatusKit.hasStatus(operate, CAN_PONG))
                ll.add(CAN_SLIP);
            for (int i = 0; i < operatesType.Length; i++)
            {
                if ((operate & operatesType[i]) == operatesType[i] && operatesType[i] != CAN_PONG)
                {
                    ll.add(operatesType[i]);
                }
            }
            return ll;
        }


        public static ArrayList<int> getList(int operate)
        {
            ArrayList<int> ll = new ArrayList<int>();

            for (int i = 0; i < operatesType.Length; i++)
            {
                if ((operate & operatesType[i]) == operatesType[i] && operatesType[i] != CAN_PONG)
                {
                    ll.add(operatesType[i]);
                }
            }
            if ((operate & CAN_PONG) == CAN_PONG)
                ll.add(-1);
            return ll;
        }


        public UnrealScaleButton getShowButton(int type)
        {
            int index = Room.room.roomRule.rule.sid == 1017 ? 1 : 0;
            switch (type)
            {
                case CAN_PONG:
                    this.dui_img.ShowIndex(index);
                    return this.dui;
                case CAN_ZHAOCHOW:
                    return this.zaochi;
                case CAN_HU:
                    return this.hu;
                case CAN_CANCEL:
                    return this.guo;
                case CAN_CHOW:
                    return this.chi;
                case CAN_ZHAOPONG:
                    return this.zaopeng;
                case CAN_CHOWTU:
                    return this.chitu;
                case CAN_SLIP:
                    this.hua_img.ShowIndex(index);
                    return this.hua_1;
                case CAN_KONG:
                    this.ba_img.ShowIndex(index);
                    return this.ba;
                case CAN_BAOPAI:
                    return this.bao;
                case CAN_DANG:
                    return this.dang;
                case CAN_SINGLE:
                    return this.single_tou;
                case CAN_PIAO:
                    return this.piao;
                case -1:
                    return this.hua_1;
            }
            return null;
        }
    }
}
