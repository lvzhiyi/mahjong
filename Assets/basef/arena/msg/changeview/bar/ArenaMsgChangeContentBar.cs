using basef.player;
using cambrian.common;
using cambrian.unreal.scroll;

namespace basef.arena
{
    /// <summary> 退出申请 内容 bar </summary>
    public class ArenaMsgChangeContentBar : ScrollBar
    {
        ArenaMemberChangeRecord data;

        /// <summary> 时间 </summary>
        private UnrealTextField time;

        /// <summary> 用户名 </summary>
        private UnrealTextField username;

        /// <summary> 用户ID </summary>
        private UnrealTextField usersid;

        /// <summary> 类型 </summary>
        private UnrealTextField type;

        /// <summary> 变动方式 </summary>
        private UnrealTextField changetype;

        /// <summary> 头像 </summary>
        private PlayerHeadView head;

        protected override void xinit()
        {
            head = transform.Find("head").GetComponent<PlayerHeadView>();
            head.init();

            time = transform.Find("time").GetComponent<UnrealTextField>();
            time.init();

            username = transform.Find("name").GetComponent<UnrealTextField>();
            username.init();

            usersid = transform.Find("usersid").GetComponent<UnrealTextField>();
            usersid.init();

            type = transform.Find("type").GetComponent<UnrealTextField>();
            type.init();

            changetype = transform.Find("changetype").GetComponent<UnrealTextField>();
            changetype.init();
        }

        protected override void xrefresh()
        {
            setTime();
            username.text = data.nickname;
            usersid.text = data.dest.ToString();
            head.setData(data.head, data.dest);
            head.refresh();
            changetype.text = data.info;
            type.text = ArenaMemberChangeRecord.getTypeName(data.type);
        }

        private void setTime()
        {
            string t = TimeKit.format(data.time, "yyyy-MM-dd HH:mm:ss");
            string yymmdd = t.Substring(0, 10);
            string hhmmss = t.Substring(11, 8);

            this.time.text = yymmdd + "\n" + hhmmss;
        }

        public void setData(ArenaMemberChangeRecord record)
        {
            data = record;
        }
    }
}
