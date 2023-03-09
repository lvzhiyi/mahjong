namespace platform.mahjong
{
    public class SelectMJDeskTopProcess:MouseClickProcess
    {
        public override void execute()
        {
            MJSettingDeskBgBar bar = GetComponent<MJSettingDeskBgBar>();
            getRoot<MJRoomSettingPanel>().selectedDeskTop(bar.index_space);
            MJSettingManager.setDeskTopStyle(bar.index_space);
            if (UnrealRoot.root.checkDisplayObject<MahjongRoomPanel>())
                UnrealRoot.root.getDisplayObject<MahjongRoomPanel>().refreshDeskTop();
            else if (UnrealRoot.root.checkDisplayObject<AYMJRoomPanel>())
                UnrealRoot.root.getDisplayObject<AYMJRoomPanel>().refreshDeskTop();
        }
    }
}
