using basef.player;
using basef.rule;
using cambrian.common;
using cambrian.game;
using cambrian.unreal.scroll;
using cambrian.uui;
using UnityEngine;
using UnityEngine.UI;

namespace basef.arena
{
    public class ArenaDeskView:UnrealLuaSpaceObject
    {
        [HideInInspector] public ScrollTableContainer container;
        /// <summary>
        /// 规则标签容器
        /// </summary>
        UnrealTableContainer ruleLabel;

        private Image ruleLabelBg;
        /// <summary>
        /// 桌子列数
        /// </summary>
        public int deskCols = 0;
        /// <summary>
        /// 桌子数量
        /// </summary>
        public int deskCount = 0;
        /// <summary>
        /// 所有的房间
        /// </summary>
        private ArrayList<ArenaRoom> all_list = new ArrayList<ArenaRoom>();

        private SpritesList showMatchRoom;
        /// <summary>
        /// 是否显示满足(只是主人显示)
        /// </summary>
        private Transform isShowMatchRoom;

        public bool isMatchRoom { get; private set; }

        public void setMatchRoom(bool isShow)
        {
            isMatchRoom = isShow;
            ByteBuffer data = new ByteBuffer();
            data.writeBoolean(isMatchRoom);
            FileCachesManager.savePathData("arenashowMatch", true, data);
        }

        protected override void xinit()
        {
            container = transform.Find("container").GetComponent<ScrollTableContainer>();
            container.init();
            ruleLabel = transform.Find("label").Find("mask").Find("container").GetComponent<UnrealTableContainer>();
            ruleLabel.init();
            ruleLabelBg = transform.Find("bg").GetComponent<Image>();

            showMatchRoom = transform.Find("ismatchroom").Find("btn").GetComponent<SpritesList>();
            isShowMatchRoom = transform.Find("ismatchroom");
            ByteBuffer data = FileCachesManager.loadPathData("arenashowMatch", true);
            if (data != null) isMatchRoom = data.readBoolean();
            else isMatchRoom = true;
        }

        /// <summary>
        /// 设置茶馆相关信息
        /// </summary>
        /// <param name="club"></param>
        /// <param name="all_list">所有的房间</param>
        public void setClub(ArrayList<ArenaRoom> all_list)
        {
            this.all_list.clear();

            bool b = Player.player.userid == Arena.arena.getMaster();
            if (!b)
                isMatchRoom = true;

            if (curSelectRule == 0)
            {
                if (!isMatchRoom)
                {
                    for (int i = 0; i < all_list.count; i++)
                    {
                        if (all_list.get(i).playercount != all_list.get(i).rule.rule.playerCount)
                        {
                            this.all_list.add(all_list.get(i));
                        }
                    }
                }
                else this.all_list = all_list;
            }
            else
            {
                for (int i = 0; i < all_list.count; i++)
                {
                    if (all_list.get(i).rule.rule.sid == curSelectRule)
                    {
                        if (!isMatchRoom)
                        {
                            if (all_list.get(i).playercount != all_list.get(i).rule.rule.playerCount)
                                this.all_list.add(all_list.get(i));
                        }
                        else
                        {
                            this.all_list.add(all_list.get(i));
                        }
                    }
                }
            }
        }

        protected override void xrefresh()
        {
            container.cols = deskCols;
            container.updateData(updateDatas);
            container.resize(deskCount);
            lastLockedRulelen = Arena.arena.fkcSettings.getAssignRules(curSelectRule).count;
            showMatchRoom.ShowIndex(isMatchRoom ? 1 : 0, false);
            rerefresh();

            bool b = Player.player.userid == Arena.arena.getMaster();
            isShowMatchRoom.gameObject.SetActive(b);
            if (!b)
            {
                isMatchRoom = false;
            }
        }

        /// <summary>
        /// 上一次锁定的规则（目的是为了避免群主重设规则，导致房间的位置没有在相应的位置上）
        /// </summary>
        private int lastLockedRulelen;
        public void rerefresh()
        {
            showLabel();
            new RefreshArenaRoomEvents().execute();
        }

