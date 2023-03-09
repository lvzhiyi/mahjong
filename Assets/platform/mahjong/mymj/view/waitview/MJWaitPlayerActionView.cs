using UnityEngine.UI;

namespace platform.mahjong
{
    /// <summary>
    /// 准备阶段
    /// </summary>
    public class MJWaitPlayerActionView:UnrealLuaSpaceObject
    {
        /// <summary>
        /// 准备图片
        /// </summary>
        private Image ready;

        protected override void xinit()
        {
            ready = transform.Find("ready").GetComponent<Image>();
        }

        protected override void xrefresh()
        {
            
        }

        public void setReady(bool b)
        {
            ready.gameObject.SetActive(b);
        }
    }
}
