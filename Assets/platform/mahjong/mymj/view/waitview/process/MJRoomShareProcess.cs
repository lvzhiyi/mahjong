namespace platform.mahjong
{
    public class MJRoomShareProcess:MouseClickProcess
    {
        public UnrealLuaSpaceObject space;
        public override void execute()
        {
            space.setVisible(true);
        }
    }
}
