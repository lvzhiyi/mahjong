namespace platform.mahjong
{
    public class SelectMJCardImgProcess : MouseClickProcess
    {
        public override void execute()
        {
            MJSettingDeskBgBar bar = GetComponent<MJSettingDeskBgBar>();
            getRoot<MJRoomSettingPanel>().selectedCardBg(bar.index_space);
            MJSettingManager.setcardImg(bar.index_space);
            if (UnrealRoot.root.checkDisplayObject<MahjongRoomPanel>())
                UnrealRoot.root.getDisplayObject<MahjongRoomPanel>().refreshCardImg();
           
            else if (UnrealRoot.root.checkDisplayObject<AYMJRoomPanel>())
                UnrealRoot.root.getDisplayObject<AYMJRoomPanel>().refreshCardImg();
        }
    }
}
