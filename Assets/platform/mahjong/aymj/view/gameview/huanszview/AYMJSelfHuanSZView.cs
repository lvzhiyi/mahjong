using UnityEngine.UI;

namespace platform.mahjong
{
    public class AYMJSelfHuanSZView : MJSelfHuanSZView
    {
        protected override void xinit()
        {
            if (transform.Find("hszview") != null)
                huansz = transform.Find("hszview").GetComponent<UnrealLuaSpaceObject>();
            hpicon = transform.Find("bg").GetComponent<Image>();
        }

        public override void showHuanOperate()
        {
            if (huansz != null)
                huansz.setVisible(true);
            hpicon.gameObject.SetActive(false);
        }

        public override  void showHpicon()
        {
            if (huansz != null)
                huansz.setVisible(false);
            hpicon.gameObject.SetActive(true);
        }
    }
}
