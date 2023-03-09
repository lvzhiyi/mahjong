using cambrian.game;
using scene.game;

namespace basef.server
{
    public class DelUserProcess : MouseClickProcess
    {
        public override void execute()
        {
            User user = this.transform.parent.GetComponent<UserBar>().user;
            User.logout(user);
            ((SpotRedRoot)UnrealRoot.root).showUsersPanel(false);
        }
    }
}
