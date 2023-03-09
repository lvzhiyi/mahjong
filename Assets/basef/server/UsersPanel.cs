using cambrian.game;
using scene.game;
using scene.loading;
using UnityEngine;

namespace basef.server
{
    public class UsersPanel:UnrealLuaPanel
    {
        User[] users;

        UnrealTableContainer container;

        protected override void xinit()
        {
            base.xinit();
            this.container = this.maskedContainer.GetComponent<UnrealTableContainer>();
            this.container.init();
            this.resizeDelta(new Margin());
        }

        public void setUsers(User[] users)
        {
            this.users = users;
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            this.container.resize(this.users.Length);
            for (int i = 0; i < this.users.Length; i++)
            {
                UserBar bar = ((UserBar)this.container.bars[i]);
                bar.setUser(this.users[i]);
                bar.refresh();
            }
            this.container.resizeDelta();
        }
    }
}
