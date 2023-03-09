using UnityEngine;
using UnityEngine.UI;

namespace basef.lobby
{
    /// <summary>
    /// 大厅公告
    /// </summary>
    public class LobbyNoticeBar:UnrealLuaSpaceObject
    {
        public Image bg;

        protected override void xinit()
        {
            this.bg = this.transform.Find("bg").GetComponent<Image>();
        }


        public void setImage(Sprite sprite)
        {
            this.bg.sprite = sprite;
        }
    }
}
