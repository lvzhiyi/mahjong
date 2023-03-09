using basef.rule;
using cambrian.common;
using DG.Tweening;
using platform.spotred;
using UnityEngine;
using UnityEngine.UI;

namespace platform.mahjong
{
    public class MJIPView:UnrealLuaSpaceObject
    {
        private Text distance;

        private Vector3 initPos;

        protected override void xinit()
        {
            distance = transform.Find("distance").GetComponent<Text>();
            initPos = transform.localPosition;
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            Room room = Room.room;
            if (room != null)
            {
                if (room.roomRule.getNowsPlayNum() > 1)
                    return;
                MatchPlayer[] players= room.players;
                string content= getIPSame(players);

                if (content.Length > 0)
                {
                    content+="\n"+getDistance(players);
                }
                else
                {
                    content = getDistance(players);
                }
                   
                if (content.Length > 0)
                {
                    distance.text = content;
                    setVisible(true);
                    move();
                }
            }
        }

        private void move()
        {
            transform.localPosition = initPos;
            transform.DOLocalMoveY(247.5f, 1).onComplete=callback;
        }

        public void callback()
        {
            transform.DOLocalMoveY(247.5f, 3).onComplete = back;
        }

        public void back()
        {
            transform.DOLocalMoveY(initPos.y, 1).onComplete = back1;
        }

        public void back1()
        {
            setVisible(false);
        }

        private string getDistance(MatchPlayer[] players)
        {
            CharBuffer buffer=new CharBuffer();
            buffer.append("玩家：");

            bool b = false;

            for (int i = 0; i < players.Length; i++)
            {
                if(players[i]==null) continue;
                if (players[i].gps_latitude == 0)
                {
                    buffer.append("<color=red>").append(StringKit.substring(players[i].player.name, 0, 6)).append("</color>");
                    b = true;
                    if (i != players.Length - 1)
                        buffer.append(",");
                }
            }


            string enabelgps ="";

            if (b)
            {
                buffer.append("未开启GPS");
                enabelgps = buffer.getString();
                buffer.clear();
            }

            string dis=getdis(players);

            if (dis == null || dis.Length == 0)
                dis = enabelgps;
            else
            {
                dis += "\n";
                dis += enabelgps;
            }

            return dis;
        }

        public double getDistance(MatchPlayer scr, MatchPlayer dest)
        {
            if (scr.gps_latitude == 0 || dest.gps_latitude == 0) return -1;

            double igpsns = scr.gps_longitude * 0.000001d;
            double igpswe = scr.gps_latitude * 0.000001d;
            double jgpsns = dest.gps_longitude * 0.000001d;
            double jgpswe = dest.gps_latitude * 0.000001d; ;

           return MathKit.GetGPSDistance(igpswe, igpsns, jgpswe, jgpsns);
           
        }

        /// <summary>
        /// 获取GPS限制距离
        /// </summary>
        /// <returns></returns>
        private int getLimitGPSInstence()
        {
            Rule rule = Room.room.roomRule.rule;
            int limitInstance = 1000;
            if (rule.getGpsLimit())
                limitInstance = rule.getGpsLimitDistance();
            return limitInstance;
        }

        private string getdis(MatchPlayer[] players)
        {
            ArrayList<int[]> ipsame = new ArrayList<int[]>();

            ArrayList<int> ips = new ArrayList<int>();
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i] == null||players[i].gps_latitude==0) continue;
                if (isExist(ipsame, i)) continue;
                for (int j = i + 1; j < players.Length; j++)
                {
                    if (players[j] == null) continue;
                    if (getDistance(players[i],players[j])< getLimitGPSInstence())
                    {
                        ips.add(j);
                    }
                }

                if (ips.count > 0)
                {
                    ips.add(i);
                    ipsame.add(ips.toArray());
                    ips.clear();
                }
            }

            CharBuffer buffer = new CharBuffer();

            int[] indexs = null;

            for (int i = 0; i < ipsame.count; i++)
            {
                indexs = ipsame.get(i);

                buffer.append("玩家：");
                for (int j = 0; j < indexs.Length; j++)
                {
                    buffer.append("<color=red>").append(StringKit.substring(players[indexs[j]].player.name, 0, 6)).append("</color>");
                    if (j != indexs.Length - 1)
                    {
                        buffer.append(",");
                    }
                }
                buffer.append(" 距离过近");
                if (ipsame.count > 1 && i != ipsame.count - 1)
                    buffer.append("、");
            }

            return buffer.getString();
        }

        private string getIPSame(MatchPlayer[] players)
        {
            ArrayList<int[]> ipsame = new ArrayList<int[]>();

            ArrayList<int> ips = new ArrayList<int>();
            for (int i = 0; i < players.Length; i++)
            {
                if(players[i]==null|| players[i].ip == null) continue;
                if (isExist(ipsame, i)) continue;
                for (int j = i + 1; j < players.Length; j++)
                {
                    if (players[j] == null || players[j].ip == null) continue;
                    if (players[i].ip.Equals(players[j].ip))
                    {
                        ips.add(j);
                    }
                }

                if (ips.count > 0)
                {
                    ips.add(i);
                    ipsame.add(ips.toArray());
                    ips.clear();
                }
            }

            CharBuffer buffer=new CharBuffer();

            int[] indexs = null;

            for (int i = 0; i < ipsame.count; i++)
            {
                indexs = ipsame.get(i);
                
                buffer.append("玩家：");
                for (int j = 0; j < indexs.Length; j++)
                {
                    buffer.append("<color=red>").append(StringKit.substring(players[indexs[j]].player.name,0,6)).append("</color>");
                    if (j != indexs.Length - 1)
                    {
                        buffer.append(",");
                    }
                }
                buffer.append(" IP相同");
                if (ipsame.count > 1 && i != ipsame.count - 1)
                    buffer.append(",");
            }

            return buffer.getString();
        }


        private bool isExist(ArrayList<int[]> lists,int index)
        {
            int[] indexs = null;
            for (int i = 0; i < lists.count; i++)
            {
                indexs = lists.get(i);
                for (int j = 0; j < indexs.Length; j++)
                {
                    if (indexs[j] == index) return true;
                }
                
            }

            return false;
        }
    }
}
