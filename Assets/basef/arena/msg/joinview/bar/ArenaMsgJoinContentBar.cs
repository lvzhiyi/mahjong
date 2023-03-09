using basef.player;
using cambrian.unreal.scroll;

namespace basef.arena
{
    /// <summary> 加入申请 内容 bar </summary>
    public class ArenaMsgJoinContentBar : ScrollBar
    {
        ArenaMsgJoinContentData data;

        /// <summary> 用户名 </summary>
        private UnrealTextField username;

        /// <summary> 用户ID </summary>
        private UnrealTextField usersid;

        /// <summary> 类型 </summary>
        private UnrealTextField type;

        /// <summary> 操作人名字 </summary>
        private UnrealTextField operatename;

        /// <summary> 头像 </summary>
        private PlayerHeadView head;

        protected override void xinit()
        {
            head = transform.Find("head").GetComponent<PlayerHeadView>();
            head.init();

            operatename = transform.Find("operatename").GetComponent<UnrealTextField>();
            operatename.init();

            username = transform.Find("name").GetComponent<UnrealTextField>();
            username.init();

            usersid = transform.Find("usersid").GetComponent<UnrealTextField>();
            usersid.init();

            type = transform.Find("type").GetComponent<UnrealTextField>();
            type.init();
        }

        protected override void xrefresh()
        {
            if (data != null)
            {
                username.text = data.username;
                usersid.text = data.usersid.ToString();
                operatename.text = data.operatename;

                head.setData(data.headurl,data.usersid);
                head.refresh();

                setType();
            }
        }

        /// <summary> 设置类型 </summary>
        private void setType()
        {
            switch (data.type)
            {
                case ArenaMsgJoin.Join_Invite:
                    type.text = "邀请加入";
                    break;
                default:
                    type.text = "类型错误";
                    break;
            }
        }

        public void setData(object obj = null)
        {
            data = (ArenaMsgJoinContentData)obj;
        }

        public ArenaMsgJoinContentData getData()
        {
            return data;
        }
    }
}
