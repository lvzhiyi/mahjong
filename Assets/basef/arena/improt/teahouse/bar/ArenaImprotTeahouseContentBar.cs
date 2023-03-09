using basef.player;
using cambrian.unreal.scroll;

namespace basef.arena
{
    /// <summary> 竞赛场 导入茶馆 内容bar </summary>
    public class ArenaImprotTeahouseContentBar : ScrollBar
    {
        ArenaImprotTeahouseData data;

        /// <summary> 茶馆头像 </summary>
        PlayerHeadView head;

        /// <summary> 茶馆名字 </summary>
        UnrealTextField teahousename;

        /// <summary> 茶馆ID </summary>
        UnrealTextField housersid;


        protected override void xinit()
        {
            head = this.transform.Find("head").GetComponent<PlayerHeadView>();
            head.init();

            teahousename = this.transform.Find("teahousename").GetComponent<UnrealTextField>();
            teahousename.init();

            housersid = this.transform.Find("housersid").GetComponent<UnrealTextField>();
            housersid.init();
        }

        protected override void xrefresh()
        {
            teahousename.text = data.name;
            housersid.text = data.id.ToString();
            setHead();
        }

        private void setHead()
        {
            head.setData(data.headurl,data.id);
            head.refresh();
        }

        public void setData(ArenaImprotTeahouseData data)
        {
            this.data = data;
        }

        public ArenaImprotTeahouseData getData()
        {
            return data;
        }
    }
}
