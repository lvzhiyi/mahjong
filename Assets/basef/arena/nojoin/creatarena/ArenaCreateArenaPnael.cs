using UnityEngine;

namespace basef.arena
{
    /// <summary> 创建竞赛场面板 </summary>
    public class ArenaCreateArenaPnael : UnrealLuaPanel
    {
        [HideInInspector] public UnrealInputTextField arenaname;

        [HideInInspector] public UnrealInputTextField notice;
        /// <summary>
        /// 是否打烊
        /// </summary>
        [HideInInspector] public bool closing = false;
        /// <summary>
        /// 是否显示排行榜
        /// </summary>
        [HideInInspector] public bool rank = false;

        //GameObject arenaclosing;

        //GameObject arenarank;

        protected override void xinit()
        {
            base.xinit();
            arenaname = this.content.Find("centers").Find("arenaname").GetComponent<UnrealInputTextField>();
            arenaname.init();

            notice = this.content.Find("centers").Find("arenasid").GetComponent<UnrealInputTextField>();
            notice.init();

            //arenaclosing = this.content.Find("centers").Find("arenaclosing").gameObject;

            //arenarank = this.content.Find("centers").Find("arenarank").gameObject;
        }

        

        public void setClosing(bool closing)
        {
            this.closing = closing;
        }

        public void setRank(bool rank)
        {
            this.rank = rank;
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            arenaname.text = "";
            //refreshSelect();
        }

        public void refreshSelect()
        {
            //refreshSelect(arenaclosing, closing);
            //refreshSelect(arenarank, rank);
        }

        private void refreshSelect(GameObject obj,bool whether)
        {
            obj.transform.Find("yes").Find("true").gameObject.SetActive(whether);
            obj.transform.Find("no").Find("true").gameObject.SetActive(!whether);
        }
    }
}
