using System.Text;
using basef.rule;
using cambrian.common;
using cambrian.game;
using cambrian.uui;

namespace platform.poker
{
    /// <summary> 顶部显示 </summary>
    public class PKTopView : UnrealLuaSpaceObject
    {
        /// <summary> 设置按钮 </summary>
        protected UnrealScaleButton settingBtn;

        /// <summary> 返回按钮 </summary>
        protected UnrealScaleButton returnBtn;

        /// <summary> 房间号 </summary>
        protected UnrealTextField roomid;

        /// <summary> 规则名字 </summary>
        protected UnrealTextField rulename;

        /// <summary> 规则底分 </summary>
        protected UnrealTextField ruledifen;

        /// <summary> 游戏局数 </summary>
        protected UnrealTextField roomnum;

        /// <summary> 语音消息 </summary>
        //protected UnrealScaleButton startbutton;

        /// <summary> 快捷消息 </summary>
        protected UnrealScaleButton imbtn;

        /// <summary> 倍数 </summary>
        protected UnrealTextField multiple;

        /// <summary> 记牌器按钮 </summary>
        protected UnrealScaleButton recorderbtn;

        /// <summary> 时间 </summary>
        protected UnrealTextField nowtime;

        /// <summary> 电量 </summary>
        protected UnrealProgressBar dian;

        /// <summary> 4G </summary>
        protected SpritesList xinhao;

        /// <summary> wifi信号 </summary>
        protected SpritesList wifi;

        /// <summary> 定位 </summary>
        protected UnrealScaleButton dwbtn;

        protected override void xinit()
        {
            if (transform.Find("dianliang") != null)
            {
                dian = transform.Find("dianliang").GetComponent<UnrealProgressBar>();
                dian.init();

                xinhao = transform.Find("xinhao").GetComponent<SpritesList>();
                wifi = transform.Find("wifi").GetComponent<SpritesList>();

                xinhao.gameObject.SetActive(false);
                wifi.gameObject.SetActive(false);
            }

            if (transform.Find("settingbtn") != null)
            {
                settingBtn = transform.Find("settingbtn").GetComponent<UnrealScaleButton>();
                settingBtn.init();
            }

            roomid = transform.Find("roomid").GetComponent<UnrealTextField>();
            roomid.init();

            rulename = transform.Find("rulename").GetComponent<UnrealTextField>();
            rulename.init();

            ruledifen = transform.Find("ruledifen").GetComponent<UnrealTextField>();
            ruledifen.init();

            roomnum = transform.Find("roomnum").GetComponent<UnrealTextField>();
            roomnum.init();

            multiple = transform.Find("multiple").GetComponent<UnrealTextField>();
            multiple.init();

            nowtime = transform.Find("nowtime").GetComponent<UnrealTextField>();
            nowtime.init();

            if (transform.Find("recorderbtn"))
            {
                recorderbtn = transform.Find("recorderbtn").GetComponent<UnrealScaleButton>();
                recorderbtn.init();
            }

            if (transform.Find("imbtn"))
            {
                imbtn = transform.Find("imbtn").GetComponent<UnrealScaleButton>();
                imbtn.init();
            }

            if (transform.Find("dwbtn"))
            {
                dwbtn = transform.Find("dwbtn").GetComponent<UnrealScaleButton>();
                dwbtn.init();
            }
        }

        /// <summary> 显示电量 </summary>
        public virtual void showDianliang(string str)
        {
            int dian = StringKit.parseInt(str);
            this.dian.setValue((float)dian / 100);
        }

        /// <summary> 设置信号强度  1-4 </summary>
        public virtual void showXinhao(string str)
        {
            int[] xinhaos = StringKit.parseInts(str);
            if (xinhaos[1] == -1) return;

            SpritesList xh = null;
            if (xinhaos[0] == 1)//wifi
            {
                wifi.gameObject.SetActive(true);
                xinhao.gameObject.SetActive(false);
                xh = wifi;
            }
            else
            {
                wifi.gameObject.SetActive(false);
                xinhao.gameObject.SetActive(true);
                xh = xinhao;
            }

            int num = xinhaos[1];
            if (num > 3) num = 3;
            xh.ShowIndex(num);
        }

        protected long lasttime = 0;

        protected override void xupdate()
        {
            if (nowtime != null)
                nowtime.text = TimeKit.format(TimeKit.currentTimeMillis, "HH:mm");
            if (xinhao != null)
            {
                if (TimeKit.currentTimeMillis - lasttime >= 5 * 1000)
                {
                    lasttime = TimeKit.currentTimeMillis;

                    WXManager.getInstance().getCurBattery("Root", "showPKDianliang");
                    WXManager.getInstance().getNetWorkStrength("Root", "showPKXinhao");
                }
            }
        }

        protected StringBuilder roominfos = new StringBuilder();

        protected override void xrefresh()
        {
            Rule rule = Room.room.roomRule.rule;

            hideShow(rule);

            roominfos.Append(rule.name);
            rulename.text = roominfos.ToString();

            roominfos.Remove(0, roominfos.Length);
            roominfos.Append(Room.room.getRoomIndex());
            roomid.text = roominfos.ToString();

            roominfos.Remove(0, roominfos.Length);
            roominfos.Append(Room.room.roomRule.getNowPalyNum());
            roomnum.text = roominfos.ToString();

            roominfos.Remove(0, roominfos.Length);
            roominfos.Append(MathKit.getRound2Int(rule.getBet()) * DDZMultipleBean.baseScore);
            ruledifen.text = roominfos.ToString();

            roominfos.Remove(0, roominfos.Length);
            foreach (var item in rule.getRuleInfo())
            {
                roominfos.Append(item).Append(',');
            }
            roominfos.Remove(0, roominfos.Length);
        }

        public virtual void hideShow(Rule rule)
        {
            if (Room.room.isStatus(Room.STATE_MATCH) && rule.sid != 3006)// 10张跑得快不显示
            {
                multiple.setVisible(true);
            }
            else
            {
                multiple.text = "0";
                multiple.setVisible(false);
            }

            if (recorderbtn)
            {
                if (Room.room.isStatus(Room.STATE_MATCH))
                    recorderbtn.setVisible(rule.getRuleAtribute("record"));
                else recorderbtn.setVisible(false);
            }

            if (imbtn)
            {
                imbtn.setVisible(!rule.getRuleAtribute("disablechattool"));
            }

            //if (startbutton)
            //{
            //    startbutton.setVisible(!rule.getRuleAtribute("disablechattool"));
            //}

            if (settingBtn)
            {
                settingBtn.setVisible(true);
            }

        }

        public void recordShow(bool b)
        {
            if (recorderbtn != null)
            {
                recorderbtn.setVisible(b);
            }
        }

        public void dwShow(bool b)
        {
            if (dwbtn != null)
            {
                dwbtn.setVisible(b);
            }
        }

        public virtual void setMuitplier(int Muitiplier)
        {
            multiple.text = "" + Muitiplier;
            xrefresh();
        }
    }
}
