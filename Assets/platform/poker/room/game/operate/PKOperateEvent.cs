using cambrian.common;

namespace platform.poker
{
    /// <summary> 操作 </summary>
    public class PKOperateEvent : MouseClickSlideProcess
    {
        public int type;

        public override void mouseClick()
        {
            switch (PKGAME.RULESID)
            {
                case PKGAME.DDZ:
                    CommandManager.addCommand(new DDZSendMatchCommand(type, DDZMatch.match.step, null));
                    break;
                case PKGAME.PDK:
                    CommandManager.addCommand(new PDKSendMatchCommand(type, PDKMatch.match.step, null));
                    break;
                case PKGAME.PDK_10:
                    CommandManager.addCommand(new PDKSendMatchCommand(type, PDKTenMatch.match.step, null));
                    break;
                case PKGAME.PDK_ANYUE:
                    CommandManager.addCommand(new PDKSendMatchCommand(type, PDKAnYueMatch.match.step, null));
                    break;
            }
            transform.parent.gameObject.SetActive(false);
        }
    }
}
