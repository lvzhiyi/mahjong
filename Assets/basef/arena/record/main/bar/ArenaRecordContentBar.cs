using basef.rule;
using cambrian.common;
using cambrian.unreal.scroll;
using cambrian.uui;
using platform;
using UnityEngine;
using UnityEngine.UI;

namespace basef.arena
{
    /// <summary> 内容信息 bar </summary>
    public class ArenaRecordContentBar : ScrollBar
    {
        public ArenaRecordContentData data;

        /// <summary> 房间ID </summary>
        UnrealTextField roomid;

        /// <summary> 游戏规则 </summary>
       
        
        UnrealTextField rulename;

        /// <summary> 回放ID </summary>
        UnrealTextField playbackid;

        /// <summary> 时间 </summary>
        UnrealTextField time;

        /// <summary> 玩家信息 </summary>
        UnrealTableContainer container;

        /// <summary> 序列 </summary>
        //UnrealTextField index;
        //锁定规则
        ArenaLockRule lockrule;
       
        protected override void xinit()
        {
            roomid = this.transform.Find("top").Find("roomid").GetComponent<UnrealTextField>();
            roomid.init();

            playbackid = this.transform.Find("top").Find("playbackid").GetComponent<UnrealTextField>();
            playbackid.init();

            time = this.transform.Find("top").Find("time").GetComponent<UnrealTextField>();
            time.init();

            rulename = this.transform.Find("top").Find("rule").GetComponent<UnrealTextField>();
            rulename.init();

            container = this.transform.Find("barcontent").GetComponent<UnrealTableContainer>();
            container.init();
        }

        protected override void xrefresh()
        {
            container.resize(data.name.Length);
            for (int i = 0; i < data.name.Length; i++)
            {
                ArenaRecordContentUserBar bar = (ArenaRecordContentUserBar)container.bars[i];
                bar.index_space = i;
                bar.setData(data.socre[i], data.name[i],data.heads[i],data.playerid[i],data.mastername[i],data.mastersid[i]);
                bar.refresh();
            }
           
            container.resizeDelta();
            time.text = TimeKit.format(data.time, "yyyy-MM-dd HH:mm");
            playbackid.text = data.playbackid.ToString();

            Rule r = RuleManager.manager.newSample(data.ruleid);
            rulename.text = r.name;
            roomid.text = data.roomid.ToString();
        }

        public void setData(ArenaRecordContentData data)
        {
            this.data = data;
        }
    }
}
