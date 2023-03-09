namespace platform.spotred.over
{
    public class OpenShareViewProcess:MouseClickProcess
    {
        public UnrealLuaSpaceObject shareview;

        public override void execute()
        {
            if (shareview != null)
                shareview.setVisible(true);
        }
    }
}
