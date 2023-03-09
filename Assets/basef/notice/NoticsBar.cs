using UnityEngine;

namespace basef.notice
{
    public class NoticsBar : UnrealLuaSpaceObject
    {

        [HideInInspector] public ScrollNotice notice;

        UnrealTextField text;

        protected override void xinit()
        {
            base.xinit();
            this.text = this.transform.Find("text").GetComponent<UnrealTextField>();
        }

        public void setNotice(ScrollNotice notice)
        {
            this.notice = notice;
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            string cc = notice.content;
            Font font = Resources.Load<Font>("zyj");
            int fontsize = 18;
            font.RequestCharactersInTexture(cc, fontsize, FontStyle.Normal);
            float width = 0f;
            CharacterInfo characterInfo;
            for (int i = 0; i < cc.Length; i++)
            {
                font.GetCharacterInfo(cc[i], out characterInfo, fontsize);
                width += characterInfo.advance;
            }
            this.setWidth(width+20);
            this.text.text = cc;
        }
    }
}
