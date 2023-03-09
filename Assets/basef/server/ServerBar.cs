using cambrian.game;
using UnityEngine;
using UnityEngine.UI;

namespace basef.server
{
    public class ServerBar : UnrealLuaSpaceObject
    {
        /// <summary>
        /// 服务器
        /// </summary>
        [HideInInspector]
        public Server server;

        /// <summary>
        /// 名字
        /// </summary>
        Text text;


        protected override void xinit()
        {
            base.xinit();
            this.text = this.transform.Find("text").GetComponent<Text>();
        }

        public void setServer(Server server)
        {
            this.server = server;
        }
        protected override void xrefresh()
        {
            base.xrefresh();
            this.text.text = this.server.name;
        }
    }
}
