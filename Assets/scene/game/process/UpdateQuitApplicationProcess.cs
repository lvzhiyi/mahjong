using cambrian.game;

namespace scene.game
{
    public class UpdateQuitApplicationProcess : MouseClickProcess
    {
        public override void execute()
        {
            WXManager.getInstance().exitSystem();
        }
    }
}