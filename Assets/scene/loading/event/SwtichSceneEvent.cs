using UnityEngine.SceneManagement;

namespace scene.loading
{
    public class SwtichSceneEvent : Process
    {
        int level;

        public SwtichSceneEvent(int level)
        {
            this.level = level;
        }

        public override void execute()
        {
            SceneManager.LoadScene(level);
        }
    }
}
