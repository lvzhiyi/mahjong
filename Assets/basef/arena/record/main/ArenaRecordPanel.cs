using cambrian.common;
using cambrian.game;
using cambrian.unreal.scroll;
using UnityEngine;

namespace basef.arena
{
    public class ArenaRecord
    {
        public static string rulename;

        public static int msgLen = 5;

        public static bool isMoreMsg = false;
    }

    public class ArenaRecordPanel : UnrealLuaPanel
    {
        /// <summary>
        /// 选择类型：0个人战绩，1赛场战绩
        /// </summary>
        public const int TYPE_SELF = 0, TYPE_ARENA = 1;
        /// <summary>
        /// 打开类型：0大厅界面打开，1成员管理打开
        /// </summary>
        public const int OPENTYPE_LOBBY = 0, OPENTYPE_MEMBER = 1;

        /// <summary>
        /// 显示时间天数：默认7天
        /// </summary>
        public int time_size = 7;

        public long userid;

        public long arenaid;

        /// <summary> 数据列表 </summary>
        ArrayList<ArenaRecordContentData> list = new ArrayList<ArenaRecordContentData>();


        ScrollContainer ruleTypeContainer;

        ScrollContainer recordTypeContainer;  //赛场战绩 我的战绩列表

        ScrollContainer container;

        /// <summary>
        /// 选中时间
        /// </summary>
        public long selectTime;
        /// <summary>
        /// 时间数组
        /// </summary>
        long[] times;


        /// <summary> 搜索玩家id </summary>
        UnrealInputTextField input;

        /// <summary> 搜索玩家id按钮 </summary>
        UnrealScaleButton inputsearch;

        /// <summary>
        /// 赛场战绩按钮
        /// </summary>
        UnrealScaleButton arenarecord;
        /// <summary>
        /// 个人战绩按钮
        /// </summary>
        UnrealScaleButton selfrecord;

        Transform normal;
        Transform selected;
        Transform normal1;
        Transform selected1;

        /// <summary>
        /// 当前选中类型:0我的战绩，1赛场战绩
        /// </summary>
        public int selectType;

        /// <summary>
        /// 打开类型
        /// </summary>
        public int openType;
        /// <summary>
        /// 搜索类型
        /// </summary>
        int searchType;

        protected override void xinit()
        {
            base.xinit();
            ruleTypeContainer = this.content.Find("content").Find("ruleType").GetComponent<ScrollContainer>();
            ruleTypeContainer.init();

            recordTypeContainer = this.content.Find("content").Find("view").Find("record").Find("type").GetComponent<ScrollContainer>();
            recordTypeContainer.init();

            container = this.content.Find("content").Find("view").Find("record").Find("container").GetComponent<ScrollContainer>();
            container.init();

            input = this.content.Find("content").Find("input").Find("text").GetComponent<UnrealInputTextField>();
            input.init();
            inputsearch = this.content.Find("content").Find("inputsearch").GetComponent<UnrealScaleButton>();
            inputsearch.init();

            arenarecord = this.content.Find("content").Find("view").Find("record").Find("arenarecord").GetComponent<UnrealScaleButton>();
            selfrecord = this.content.Find("content").Find("view").Find("record").Find("selfrecord").GetComponent<UnrealScaleButton>();

            normal = arenarecord.transform.Find("normal1").GetComponent<Transform>();
            selected = arenarecord.transform.Find("selected1").GetComponent<Transform>();

            normal1 = selfrecord.transform.Find("normal1").GetComponent<Transform>();
            selected1 = selfrecord.transform.Find("selected1").GetComponent<Transform>();
        }

        protected override void xrefresh()
        {
            updateSelect();

            container.updateData(refreshContent);
            container.resize(list.count);
            container.resizeDelta();

            ruleTypeContainer.updateData(ruleTypeCallBack);
            ruleTypeContainer.resize(time_size);
            ruleTypeContainer.resizeDelta();

            input.text = "";

            selfrecord.gameObject.SetActive(false);
            arenarecord.gameObject.SetActive(false);
            input.setVisible(false);
            inputsearch.setVisible(false);

            if (openType == OPENTYPE_LOBBY)
            {
                selfrecord.gameObject.SetActive(true);
                arenarecord.gameObject.SetActive(Arena.arena.getMember().isMaster() || Arena.arena.getMember().isAgent());
                if (selectType == TYPE_ARENA)
                {
                    input.setVisible(Arena.arena.getMember().isMaster() || Arena.arena.getMember().isAgent());
                    inputsearch.setVisible(Arena.arena.getMember().isMaster() || Arena.arena.getMember().isAgent());
                }
            }
        }

