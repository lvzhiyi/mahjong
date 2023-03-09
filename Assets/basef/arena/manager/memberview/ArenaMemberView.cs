using basef.player;
using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine;
using UnityEngine.UI;

namespace basef.arena
{
    /// <summary>
    /// 会员管理
    /// </summary>
    public class ArenaMemberView : UnrealLuaSpaceObject
    {
        private ScrollContainer container;

        /// <summary>
        /// 类型
        /// </summary>
        private Dropdown typesDropdown;

        private string[] showNames = new[] {"全部成员", "全部合伙人", "我的合伙人","我的下级"};

        /// <summary>
        /// 显示列表
        /// </summary>
        ArrayList<ArenaMember> memberlist;

        /// <summary>
        /// 总的成员列表
        /// </summary>
        private ArrayList<ArenaMember> allList;

        /// <summary>
        /// 成员详细
        /// </summary>
        public ArenaMemberDetailView memberDetailView;

        /// <summary>
        /// 输入框
        /// </summary>
        public UnrealInputTextField inputTextField;

        protected override void xinit()
        {
            container = transform.Find("container").GetComponent<ScrollContainer>();
            container.init();
            typesDropdown = transform.Find("typedrop").GetComponent<Dropdown>();
            memberDetailView.init();
            inputTextField = transform.Find("id").Find("text").GetComponent<UnrealInputTextField>();
            inputTextField.init();
        }


        public override void setVisible(bool b)
        {
            base.setVisible(b);
            if (!b)
            {
                if (allList != null)
                    allList.clear();
                if (memberlist != null)
                    memberlist.clear();
            }
        }

        private void getShowName()
        {
            ArenaMember member = Arena.arena.getMember();
            showNames = new[] { "全部成员", "全部合伙人", "我的合伙人", "我的下级" };
            if (member.isMaster() && !Arena.arena.isMultilevel())
                showNames = new[] {"全部成员"};
            else if(!member.isMaster()&&Arena.arena.isLastAgent())
                showNames = new[] { "全部成员" };
        }


        public void resetDropDownStart()
        {
            getShowName();
            typesDropdown.options.Clear();
            Dropdown.OptionData temoData;
            for (int i = 0; i < showNames.Length; i++)
            {
                //给每一个option选项赋值
                temoData = new Dropdown.OptionData();
                temoData.text = showNames[i];

                typesDropdown.options.Add(temoData);
            }

            typesDropdown.captionText.text = showNames[0]; //初始选项的显示
        }

        public int switchBillType()
        {
            switch (typesDropdown.value)
            {
                case 0:
                    return GetArenaMembersListCommand.SELECT_TREE_NODES;
                case 1:
                    return GetArenaMembersListCommand.SELECT_TREE_AGENTS;
                case 2:
                    return GetArenaMembersListCommand.SELECT_AGENTS;
                case 3:
                    return GetArenaMembersListCommand.SELECT_MEMBERS;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// 预制件上绑定的
        /// </summary>
        public void onSelectType()
        {
            int type = switchBillType();
            memberlist = getMembersList(type);
            this.container.resize(memberlist.count);
        }

        public ArrayList<ArenaMember> getMembersList(int type)
        {
            ArrayList<ArenaMember> list = new ArrayList<ArenaMember>();              
            ArenaMember member = null;
            for (int i = 0; i < allList.count; i++)
            {
                member = allList.get(i);
                if (type == GetArenaMembersListCommand.SELECT_TREE_NODES)
                {
                    list.add(member);
                }
                else if(type== GetArenaMembersListCommand.SELECT_MEMBERS)//我的下级
                {
                    if (member.master == Player.player.userid)
                        list.add(member);
                }
                else if (!member.isAgent()||member.userid==Player.player.userid) continue;
                else if (type == GetArenaMembersListCommand.SELECT_TREE_AGENTS)
                {
                    list.add(member);
                }
                else if (member.master == Player.player.userid)
                    list.add(member);
            }

            return list;
        }

        /// <summary>
        /// 显示视图时候设置
        /// </summary>
        /// <param name="memberlist"></param>
        public void setData(ArrayList<ArenaMember> members)
        {
            ArenaMember cur_member = Arena.arena.getMember();

            InsertArrayList<ArenaMember> insert = new InsertArrayList<ArenaMember>(onlinesCompare);
            for (int i = 0; i < members.count; i++)
                insert.add(members.get(i));

            allList = new ArrayList<ArenaMember>();
            allList.add(cur_member);
            this.memberlist = new ArrayList<ArenaMember>();
            this.memberlist.add(cur_member);
            
            for (int i = 0; i < insert.count; i++)
            {
                cur_member = insert.get(i);
                allList.add(cur_member);
                this.memberlist.add(cur_member);
            }

            insert.clear();
        }


        public int onlinesCompare(ArenaMember a, ArenaMember b)
        {
            if (a.hasStatus(ArenaMember.STATUS_FKC_GAMING) && b.hasStatus(ArenaMember.STATUS_FKC_GAMING))
            {
                if (a.getStatus() > b.getStatus())
                    return Comparator.COMP_GT;
                else
                    return Comparator.COMP_LT;
            }
            else if (a.hasStatus(ArenaMember.STATUS_ONLINE) || b.hasStatus(ArenaMember.STATUS_ONLINE))
            {
                if (a.hasStatus(ArenaMember.STATUS_ONLINE))
                    return Comparator.COMP_GT;
                else
                    return Comparator.COMP_LT;
            }
            else
            {
                if (a.getStatus() > b.getStatus())
                    return Comparator.COMP_GT;
                else
                    return Comparator.COMP_LT;
            }
        }

        public void setSingleMember(long userid)
        {
            ArenaMember member = null;
            for (int i = 0; i < allList.count; i++)
            {
                if (allList.get(i).userid == userid)
                {
                    member = allList.get(i);
                    break;
                }
            }

            if (member == null)
            {
                Alert.autoShow("该用户不存在");
                inputTextField.text = "";
                return;
            }

            this.memberlist.clear();
            this.memberlist.add(member);
            this.container.resize(memberlist.count);
        }

        public void removeMember(ArenaMember member)
        {
            memberlist.remove(member);
            allList.remove(member);
            container.removeresize(memberlist.count);
        }

        protected override void xrefresh()
        {
            inputTextField.text = "";
            resetDropDownStart();
            this.container.updateData(updateDatas);
            this.container.resize(memberlist.count);
        }

        public void updateDatas(ScrollBar bar, int index)
        {
            ArenaMemberBar memberBar = (ArenaMemberBar) bar;
            memberBar.index_space = index;
            ArenaMember member = memberlist.get(index);
            memberBar.setData(member);
            memberBar.refresh();
        }
    }
}
