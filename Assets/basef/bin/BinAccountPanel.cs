using UnityEngine;

namespace basef.bin
{
    /// <summary> 绑定账号 </summary>
    public class BinAccountPanel : UnrealLuaPanel
    {
        [HideInInspector] public UnrealTextField text;

        protected override void xinit()
        {
            this.text = this.transform.Find("Canvas").Find("number").Find("text").GetComponent<UnrealTextField>();
        }

        protected override void xrefresh()
        {
            this.text.text = "";

        }
    }
}
