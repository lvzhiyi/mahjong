using cambrian.game;

namespace basef.setting
{
    /// <summary>
    /// 保存声音配置
    /// </summary>
    public class SaveSoundProcess : MouseClickProcess
    {
        public override void execute()
        {
            SoundManager.save();
        }
    }
}
