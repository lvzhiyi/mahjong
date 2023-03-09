
using cambrian.common;
using System;

namespace basef.joinroom
{
    /// <summary>
    /// 加入房间面板
    /// </summary>
    public class SpotJoinRoomPanel:UnrealLuaPanel
    {
        Action<long> callback;

        UnrealTextField[] texts;

        long value;
        int numlen;


        protected override void xinit()
        {
            base.xinit();
            this.numlen = transform.Find("Canvas").Find("roomnum").Find("texts").childCount;
            texts = new UnrealTextField[numlen];
            for (int i = 0; i < numlen; i++)
            {
                texts[i] = transform.Find("Canvas").Find("roomnum").Find("texts").Find("text" + i).GetComponent<UnrealTextField>();
                texts[i].init();
            }

            for (int i = 0; i < 12; i++)
            {
                UnrealButton btn = transform.Find("Canvas").Find("num").Find("num" + i).GetComponent<UnrealButton>();
                btn.init();
                btn.setState(UnrealButton.NORMAL);
            }
            this.clear();
            resizeDelta(new Margin());
        }


        /// <summary>
        /// 设置回调函数
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="value"></param>
        public void setCallBack(Action<long> callback)
        {
            this.callback = callback;
        }
        /// <summary>
        /// 输入
        /// </summary>
        /// <param name="str"></param>
        public void input(string str)
        {
            this.value = this.value * 10 + StringKit.parseLong(str);
            this.showValue();
            if (value >= 100000)
                confirm();

        }
        /// <summary>
        /// 退格
        /// </summary>
        public void del()
        {
            this.value = this.value / 10;
            this.showValue();
        }

        private void showValue()
        {
            string valuestr = "";
            if (value != 0)
            {
                valuestr = value + "";
            }
            char[] strs = valuestr.ToCharArray();
            for (int i = 0; i < this.numlen; i++)
            {
                if (strs.Length > i)
                {
                    texts[i].text = strs[i] + "";
                }
                else
                {
                    texts[i].text = "";
                }
            }
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void clear()
        {
            this.value = 0;
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].text = "";
            }
        }
        /// <summary>
        /// 确定
        /// </summary>
        public void confirm()
        {
            long index = value;
            callback(index);
            callback = null;
            this.clear();
            setVisible(false);
        }
        /// <summary>
        /// 取消
        /// </summary>
        public void cancel()
        {
            callback = null;
            clear();
            setVisible(false);
        }
    }
}
