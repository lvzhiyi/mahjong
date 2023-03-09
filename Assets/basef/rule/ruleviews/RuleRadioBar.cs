using UnityEngine;
using UnityEngine.UI;

namespace basef.rule
{
    /// <summary>
    /// 预制件上引用
    /// </summary>
    public class RuleRadioBar: IdBar
    {
        Text normal_text;

        Text active_text;

        /// <summary>
        /// 数量
        /// </summary>
        [HideInInspector] public int count;
        /// <summary>
        /// rule,属性true,还是false
        /// </summary>
        [HideInInspector] public bool isCheck;

        [HideInInspector] public bool isLocked;
        /// <summary>
        /// 锁定图标
        /// </summary>
        private Image lockedicon;

        protected override void xinit()
        {
            base.xinit();
            this.normal_text = this.transform.Find("normal").GetComponent<Text>();
            this.active_text = this.transform.Find("active").GetComponent<Text>();
            if (transform.Find("lock") != null)
                lockedicon = transform.Find("lock").GetComponent<Image>();
        }

        public void setTitle(string title, bool isLocked)
        {
            normal_text.text = title;
            active_text.text = title;
            this.isLocked = isLocked;
           
            setId(index_space);
        }

        /// <summary>
        /// 设置数量
        /// </summary>
        /// <param name="count"></param>
        public void setCount(int count)
        {
            this.count = count;
        }

        /// <summary>
        /// rule,属性true,还是false
        /// </summary>
        /// <param name="check"></param>
        public void setCheck(bool check)
        {
            this.isCheck = check;
        }

        public override void setState(bool state)
        {
            base.setState(state);
            normal_text.gameObject.SetActive(!state);
            active_text.gameObject.SetActive(state);
            if (lockedicon != null)
                lockedicon.gameObject.SetActive(isLocked&&state);
        }
    }
}
