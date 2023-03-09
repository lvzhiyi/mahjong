namespace platform.mahjong
{
    public class SelectMJCardBgImgProcess : MouseClickProcess
    {
        public override void execute()
        {
            MJSettingDeskBgBar bar = GetComponent<MJSettingDeskBgBar>();
            getRoot<MJRoomSettingPanel>().selectedCardDi(bar.index_space);
            MJSettingManager.setcardbgImg(bar.index_space);

            if (UnrealRoot.root.checkDisplayObject<MahjongRoomPanel>())
                UnrealRoot.root.getDisplayObject<MahjongRoomPanel>().refreshCardImg();
            else if (UnrealRoot.root.checkDisplayObject<AYMJRoomPanel>())
                UnrealRoot.root.getDisplayObject<AYMJRoomPanel>().refreshCardImg();
        }
    }
}
