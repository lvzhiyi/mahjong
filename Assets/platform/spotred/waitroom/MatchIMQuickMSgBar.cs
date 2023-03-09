using basef.im;
using cambrian.common;
using UnityEngine;

namespace platform.spotred.waitroom
{
    public class MatchIMQuickMSgBar : UnrealLuaSpaceObject
    {

        public static int MJ_QUICK_TYPE=0,CP_QUICK_TYPE=1;

        private UnrealTextField text;

        private UnrealSprite icon;

        [HideInInspector]
        public IMQuickMsg msg;

        public bool isMove;

        private CountdownVisibleProcess visibleProcess;

        private MatchExpressMsgView expressMsg;


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

            if (this.transform.Find("expressview") != null)
            {
                this.expressMsg = this.transform.Find("expressview").GetComponent<MatchExpressMsgView>();
                this.expressMsg.init();
            }

            this.visibleProcess = this.GetComponent<CountdownVisibleProcess>();

        }

        public void setIMQuickMsg(IMQuickMsg msg)
        {
            this.msg = msg;
            visibleProcess.starttime = TimeKit.currentTimeMillis;
        }

        public void showIMTextMsg(IMTextMsg msg)
        {
            string cc = msg.getContent();
            Font font = Resources.Load<Font>("zyj");
            int fontsize = 24;
            font.RequestCharactersInTexture(cc, fontsize, FontStyle.Normal);
            float width = 0f;
            CharacterInfo characterInfo;
            for (int i = 0; i < cc.Length; i++)
            {
                font.GetCharacterInfo(cc[i], out characterInfo, fontsize);
                width += characterInfo.advance;
            }
            this.bar_bg.GetComponent<RectTransform>().sizeDelta = new Vector2(width + 40, 60);
            this.text.text = cc;
            if (isMove)
                DisplayKit.setLocalX(this.text.gameObject, -width - 20);

            visibleProcess.starttime = TimeKit.currentTimeMillis;

        }

        protected override void xrefresh()
        {
            base.xrefresh();
            if (msg.type == IMQuickMsg.TYPE_TEXT)
            {
                string cc = msg.info;
                Font font = Resources.Load<Font>("zyj");
                int fontsize = 24;
                font.RequestCharactersInTexture(cc, fontsize, FontStyle.Normal);
                float width = 0f;
                CharacterInfo characterInfo;
                for (int i = 0; i < cc.Length; i++)
                {
                    font.GetCharacterInfo(cc[i], out characterInfo, fontsize);
                    width += characterInfo.advance;
                }
                this.bar_bg.GetComponent<RectTransform>().sizeDelta = new Vector2(width + 40, 60);
                this.text.text = cc;
                if(isMove)
                    DisplayKit.setLocalX(this.text.gameObject,-width-20);

            }
            else
            {
                if(expressMsg!=null) expressMsg.showIndex(msg.value);
            }
        }
    }
}
