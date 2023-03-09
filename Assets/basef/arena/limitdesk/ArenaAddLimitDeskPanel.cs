using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine;

namespace basef.arena
{
    /// <summary>
    /// 同桌限制组面板
    /// </summary>
    public class ArenaAddLimitDeskPanel: UnrealLuaPanel
    {
        [HideInInspector] public UnrealInputTextField searchid;

        ScrollTableContainer leftcontainer;

        ScrollTableContainer rightcontainer;

        private ArrayList<ArenaMember> leftmembers;

        [HideInInspector ] public ArrayList<ArenaMutexMember> rightmembers;

        /// <summary>
        /// 搜索的成员列表
        /// </summary>
        ArrayList<ArenaMember> searchMemberlist;
        /// <summary>
        /// 搜索的人员
        /// </summary>
        ArenaMember member;


        public bool isUpdate;

        public long limitid;

        bool ismore = false;

        protected override void xinit()
        {
            base.xinit();
            leftcontainer = content.transform.Find("content").Find("leftcontainer").GetComponent<ScrollTableContainer>();
            leftcontainer.init();
            rightcontainer = content.transform.Find("content").Find("rightcontainer").GetComponent<ScrollTableContainer>();
            rightcontainer.init();
            searchid = content.transform.Find("content").Find("searchinput").GetComponent<UnrealInputTextField>();
            searchid.init();
            searchid.valueChange = searchidvaluechange;
        }

        public void setIsUpdate(bool b,long limitid)
        {
            isUpdate = b;
            this.limitid = limitid;
        }

        

        public void searchidvaluechange(string str)
        {
            if (str.Length == 0)
            {
                leftcontainer.updateData(updateLeftdata);
                leftcontainer.resize(leftmembers.count);
            }
        }

        public void setLeftData(ArrayList<ArenaMember> leftmembers)
        {
            this.leftmembers = leftmembers;
        }

        /// <summary>
        /// 设置搜索的成员
        /// </summary>
        /// <param name="member"></param>
        public void setSearchMemeber(ArenaMember member)
        {
            this.member = member;
            //if(searchMemberlist==null)
            //    searchMemberlist=new ArrayList<ArenaMember>();
            //else
            //    searchMemberlist.clear();
            //for (int i = 0; i < leftmembers.count; i++)
            //{
            //    if (leftmembers.get(i).userid == member.userid)
            //    {
            //        searchMemberlist.add(leftmembers.get(i));
            //        break;
            //    }
            //}
            leftcontainer.updateData(updatesearchData);
            leftcontainer.resize(1);
        }

        public void updatesearchData(ScorllTableBar bar, int index)
        {
            ArenaAddLimitLeftBar limit = (ArenaAddLimitLeftBar)bar;
            //ArenaMember member = searchMemberlist.get(index);
            limit.setArenaMember(member, isExist(member.userid));
            limit.refresh();
        }

        public void addLeftMemberData(ArenaMember[] members)
        {
            if (members.Length == 0) return;
            leftmembers.add(members);
            this.leftcontainer.incrResize(members.Length);
        }

        public void setRigthData(ArrayList<ArenaMutexMember> rightmembers)
        {
            this.rightmembers = rightmembers;
        }

        public void addRightArenaMember(ArenaMutexMember member)
        {
            this.rightmembers.add(member);
            updateRightContianer();
        }

        public void removeRigthArenaMember(long userid)
        {
            for (int i = 0; i < rightmembers.count; i++)
            {
                if (rightmembers.get(i).userid == userid)
                {
                    rightmembers.removeAt(i);
                    break;
                }
            }
            updateRightContianer();
        }

        public void refrshleftMemberIsSelect(long userid)
        {
            ArenaMember member = null;
            for (int i = 0; i < leftmembers.count; i++)
            {
                member = leftmembers.get(i);
                if (member.userid == userid)
                {
                    member.isSelect = false;
                }
            }

            ScorllTableBar[] limit =leftcontainer.getShowBar();
            for (int i = 0; i < limit.Length; i++)
            {
                limit[i].refresh();
            }
        }

        protected override void xrefresh()
        {
            ismore = leftmembers.count == 50;
            searchid.text = "";
            leftcontainer.updateData(updateLeftdata);
            leftcontainer.resize(leftmembers.count);
            
            updateRightContianer();
        }

        public void updateRightContianer()
        {
            rightcontainer.updateData(updaterightdata);
            rightcontainer.resize(rightmembers.count);
        }

        public bool isExist(long userid)
        {
            for (int i = 0; i < rightmembers.count; i++)
            {
                if (rightmembers.get(i).userid == userid)
                    return true;
            }
            return false;
        }

        public void updateLeftdata(ScorllTableBar bar,int index)
        {
            ArenaMember member = leftmembers.get(index);
            ArenaAddLimitLeftBar limit =(ArenaAddLimitLeftBar)bar;
            limit.setArenaMember(member, isExist(member.userid));
            limit.refresh();


            if (index + 1 == leftmembers.count && ismore)
            {
                getMoreData();
            }
        }

        private void getMoreData()
        {
            var data = new ByteBuffer();
            data.writeLong(Arena.arena.getId());
            data.writeInt(103);// 类型
            data.writeBoolean(true);
            data.writeInt(leftmembers.count);


            ArenaMember member = null;
            var command = new LuaCommand(2521, data);
            CommandManager.addCommand(
                command,
                (obj) =>
                {
                    if (obj == null)
                        return;
                    data = (ByteBuffer)obj;
                    var len = data.readInt();
                    for (int i = 0; i < len; i++)
                    {
                        member = new ArenaMember();
                        member.bytesRead(data);
                        leftmembers.add(member);
                    }
                    ismore = (len == 50);
                    leftcontainer.incrResize(leftmembers.count);
                }
            );
        }

        public void updaterightdata(ScorllTableBar bar, int index)
        {
            ArenaAddLimitRightBar limit = (ArenaAddLimitRightBar)bar;
            limit.setArenaMember(rightmembers.get(index),true);
            limit.refresh();
        }
    }
}
