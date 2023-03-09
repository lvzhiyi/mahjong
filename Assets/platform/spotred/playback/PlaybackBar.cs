using cambrian.common;
using cambrian.game;
using cambrian.unreal.scroll;
using UnityEngine;

namespace platform.spotred.playback
{
    public class PlaybackBar: ScrollBar
    {
        [HideInInspector]
        public int index;

        public Record record;

        UnrealTextField idtext;

        UnrealTextField date;

        UnrealTextField roomid;

        private UnrealTextField rulename;
        /// <summary>
        /// 回放ID
        /// </summary>
        private UnrealTextField playBackId;

        /// <summary> 玩家信息 </summary>
        UnrealTableContainer container;

        protected override void xinit()
        {
            this.idtext = this.transform.Find("id").GetComponent<UnrealTextField>();
            this.idtext.init();
            this.date = this.transform.Find("date").GetComponent<UnrealTextField>();
            this.date.init();
            this.roomid = this.transform.Find("roomid").GetComponent<UnrealTextField>();
            this.roomid.init();

            this.rulename = this.transform.Find("rulename").GetComponent<UnrealTextField>();
            this.rulename.init();
            this.playBackId = this.transform.Find("playbackid").GetComponent<UnrealTextField>();
            this.playBackId.init();

            container = this.transform.Find("barcontent").GetComponent<UnrealTableContainer>();
            container.init();

        }

        public void setPlayback(int index, Record record)
        {
            this.index = index;
            this.record = record;
        }

        protected override void xrefresh()
        {
            idtext.text = (index+1).ToString();
            rulename.text = record.rule.rule.name;
            playBackId.text ="回放ID: "+ record.id;

            date.text = "时间：" + TimeKit.format(record.time, "yyyy/MM/dd HH:mm:ss");
            roomid.text = "房间号：" + record.roomid;

            container.resize(record.players.Length);
            for (int i = 0; i < record.players.Length; i++)
            {
                PlaybackUserBar bar = (PlaybackUserBar)container.bars[i];
                bar.index_space = i;
                bar.setData(record.totalscores[i], record.players[i]);
                bar.refresh();
            }
            container.refresh();
            container.resizeDelta();
        }
    }
}
