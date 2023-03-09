using scene.game;
using UnityEngine;

namespace basef.im
{
    /// <summary>
    /// 快捷会话条 （牌局中,头像上显示的）
    /// </summary>
    public class IMQuickMsgBar: UnrealLuaSpaceObject
    {
        UnrealTextField text;

        UnrealSprite icon;

        [HideInInspector]
        public IMQuickMsg msg;

        protected override void xinit()
        {
            base.xinit();
            if (this.transform.Find("text") != null)
            {
                this.text = this.transform.Find("text").GetComponent<UnrealTextField>();
                this.text.init();
            }
            if (this.transform.Find("icon") != null)
            {
                this.icon = this.transform.Find("icon").GetComponent<UnrealSprite>();
                this.icon.init();
            }
        }

        public void setIMQuickMsg(IMQuickMsg msg)
        {
            this.msg = msg;
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            if (msg.type == IMQuickMsg.TYPE_TEXT)
            {
                this.text.text = msg.info;
            }
            else
            {
                this.icon.setSprite(CacheManager.emojis[msg.value]);
            }
        }
    }
}
