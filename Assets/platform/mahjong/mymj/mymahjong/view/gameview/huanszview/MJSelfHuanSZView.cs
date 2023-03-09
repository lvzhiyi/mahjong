using UnityEngine.UI;

namespace platform.mahjong
{
    public class MJSelfHuanSZView : UnrealLuaSpaceObject
    {
        protected UnrealLuaSpaceObject huansz;

        /// <summary>
        /// 已换牌
        /// </summary>
        protected Image hpicon;

        protected override void xinit()
        {
            if (transform.Find("hszview") != null)
                huansz = transform.Find("hszview").GetComponent<UnrealLuaSpaceObject>();
            hpicon = transform.Find("bg").GetComponent<Image>();
        }

        public virtual void showHuanOperate()
        {
            if (huansz != null)
                huansz.setVisible(true);
            hpicon.gameObject.SetActive(false);
        }

        public virtual void showHpicon()
        {
            if (huansz != null)
                huansz.setVisible(false);
            hpicon.gameObject.SetActive(true);
        }
    }
}
