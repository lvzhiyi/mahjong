using UnityEngine;


namespace cambrian.game
{

    /// <summary>
    /// 数据加载界面
    /// </summary>
    public class LoadingDataPanel : UnrealLuaPanel
    {
        private Transform load;

        protected override void xinit()
        {

            this.load = this.transform.Find("Canvas").Find("loading");
            this.resizeDelta(new Margin());
        }

        public void hidden()
        {
            this.setVisible(false);
        }

        protected override void xupdate()
        {
            this.load.Rotate(0, 0, -5);
        }
    }

}

