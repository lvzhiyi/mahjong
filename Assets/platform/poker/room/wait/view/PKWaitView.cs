namespace platform.poker
{
    /// <summary> 房间等待中视图 </summary>
    public class PKWaitView : UnrealLuaSpaceObject
    {
        /// <summary> 房卡场 </summary>
        public PKRoomCardWaitView room_card { get; set; }

        /// <summary> 金币场</summary>
        private PKRoomGoldWaitView room_gold;

        protected override void xinit()
        {
            room_card = transform.Find("room_card").GetComponent<PKRoomCardWaitView>();
            room_card.init();
        }

        protected override void xrefresh()
        {
            if (Room.room == null) return;
            room_card.setVisible(true);
            room_card.refresh();
        }
    }
}
