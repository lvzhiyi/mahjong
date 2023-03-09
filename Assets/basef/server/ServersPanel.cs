using cambrian.game;

namespace basef.server
{
    public class ServersPanel:UnrealLuaPanel
    {
        Server[] servers;

        UnrealTableContainer container;

        protected override void xinit()
        {
            base.xinit();
            this.container = this.maskedContainer.GetComponent<UnrealTableContainer>();
            this.container.init();
            this.resizeDelta(new Margin());
        }

        public void setServers(Server[] servers)
        {
            this.servers = servers;
        }

        protected override void xrefresh()
        {
            this.container.resize(this.servers.Length);
            for (int i = 0; i < this.servers.Length; i++)
            {
                ServerBar bar = ((ServerBar)this.container.bars[i]);
                bar.setServer(this.servers[i]);
                bar.refresh();
            }
            this.container.resizeDelta();
        }
    }
}
