namespace platform.spotred.room
{
    public class PropView:UnrealLuaSpaceObject
    {
        PropHeadView top;

        PropHeadView right;

        PropHeadView left;

        PropHeadView down;

        protected override void xinit()
        {
            this.top = this.transform.Find("top").GetComponent<PropHeadView>();
            this.top.init();
            this.right = this.transform.Find("right").GetComponent<PropHeadView>();
            this.right.init();
            this.left = this.transform.Find("left").GetComponent<PropHeadView>();
            this.left.init();
            this.down = this.transform.Find("down").GetComponent<PropHeadView>();
            this.down.init();
        }

        public void showPropAnimation(int srcindex,int destIndex,int prop)
        {
            PropHeadView useview=null;
            switch (srcindex)
            {
                case RoomPanel.LOC_DOWN:
                    useview = down;
                    break;
                case RoomPanel.LOC_RIGHT:
                    useview = right;
                    break;
                case RoomPanel.LOC_TOP:
                    useview = top;
                    break;
                case RoomPanel.LOC_LEFT:
                    useview = left;
                    break;
            }

            PropHeadView destview=null;

            switch (destIndex)
            {
                case RoomPanel.LOC_DOWN:
                    destview = down;
                    break;
                case RoomPanel.LOC_RIGHT:
                    destview = right;
                    break;
                case RoomPanel.LOC_TOP:
                    destview = top;
                    break;
                case RoomPanel.LOC_LEFT:
                    destview = left;
                    break;
            }
            useview.moveImage(destview.transform.localPosition, prop,destview.effect.showIndex);
        }
    }
}
