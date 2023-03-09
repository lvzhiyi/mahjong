using basef.teahouse;
using cambrian.common;
using cambrian.unreal.scroll;
using cambrian.uui;
using UnityEngine;
using UnityEngine.UI;

namespace basef.arena
{
    public class ArenaDeskBar: ScorllTableBar
    {
        /// <summary>
        /// roomid
        /// </summary>
        private Text roomid;
        /// <summary>
        /// name
        /// </summary>
        private Text rule_name;
        /// <summary>
        /// jushu
        /// </summary>
        private Text jushu;

        private UnrealScaleButton btn;

        [HideInInspector] public ArenaRoom room;

        private UnrealTableContainer heads;

        private UnrealLuaSpaceObject roomLuaSpaceObject;

        private Text number;
        /// <summary>
        /// 桌子旗子状态
        /// </summary>
        SpritesList deskStatusIcons;
        /// <summary>
        /// 当前房间类型
        /// </summary>
        public bool roomType;
        /// <summary>
        /// 锁定规则名称
        /// </summary>
        private Text lockRuleName;
        /// <summary>
        /// 桌布图片列表
        /// </summary>
        private SpritesList deskbg;
        /// <summary>
        /// 竞赛场
        /// </summary>
        private Arena arena;
        /// <summary>
        /// 当前锁定的规则
        /// </summary>
        private ArenaLockRule rule;

        protected override void xinit()
        {
            this.roomLuaSpaceObject = this.transform.Find("room").GetComponent<UnrealLuaSpaceObject>();
            this.roomid = this.transform.Find("room").Find("roomid").GetComponent<Text>();
            this.rule_name = this.transform.Find("room").Find("name").GetComponent<Text>();
            this.jushu = this.transform.Find("room").Find("jushu").GetComponent<Text>();
            this.btn = this.transform.Find("room").Find("join").GetComponent<UnrealScaleButton>();
            this.heads = this.transform.Find("room").Find("container").GetComponent<UnrealTableContainer>();
            this.heads.init();

            this.deskStatusIcons = this.transform.Find("icon").GetComponent<SpritesList>();

            this.number = this.transform.Find("number").GetComponent<Text>();
            this.lockRuleName = this.transform.Find("rulename").GetComponent<Text>();
            this.deskbg = this.transform.Find("bg").GetComponent<SpritesList>();
        }

        public void setClubRoom(ArenaRoom room, bool roomType, Arena arena, ArenaLockRule rule)
        {
            this.room = room;
            this.roomType = roomType;
            this.arena = arena;
            this.rule = rule;
        }

        public ArenaLockRule getRule()
        {
            return rule;
        }

        protected override void xrefresh()
        {
            deskStatusIcons.setVisible(true);
            number.gameObject.SetActive(true);
            this.lockRuleName.gameObject.SetActive(false);
            this.number.text = (this.index_space + 1) + "";

            this.roomLuaSpaceObject.setVisible(false);
            if (this.room != null)
            {
               
                if (room.isStatus(RoomEvent.WAIT) || room.isStatus(RoomEvent.INIT))
                    deskStatusIcons.ShowIndex(0);
                else
                    deskStatusIcons.ShowIndex(1);

                this.rule_name.text = room.rule.name;
                this.jushu.text = "局数:" + room.getCurTray();
                this.jushu.gameObject.SetActive(true);
                if (roomType)
                    this.roomid.text = "房号:" + room.roomid;
                else
                {
                    this.roomid.text = "底分:" + room.rule.rule.getBet();
                    this.jushu.gameObject.SetActive(false);
                }

                int playernum = room.rule.rule.playerCount;

                if (room.playercount == playernum)
                    this.btn.setVisible(false);
                else
                    this.btn.setVisible(true);

                this.heads.resize(playernum);
                for (int i = 0; i < playernum; i++)
                {
                    TeaHousePlayerHeadBar bar = (TeaHousePlayerHeadBar)this.heads.bars[i];
                    bar.setMatchPlayer(room.players[i]);
                    bar.refresh();
                }

                deskbg.ShowIndex(room.rule.rule.playerCount - 2, true);
                this.roomLuaSpaceObject.setVisible(true);
                if (room.rule.rule.playerCount == 2)
                {
                    var b = heads.bars[1];
                    var height= b.getHeight();
                    var h = b.transform.localPosition.y - height;
                    DisplayKit.setLocalY(b.gameObject,h);
                }
            }
            else
            {
                deskStatusIcons.ShowIndex(0);
                deskbg.ShowIndex(2, true);
            }

            refreshLockRuleInfo();
        }

        protected void refreshLockRuleInfo()
        {
            if (arena == null) return;
            ArrayList<ArenaLockRule> rules = this.arena.fkcSettings.getRules();
            if (rules.count >= 1 && rule != null&&index_space<rules.count)
            {
                lockRuleName.text = rule.name+ "\n(创建房间)";
                lockRuleName.gameObject.SetActive(true);
                deskStatusIcons.setVisible(false);
                number.gameObject.SetActive(false);
                deskbg.ShowIndex(rule.rule.playerCount-2);
            }
        }
    }
}
