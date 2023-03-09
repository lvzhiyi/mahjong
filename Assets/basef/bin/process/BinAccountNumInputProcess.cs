using cambrian.game;

namespace basef.bin
{
    public class BinAccountNumInputProcess : MouseClickProcess
    {
        public override void execute()
        {
            NumberKey key = this.GetComponent<NumberKey>();
            this.getRoot<BinAccountPanel>().text.text += key.key;
            SoundManager.playButton();
        }
    }
}
