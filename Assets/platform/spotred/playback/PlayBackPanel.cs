using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine;

namespace platform.spotred.playback
{

    public class PlayBackPanel : UnrealViewPanel
    {
        ScrollContainer playback;

        /// <summary>
        /// 回放列表
        /// </summary>
        [HideInInspector] public InsertArrayList<Record> list;

        /// <summary>
        /// 输入框
        /// </summary>
        [HideInInspector] public UnrealInputTextField input;

        protected override void xinit()
        {
            base.xinit();
            this.playback = this.content_conainer.transform.Find("container").GetComponent<ScrollContainer>();
            this.playback.init();
            this.input = this.content_conainer.transform.Find("search").Find("id").Find("text").GetComponent<UnrealInputTextField>();
            this.input.init();
        }

        public void dataInit(Record[] records)
        {
            if (list == null)
                list = new InsertArrayList<Record>(timeCompare);
            else
                list.clear();
            if (records != null)
            {
                for (int i = 0; i < records.Length; i++)
                {
                    list.add(records[i]);
                }
            }

            //this.refresh();
            //this.setCVisible(true);
        }

        public int timeCompare(Record a, Record b)
        {
            return a.time > b.time ? Comparator.COMP_GT : Comparator.COMP_LT;
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            this.input.text = "";
            this.showPlayback();
        }

        public override void setVisible(bool b)
        {
            base.setVisible(b);
        }

        public override void setCVisible(bool b)
        {
            base.setCVisible(b);
        }


        /// <summary>
        /// 显示历史牌局
        /// </summary>
        public void showPlayback()
        {
            this.playback.updateData(updatePlayBackData);
            this.playback.resize(this.list.count);
            this.playback.setVisible(true);
        }

        public void updatePlayBackData(ScrollBar bar, int i)
        {
            ((PlaybackBar)bar).setPlayback(i, this.list.get(i));
            ((PlaybackBar)bar).index = i;
            bar.refresh();
        }
    }
}
