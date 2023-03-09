using UnityEngine;

namespace scene.game
{
    public class QuitApplicationProcess : MouseClickProcess
    {
        public override void execute()
        {
          //  CommandManager.addCommand(new UpdatePlayerStateCommand(false));
            Application.Quit();
        }
    }
}