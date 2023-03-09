using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine.UI;

namespace basef.arena
{
    public class ArenaExtensionDetailBar : ScrollBar
    {
        /// <summary>
        /// 任务
        /// </summary>
        private Text task;
        /// <summary>
        /// 剩余任务
        /// </summary>
        private Text surplustask;
        /// <summary>
        /// 交易类型
        /// </summary>
        private Text type;
        /// <summary>
        /// 备注
        /// </summary>
        private Text info;
        /// <summary>
        /// 时间
        /// </summary>
        private Text time;

        public TaskBill bill;

        protected override void xinit()
        {
            task = transform.Find("task").GetComponent<Text>();
            surplustask = transform.Find("sytask").GetComponent<Text>();
            type = transform.Find("type").GetComponent<Text>();
            info = transform.Find("beizhu").GetComponent<Text>();
            time = transform.Find("time").GetComponent<Text>();
        }

        public void setBill(TaskBill bill)
        {
            this.bill = bill;
        }

        protected override void xrefresh()
        {
            setColor();
            task.text = bill.getValue().ToString();
            surplustask.text = bill.getTask().ToString();
            type.text = bill.getTaskType();
            info.text = bill.info;
            time.text = TimeKit.format(bill.time,"yy-MM-dd HH:mm:ss");
        }

        private void setColor()
        {
            if (bill.getValue() >= 0)
            {
                task.color = ColorKit.getColor(47,182,38,255);
            }
            else
            {
                task.color = ColorKit.getColor(238,1,1,255);
            }
        }
    }
}
