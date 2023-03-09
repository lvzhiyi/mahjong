using basef.share;
using cambrian.common;
using UnityEngine;
using UnityEngine.UI;

namespace platform.spotred.over
{
    public class AllOverPanel: UnrealLuaPanel
    {
        UnrealTableContainer container;
        /// <summary>
        /// 牌局开始时间
        /// </summary>
        public Text time;
        /// <summary>
        /// 规则
        /// </summary>
        public Text rule_info;
        /// <summary>
        /// 房间号
        /// </summary>
        public Text room_id;

        [HideInInspector] public Room room;


        protected override void xinit()
        {
            this.container = this.transform.Find("Canvas").Find("container").GetComponent<UnrealTableContainer>();
            this.container.init();
            this.resizeDelta(new Margin());

            this.room_id = this.transform.Find("Canvas").Find("room_id").GetComponent<Text>();
            this.rule_info = this.transform.Find("Canvas").Find("rule_info").GetComponent<Text>();
            this.time = this.transform.Find("Canvas").Find("time").GetComponent<Text>();
        }

        public void show(Room room)
        {
            this.room = room;
            this.rule_info.text = room.roomRule.rule.name + ":<color=#38753f>" + room.scores.Length +
                                  "</color>/" +
                                  room.roomRule.rule.matchCount;
            this.room_id.text = "房间号：" + room.getRoomIndex();
            this.time.text = TimeKit.format(room.getCreateRoomTime(), "yyyy/MM/dd HH:mm:ss");

            int num = room.players.Length;
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
                AllOverBar bar = (AllOverBar) this.container.bars[i];

                int type = AllOverBar.NO;

                for (int j = 0; j < pc.count; j++)
                {
                    if (pc.get(j).getIndex() == i)
                    {

                        type = pc.get(j).getType();

                        break;
                    }
                }

                bar.setInfo(room.players[i].player.userid == room.masterid, room.players[i].player, room.getRoomResult().getIndexCount(i), type,
                    list.get(i),room);
                bar.refresh();
            }

            this.container.resizeDelta();
            this.container.relayout();
        }

        InsertArrayList<PointCompare> getPointCompare(Room room)
        {
            ArrayList<long> points = new ArrayList<long>();

            ArrayList<TotalScore> counts= room.getRoomResult().getTotalScores();

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

             long p=0;
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
            for (int i = list.count-1; i >= 0; i--)
            {
                PointCompare pc = list.get(i);
                if (i == list.count-1)
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

        int pointSort(PointCompare a,PointCompare b)
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

    class PointCompare
    {
        /// <summary>
        /// 分数
        /// </summary>
        long point;
        /// <summary>
        /// 索引
        /// </summary>
        int index;
        /// <summary>
        /// 类型(大赢家，点炮王，无)
        /// </summary>
        private int type;

        public PointCompare(int index,long point)
        {
            this.point = point;
            this.index = index;
            this.type = AllOverBar.NO;
        }

        public void setType(int type)
        {
            this.type = type;
        }

        public int getType()
        {
            return this.type;
        }

        public long getPoint()
        {
            return point;
        }

        public int getIndex()
        {
            return index;
        }
    }
}
