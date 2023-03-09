using basef.share;
using cambrian.common;
using platform.spotred;
using platform.spotred.over;
using UnityEngine;
using UnityEngine.UI;

namespace platform.mahjong
{
    /// <summary>
    /// 麻将总结算
    /// </summary>
    public class MJAllOverPanel:UnrealLuaPanel
    {
        UnrealTableContainer container;

        public Room room;
        /// <summary>
        /// 房间id
        /// </summary>
        Text roomid;
        /// <summary>
        /// 局数
        /// </summary>
        Text jushu;
        /// <summary>
        /// 时间
        /// </summary>
        Text time;

        [HideInInspector] public static MahjongCount[] counts;

        protected override void xinit()
        {
            base.xinit();
            container = content.Find("container").GetComponent<UnrealTableContainer>();
            container.init();

            roomid = content.Find("room_id").GetComponent<Text>();
            time = content.Find("time").GetComponent<Text>();
            jushu = content.Find("jushu").GetComponent<Text>();
            
            this.resizeDelta(new Margin());
        }

        public void show(Room room)
        {
            this.room = room;
            this.jushu.text = "局"+room.scores.Length + "/" + room.roomRule.rule.matchCount;
            this.roomid.text = "房间号：" + room.getRoomIndex();
            this.time.text = TimeKit.format(room.getCreateRoomTime(), "yyyy/MM/dd HH:mm:ss");

            int num = room.players.Length;
            this.container.cols = num;
            this.container.resize(num);

            InsertArrayList<PointCompare> pc = getPointCompare(room);

            long[][] scores = room.scores;

            ArrayList<long[]> list = new ArrayList<long[]>();
            if (scores.Length == 0)
            {
                for (int i = 0; i < num; i++)
                {
                    list.add(new long[0]);
                }
            }
            else
            {
                for (int i = 0; i < scores[0].Length; i++)
                {
                    long[] s = new long[scores.Length];
                    for (int j = 0; j < scores.Length; j++)
                    {
                        s[j] = scores[j][i];
                    }

                    list.add(s);
                }
            }


            for (int i = 0; i < num; i++)
            {
                MJAllOverBar bar = (MJAllOverBar)this.container.bars[i];

                int type = AllOverBar.NO;

                for (int j = 0; j < pc.count; j++)
                {
                    if (pc.get(j).getIndex() == i)
                    {

                        type = pc.get(j).getType();

                        break;
                    }
                }

                bar.index_space = i;
                bar.setInfo(room.players[i].player.userid==room.masterid, type,list.get(i), room, counts[i]);

                bar.refresh();
            }

            this.container.resizeDelta();
            this.container.relayout();
        }

        InsertArrayList<PointCompare> getPointCompare(Room room)
        {
            ArrayList<long> points = new ArrayList<long>();

            ArrayList<TotalScore> counts = room.getRoomResult().getTotalScores();

            for (int i = 0; i < counts.count; i++)
            {
                points.add(counts.get(i).score);
            }

            InsertArrayList<PointCompare> list = new InsertArrayList<PointCompare>(pointSort);

            for (int i = 0; i < points.count; i++)
            {
                PointCompare compare = new PointCompare(i, points.get(i));
                list.add(compare);
            }

            long p = 0;
            //设置大赢家
            for (int i = 0; i < list.count; i++)
            {
                PointCompare pc = list.get(i);
                if (i == 0)
                {
                    p = pc.getPoint();
                    if (p == 0) break;
                    pc.setType(AllOverBar.WIN);
                }
                else if (pc.getPoint() == p)
                {
                    pc.setType(AllOverBar.WIN);
                }
            }

            //设置点炮王
            for (int i = list.count - 1; i >= 0; i--)
            {
                PointCompare pc = list.get(i);
                if (i == list.count - 1)
                {
                    p = pc.getPoint();
                    if (p == 0) break;
                    pc.setType(AllOverBar.DIAN_PAO);
                }
                else if (pc.getPoint() == p)
                {
                    pc.setType(AllOverBar.DIAN_PAO);
                }
            }
            return list;
        }

        int pointSort(PointCompare a, PointCompare b)
        {
            return a.getPoint() > b.getPoint() ? Comparator.COMP_GT : Comparator.COMP_LT;
        }


        private bool b = false;

        /// <summary>
        /// 截屏
        /// </summary>
        void OnGUI()
        {
            if (b)
            {
                b = false;
                CaptureScreenshot.captures(onCallBack);
            }
        }

        private int type = 0;
        /// <summary>
        /// 开始截屏
        /// </summary>
        public void startCaptures(int type)
        {
            b = true;
            this.type = type;
        }

        public void onCallBack(byte[] obj)
        {
            ShareManager.getInstance().sharePicToApplication(obj, type); ;
        }
    }
}

