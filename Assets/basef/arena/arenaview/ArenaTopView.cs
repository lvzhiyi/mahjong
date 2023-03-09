using basef.player;
using UnityEngine;
using UnityEngine.UI;

namespace basef.arena
{
    /// <summary>
    /// 上方视图
    /// </summary>
    public class ArenaTopView : UnrealLuaSpaceObject
    {
        /// <summary>
        /// 竞赛场名称
        /// </summary>
        private UnrealTextField arenaName;

        /// <summary>
        /// 竞赛场id
        /// </summary>
        private UnrealTextField id;

        /// <summary>
        /// 金豆数量
        /// </summary>
        private UnrealTextField gold;

        /// <summary>
        /// 奖章数量
        /// </summary>
        private UnrealTextField medal;

        /// <summary>
        /// 消息
        /// </summary>
        private UnrealScaleButton msg;

        /// <summary>
        /// 消息初始位置
        /// </summary>
        private Vector3 msgpos;

        /// <summary>
        /// 兑换
        /// </summary>
        private UnrealScaleButton exchange;

        /// <summary>
        /// 兑换初始位置
        /// </summary>
        private Vector3 exchagepos;

        /// <summary>
        /// 邀请
        /// </summary>
        private UnrealScaleButton invitation;

        /// <summary>
        /// 邀请初始位置
        /// </summary>
        private Vector3 invitationpos;

        /// <summary>
        /// 报名
        /// </summary>
        private UnrealScaleButton sign;

        /// <summary>
        /// 报名初始位置
        /// </summary>
        private Vector3 signpos;

        private UnrealLuaSpaceObject invitationView;

        private Image addMsg;

        protected override void xinit()
        {
            arenaName = transform.Find("name").GetComponent<UnrealTextField>();
            arenaName.init();
            id = transform.Find("id").GetComponent<UnrealTextField>();
            id.init();
            gold = transform.Find("gold").GetComponent<UnrealTextField>();
            gold.init();
            medal = transform.Find("medal").GetComponent<UnrealTextField>();
            medal.init();
            msg = transform.Find("msg").GetComponent<UnrealScaleButton>();
            msgpos = msg.transform.localPosition;
            exchange = transform.Find("exchange").GetComponent<UnrealScaleButton>();
            exchagepos = exchange.transform.localPosition;
            invitation = transform.Find("invitation").GetComponent<UnrealScaleButton>();
            invitationpos = invitation.transform.localPosition;
            invitationView = transform.Find("invitationview").GetComponent<UnrealLuaSpaceObject>();
            invitation.init();
            sign = transform.Find("signup").GetComponent<UnrealScaleButton>();
            signpos = sign.transform.localPosition;
            addMsg = msg.transform.Find("addnews").GetComponent<Image>();
            
        }

        protected override void xrefresh()
        {
            Arena arena = Arena.arena;
            arenaName.text = arena.getName();
            id.text = "ID:" + arena.getId();
            gold.text = arena.getMember().getArenaGold().ToString();
            medal.text = Player.player.medal.ToString();
            if (arena.getMember().isAgent())
                masterShow();
            else
                normalShow();

            showMsgHint(Arena.arena.isNewMsg);
        }

        public void showInvitationView(bool b)
        {
            invitationView.setVisible(b);
        }

        /// <summary>
        /// 擂主显示(或者普通推广员)
        /// </summary>
        private void masterShow()
        {
            sign.transform.localPosition = signpos;
            exchange.transform.localPosition = exchagepos;
            invitation.transform.localPosition = invitationpos;
            invitationView.transform.localPosition = invitationpos;
            msg.transform.localPosition = msgpos;
            invitation.setVisible(true);
            msg.setVisible(true);
        }

        /// <summary>
        /// 普通玩家显示
        /// </summary>
        private void normalShow()
        {
            sign.transform.localPosition = exchagepos;
            exchange.transform.localPosition = msgpos;
            invitation.setVisible(false);
            msg.setVisible(false);
        }

        public void showMsgHint(bool isActive)
        {
            addMsg.gameObject.SetActive(isActive);
        }
    }
}