        private void showLabel()
        {
            ArrayList<Rule> list= Arena.arena.fkcSettings.getArenaLockRuleType();
            int count = 0;
            if (list.count > 0)
                count = list.count + 1;
            ruleLabel.cols = count;
            ruleLabel.resize(count);
            for (int i = 0; i < count; i++)
            {
                ArenaDeskLabelBar bar=(ArenaDeskLabelBar) ruleLabel.bars[i];
                if (i == 0)
                {
                    bar.setRule(null);
                }
                else
                {
                    bar.setRule(list.get(i - 1));
                }

                bar.index_space = i;
                bar.refresh();
            }

            if (count > 0)
            {
                if (labelIndex < count)
                {
                    ruleLabelSelect(labelIndex);
                }
                else
                {
                    ruleLabelSelect(0);
                }
            }

            ruleLabel.resizeDelta();

            ruleLabelBg.gameObject.SetActive(count>0);
        }


        public ArrayList<ArenaLockRule> getLockedRule()
        {
            return Arena.arena.fkcSettings.getAssignRules(curSelectRule);
        }

        public void refreshRoom()
        {
            ArrayList<ArenaLockRule> rules = getLockedRule();
            int len = rules.count;
            if (len != lastLockedRulelen)
            {
                refresh();
                return;
            }

            ScorllTableBar[] bars = this.container.getShowBar();
            int roomindex = 0;
            int roomcount = all_list.count;
            for (int i = 0; i < bars.Length; i++)
            {
                ArenaDeskBar bar = (ArenaDeskBar) bars[i];
                if (bar == null)
                    continue;
                if (bar.index_space < len)
                    bar.setClubRoom(null, true, Arena.arena, rules.get(bar.index_space));
                else
                {
                    roomindex = bar.index_space - len;
                    if (roomcount > roomindex)
                    {
                        bar.setClubRoom(all_list.get(roomindex), true, Arena.arena, null);
                    }
                    else
                    {
                        bar.setClubRoom(null, true, Arena.arena, null);
                    }
                }
                bar.refresh();
            }
        }

        protected void refreshRoomBar(ScorllTableBar[] bars)
        {
            for (int i = 0; i < bars.Length; i++)
            {
                ArenaDeskBar bar = (ArenaDeskBar)bars[i];
                if (bar == null)
                    continue;
                bar.setClubRoom(getIndexClubRoom(bar.index_space), true, Arena.arena, null);
                bar.refresh();
            }
        }

        public void updateDatas(ScorllTableBar bar, int index)
        {
            ArenaDeskBar panelBar = (ArenaDeskBar) bar;
            panelBar.index_space = index;
            ArrayList<ArenaLockRule> rules = Arena.arena.fkcSettings.getAssignRules(curSelectRule);
            int len = rules.count;
            if (len >= 1)
            {
                int roomindex = index - len;
                if (index < len)
                {
                    panelBar.setClubRoom(null, true, Arena.arena, rules.get(index));
                }
                else if (all_list.count > roomindex)
                {
                    panelBar.setClubRoom(all_list.get(roomindex), true, Arena.arena, null);
                }
                else
                {
                    panelBar.setClubRoom(null, true, Arena.arena, null);
                }
            }
            else
                panelBar.setClubRoom(getIndexClubRoom(index), true, Arena.arena, null);

            panelBar.refresh();
        }

        public ArenaRoom getIndexClubRoom(int index)
        {
            for (int i = 0; i < all_list.count; i++)
            {
                ArenaRoom room = all_list.get(i);
                if (room.deskIndex == index)
                    return room;
            }
            return null;
        }

        int curSelectRule;

        private int labelIndex = 0;

        public void ruleLabelSelect(int index)
        {
            for (int i=0;i<ruleLabel.size;i++)
            {
                ArenaDeskLabelBar bar=(ArenaDeskLabelBar) ruleLabel.bars[i];
                bar.selected(index==bar.index_space);
                if (index == bar.index_space&&bar.getRule() != null)
                {
                    curSelectRule = bar.getRule().sid;
                }
                bar.refresh();
            }

            if (index == 0)
            {
                curSelectRule = 0;
            }

            labelIndex = index;
        }
    }
}
