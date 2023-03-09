using basef.player;
using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine;
using UnityEngine.UI;

namespace basef.arena
{
    public class ArenaExtensionTaskApplyBar:ScrollBar
    {
        private PlayerHeadView head;

        private Text nickname;

        private Text id;

        private Text task;

        private Text time;
        /// <summary>
        /// 同意，拒绝
        /// </summary>
        private Text operateType;

        [HideInInspector] public ArenaEvent data;

        private UnrealLuaSpaceObject operateview;

        protected override void xinit()
        {
            head = transform.Find("head").GetComponent<PlayerHeadView>();
            head.init();
            nickname = transform.Find("nickname").GetComponent<Text>();
            id = transform.Find("id").GetComponent<Text>();
            task = transform.Find("tasknum").GetComponent<Text>();
            time = transform.Find("time").GetComponent<Text>();
            operateType = transform.Find("opeeratejiguo").GetComponent<Text>();
            operateview = transform.Find("operate").GetComponent<UnrealLuaSpaceObject>();
        }

        public void setData(ArenaEvent data)
        {
            this.data = data;
        }

        protected override void xrefresh()
        {
            head.setData(data.head,data.getUserid());
            head.refresh();
            nickname.text = data.name;
            id.text = data.getUserid().ToString();
            task.text = data.getValue().ToString();
            time.text = TimeKit.format(data.time, "yyyy/MM/dd HH:mm:ss");
            operateview.setVisible(data.getStatus() == ArenaEvent.STATUS_WAIT);
            operateType.gameObject.SetActive(data.getStatus() != ArenaEvent.STATUS_WAIT);
            if (data.getStatus() == ArenaEvent.STATUS_AGREE)
            {
                operateType.text = "同意";
                operateType.color = ColorKit.getColor(47,182,38,255);
            }
            else
            {
                operateType.text = "拒绝";
                operateType.color = ColorKit.getColor(238,1,1,255);
            }
        }
    }
}
