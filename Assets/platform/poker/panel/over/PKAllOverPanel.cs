using basef.share;
using cambrian.common;
using UnityEngine;
using UnityEngine.UI;

namespace platform.poker
{
    public class PKAllOverPanel : UnrealLuaPanel
    {
        private UnrealTableContainer container;

        /// <summary> 牌局开始时间 </summary>
        private Text time;

        /// <summary> 规则 </summary>
        private Text rule_info;

        /// <summary> 房间号 </summary>
        private Text room_id;
        
        [HideInInspector] public Room room;

        protected override void xinit()
        {
            base.xinit();
            container = transform.Find("Canvas").Find("container").GetComponent<UnrealTableContainer>();
            container.init();

            time = transform.Find("Canvas").Find("time").GetComponent<Text>();
            rule_info = transform.Find("Canvas").Find("rule_info").GetComponent<Text>();
            room_id = transform.Find("Canvas").Find("room_id").GetComponent<Text>();
            
            resizeDelta(new Margin());
        }

        public void show(Room room)
        {
            this.room = room;
            rule_info.text = string.Format("{0}:<color=#38753f>{1}</color>/{2}", room.roomRule.rule.name, room.scores.Length, room.roomRule.rule.matchCount);
            room_id.text = string.Format("房间号:{0}", room.getRoomIndex());
            time.text = TimeKit.format(room.getCreateRoomTime(), "yyyy/MM/dd HH:mm:ss");

            int num = room.players.Length;
            container.resize(num);
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
            bool[] winLoser = new bool[num];
            long[] scoress = new long[num];
            for (int i = 0; i < num; i++)
            {
                scoress[i] = room.getRoomResult().getIndexCount(i).score;
            }
            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < num - i - 1; j++)
                {
                    if (scoress[j] < scoress[j + 1])
                    {
                        scoress[j + 1] = scoress[j + 1] ^ scoress[j];
                        scoress[j] = scoress[j + 1] ^ scoress[j];
                        scoress[j + 1] = scoress[j + 1] ^ scoress[j];
                    }
                }
            }
            for (int i = 0; i < num; i++)
            {
                winLoser[i] = (scoress[0] == room.getRoomResult().getIndexCount(i).score);
            }
            for (int i = 0; i < num; i++)
            {
                PKAllOverBar bar = (PKAllOverBar)container.bars[i];
                bar.setInfo(
                    room.players[i].player.userid == room.masterid,
                    room.players[i].player,
                    room.getRoomResult().getIndexCount(i),
                    list.get(i),
                    winLoser[i]
                    , room);
                bar.index_space = i;
                bar.refresh();
            }

            container.resizeDelta();
            container.relayout();
        }

        private bool b = false;

        /// <summary> 截屏 </summary>
        private void OnGUI()
        {
            if (b)
            {
                b = false;
                CaptureScreenshot.captures(onCallBack);
            }
        }

        private int type = 0;

        /// <summary> 开始截屏 </summary>
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