        public void setSearchType(int searchType)
        {
            this.searchType = searchType;
        }


        public void refresh1()
        {
            updateSelect();

            container.updateData(refreshContent);
            container.resize(list.count);
            container.resizeDelta();

            ruleTypeContainer.refreshShow();

            input.text = "";

            selfrecord.gameObject.SetActive(false);
            arenarecord.gameObject.SetActive(false);
            input.setVisible(false);
            inputsearch.setVisible(false);

            if (openType == OPENTYPE_LOBBY)
            {
                selfrecord.gameObject.SetActive(true);
                arenarecord.gameObject.SetActive(Arena.arena.getMember().isMaster() || Arena.arena.getMember().isAgent());
                if (selectType == TYPE_ARENA)
                {
                    input.setVisible(Arena.arena.getMember().isMaster() || Arena.arena.getMember().isAgent());
                    inputsearch.setVisible(Arena.arena.getMember().isMaster() || Arena.arena.getMember().isAgent());
                }
            }
        }


        /// <summary> 刷新时间 </summary>
        public void ruleTypeCallBack(ScrollBar bar, int index)
        {
            ArenaRecordRuleTypeBar value = (ArenaRecordRuleTypeBar)bar;
            value.setData(times[index], selectTime);
            value.index_space = index;
            value.refresh();
        }

        /// <summary> 刷新内容 </summary>
        public void refreshContent(ScrollBar bar, int index)
        {
            ArenaRecordContentBar value = (ArenaRecordContentBar)bar;
            value.setData(list.get(index));
            value.index_space = index;
            value.refresh();

            commandContent(index);
        }

        /// <summary> 更新内容 </summary>
        private void commandContent(int index)
        {
            if (ArenaRecord.isMoreMsg && index == list.count - 1)
            {
                UserCommand command = null;
                if (searchType==1|| selectType == 0)
                    command = new GetArenaRecordSelfDataListCommand(arenaid, userid, selectTime, getRuleType(), list.getLast().uuid);
                else
                    command = new GetArenaRecordDataListCommand(arenaid, selectTime, getRuleType(), list.getLast().uuid);
                CommandManager.addCommand(command, commandContentBack);
            }
        }

        private void commandContentBack(object obj)
        {
            if (obj == null) return;
            ArrayList<ArenaRecordContentData> bean = (ArrayList<ArenaRecordContentData>)obj;
            list.add(bean.toArray());
            container.incrResize(list.count);
        }

        /// <summary>
        /// 设置数据 回放锁定规则 回放数据
        /// </summary>
        public void setData(ArrayList<ArenaRecordContentData> list)
        {
            if (list != null)
            {
                ArenaRecord.isMoreMsg = (list.count == ArenaRecord.msgLen);
                this.list = list;
            }
        }

        /// <summary> 设置类型 时间类型 </summary>
        public void setTime(long time)
        {
            selectTime = time;
            times = new long[time_size];
            for(int i=0;i<time_size;i++)
            {
                times[i] = time-TimeKit.DAY_MILLS*i;
            }
        }

        public void updateTime(long time)
        {
            selectTime = time;
            ruleTypeContainer.updateData(ruleTypeCallBack);
            ruleTypeContainer.resize(time_size);
            ruleTypeContainer.resizeDelta();
        }

        /// <summary> 获取规则类型 </summary>
        public int getRuleType()
        {
            //if (ruleType.count == 0) return 0;
            //return ruleType.get(selectRuleType).uuid;

            // 弃用 只返回0
            return 0;
        }

        /// <summary> 获取输入的玩家id </summary>
        public string getUserId()
        {
            return input.text;
        }

        /// <summary>
        /// 设置选择类型
        /// </summary>
        /// <param name="selectType"></param>
        public void setSelectType(int selectType)
        {
            this.selectType = selectType;
            searchType = 0;
        }

        /// <summary>
        /// 刷新选择
        /// </summary>
        public void updateSelect()
        {
            normal.gameObject.SetActive(false);
            selected.gameObject.SetActive(false);
            normal1.gameObject.SetActive(false);
            selected1.gameObject.SetActive(false);

            if (selectType == TYPE_SELF)
            {
                selfrecord.setState(UnrealButton.DISABLE);
                arenarecord.setState(UnrealButton.NORMAL);
                normal1.gameObject.SetActive(true);
                selected.gameObject.SetActive(true);
            }
            else
            {
                selfrecord.setState(UnrealButton.NORMAL);
                arenarecord.setState(UnrealButton.DISABLE);
                normal.gameObject.SetActive(true);
                selected1.gameObject.SetActive(true);
            }
        }
    }
}
