using basef.player;
using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine;
using UnityEngine.UI;

namespace basef.arena
{
    /// <summary>
    /// 下级推广员
    /// </summary>
    public class ArenaExtensionNextView:UnrealLuaSpaceObject
    {
        private ScrollContainer container;

        private ArenaNextExtension[] nextExtensions;

        ArrayList<ArenaNextExtension> list=new ArrayList<ArenaNextExtension>();
        /// <summary>
        /// 是否是我自己的推广员
        /// </summary>
        [HideInInspector] public bool ismyExtension;

        /// <summary>
        /// 总积分
        /// </summary>
        [HideInInspector] public long total;
        /// <summary>
        /// 个人积分
        /// </summary>
        [HideInInspector] public long score;

        /// <summary>
        /// 勾选我的推广员
        /// </summary>
        private Image ok;
        /// <summary>
        /// 输入玩家id
        /// </summary>
        public UnrealInputTextField text;

        // 新增
        /// <summary>
        /// 总积分
        /// </summary>
        private UnrealTextField totalscore;
        /// <summary>
        /// 总积分标题
        /// </summary>
        private Text totalTitle;
        /// <summary>
        /// 个人积分
        /// </summary>
        private UnrealTextField selfscore;
        /// <summary>
        /// 积分编辑
        /// </summary>
        private UnrealScaleButton editscore;
        /// <summary>
        /// 合伙人操作视图
        /// </summary>
        private Transform operateView;
        /// <summary>
        /// 冻结推广员操作按钮标题
        /// </summary>
        private Text lockAgent;
        /// <summary>
        /// 操作合伙人对象
        /// </summary>
        [HideInInspector]public ArenaExtensionNextBar operateAgent;

        protected override void xinit()
        {
            container = transform.Find("container").GetComponent<ScrollContainer>();
            container.init();
            ok = transform.Find("minus").Find("img").GetComponent<Image>();
            text = transform.Find("id").Find("text").GetComponent<UnrealInputTextField>();
            text.init();
            text.endChange = endchange;

            // 新增
            totalscore = transform.Find("totalscore").GetComponent<UnrealTextField>();
            totalTitle = totalscore.transform.Find("name").GetComponent<Text>();
            selfscore = transform.Find("selfscore").GetComponent<UnrealTextField>();
            editscore = transform.Find("edit").GetComponent<UnrealScaleButton>();
            operateView = transform.Find("operate");
            lockAgent = operateView.Find("group").Find("lockagent").Find("text").GetComponent<Text>();
        }

        public void endchange(string value)
        {
            if (value == null || value.Length == 0)
                return;
            long userid = StringKit.parseLong(value);
            searchExtension(userid);
        }

        public void setData(object[] objs)
        {
            ArenaNextExtension[] nextExtensions = (ArenaNextExtension[])objs[0];
            total = (long)objs[1];
            score = (long)objs[2];
            list.clear();
            list.add(nextExtensions);
            this.nextExtensions = nextExtensions;
            ismyExtension = false;
        }

        protected override void xrefresh()
        {
            text.text = "";
            ok.gameObject.SetActive(false);
            // 新增====================
            totalscore.text = MathKit.getRound2Long(total+score).ToString();
            selfscore.text = MathKit.getRound2Long(score).ToString();

            Arena arena = Arena.arena;
            ArenaMember member = arena.getMember();
            if (arena.isMaster(member.userid) || (arena.isMaster(member.master) && member.hasStatus(ArenaMember.STATUS_ADMIN)))
                totalTitle.text = "休闲场总积分";
            else
                totalTitle.text = "合伙人总积分";
            editscore.gameObject.SetActive(arena.getMaster() == member.userid);
            // end
            container.updateData(updateData);
            container.resize(list.count);
            operateView.gameObject.SetActive(false);
        }

        public void callback(long score,long total)
        {
            this.score = score;
            this.total = total;
            selfscore.text = MathKit.getRound2Long(score).ToString();
            totalscore.text = MathKit.getRound2Long(total + score).ToString();
        }

        public void refreshScore(long score)
        {
            this.score = score;
            selfscore.text = MathKit.getRound2Long(score).ToString();
            //totalscore.text = MathKit.getRound2Long(total + score).ToString();
        }

        public void showselfExtension(bool b)
        {
            ismyExtension = b;
            ok.gameObject.SetActive(b);
            list.clear();
            if (b)
            {
                ArrayList<ArenaNextExtension> ll = new ArrayList<ArenaNextExtension>();
                ArenaNextExtension next = null;
                for (int i = 0; i < nextExtensions.Length; i++)
                {
                    next = nextExtensions[i];
                    if (next.master == Player.player.userid)
                        ll.add(next);
                }
                list.add(ll.toArray());
            }
            else
            {
                list.add(nextExtensions);
            }
            container.resize(list.count);
        }

        public void searchExtension(long userid)
        {
            list.clear();
            ArenaNextExtension next = null;
            for (int i = 0; i < nextExtensions.Length; i++)
            {
                next = nextExtensions[i];
                if (next.userid == userid)
                {
                    list.add(next);
                    break;
                }
            }
            container.resize(list.count);
        }

        public void updateData(ScrollBar bar,int index)
        {
            ArenaExtensionNextBar nextbar = (ArenaExtensionNextBar)bar;
            nextbar.setData(list.get(index));
            nextbar.index_space = index;
            nextbar.refresh();
        }

        public void showOperateView(ArenaExtensionNextBar bar)
        {
            this.operateAgent = bar;
            if (bar.extension.hasStatus(262144)) //冻结推广员
                lockAgent.text = "解冻合伙人";
            else
                lockAgent.text = "冻结合伙人";
            operateView.gameObject.SetActive(true);
        }
    }
}
