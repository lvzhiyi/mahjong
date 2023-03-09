using UnityEngine.UI;

namespace platform.spotred.room
{
    public class CardBoxCardView:UnrealLuaSpaceObject
    {
        /// <summary>
        /// 翻开的牌
        /// </summary>
        private Image opencard;
        /// <summary>
        /// 未翻开的牌
        /// </summary>
        private Image card_bg;
        /// <summary>
        /// 背面牌(用于偷牌)
        /// </summary>
        private Image backcard;

        protected override void xinit()
        {
            this.opencard = this.transform.Find("opencard").GetComponent<Image>();
            this.card_bg = this.transform.Find("card_bg").GetComponent<Image>();
            this.backcard = this.transform.Find("backcard").GetComponent<Image>();
        }

        /// <summary>
        /// 获取翻开的牌
        /// </summary>
        /// <returns></returns>
        public Image getOpenCard()
        {
            return opencard;
        }

        public Image getCardBg()
        {
            return card_bg;
        }

        /// <summary>
        /// 获取偷牌背面图
        /// </summary>
        /// <returns></returns>
        public Image getbackCard()
        {
            return backcard;
        }

        protected override void xrefresh()
        {
            this.opencard.gameObject.SetActive(false);
            this.card_bg.gameObject.SetActive(false);
            this.backcard.gameObject.SetActive(false);
        }
    }
}
