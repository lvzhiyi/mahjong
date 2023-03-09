
using basef.im;
using UnityEngine;

namespace platform.spotred.room
{
    /// <summary>
    /// 表情，短语，文字视图
    /// </summary>
    public class ExpressionView:UnrealLuaSpaceObject
    {
        private ExpressionPlayerView down;
        private ExpressionPlayerView right;
        private ExpressionPlayerView left;
        private ExpressionPlayerView top;
        protected override void xinit()
        {
            this.down = transform.Find("down").GetComponent<ExpressionPlayerView>();
            this.down.init();

            this.right = transform.Find("right").GetComponent<ExpressionPlayerView>();
            this.right.init();

            this.left = transform.Find("left").GetComponent<ExpressionPlayerView>();
            this.left.init();

            this.top = transform.Find("top").GetComponent<ExpressionPlayerView>();
            this.top.init();
        }

        protected override void xrefresh()
        {
            this.down.refresh();
            this.right.refresh();
            this.left.refresh();
            this.top.refresh();
        }


        public void playMJQuickMsg(int type, IMQuickMsg msg, Vector3 pos)
        {
            switch (type)
            {
                case RoomPanel.LOC_DOWN:
                    down.setLocalVector3(pos);
                    this.down.playQuickMsg(msg);
                    break;
                case RoomPanel.LOC_LEFT:
                    left.setLocalVector3(pos);
                    this.left.playQuickMsg(msg);
                    break;
                case RoomPanel.LOC_RIGHT:
                    right.setLocalVector3(pos);
                    this.right.playQuickMsg(msg);
                    break;
                case RoomPanel.LOC_TOP:
                    top.setLocalVector3(pos);
                    this.top.playQuickMsg(msg);
                    break;
            }
        }

        public void setMJIMTextMsg(int type, IMTextMsg msg, Vector3 pos)
        {
            switch (type)
            {
                case RoomPanel.LOC_DOWN:
                    down.setLocalVector3(pos);
                    this.down.setIMTextMsg(msg);
                    break;
                case RoomPanel.LOC_LEFT:
                    left.setLocalVector3(pos);
                    this.left.setIMTextMsg(msg);
                    break;
                case RoomPanel.LOC_RIGHT:
                    right.setLocalVector3(pos);
                    this.right.setIMTextMsg(msg);
                    break;
                case RoomPanel.LOC_TOP:
                    top.setLocalVector3(pos);
                    this.top.setIMTextMsg(msg);
                    break;
            }
        }



        public void playQuickMsg(int type, IMQuickMsg msg)
        {
            switch (type)
            {
                case RoomPanel.LOC_DOWN:
                    this.down.playQuickMsg(msg);
                    break;
                case RoomPanel.LOC_LEFT:
                    this.left.playQuickMsg(msg);
                    break;
                case RoomPanel.LOC_RIGHT:
                    this.right.playQuickMsg(msg);
                    break;
                case RoomPanel.LOC_TOP:
                    this.top.playQuickMsg(msg);
                    break;
            }
        }

        public void setIMTextMsg(int type,IMTextMsg msg)
        {
            switch (type)
            {
                case RoomPanel.LOC_DOWN:
                    this.down.setIMTextMsg(msg);
                    break;
                case RoomPanel.LOC_LEFT:
                    this.left.setIMTextMsg(msg);
                    break;
                case RoomPanel.LOC_RIGHT:
                    this.right.setIMTextMsg(msg);
                    break;
                case RoomPanel.LOC_TOP:
                    this.top.setIMTextMsg(msg);
                    break;
            }
        }

        public void playVoice(int type,bool isShow)
        {
            switch (type)
            {
                case RoomPanel.LOC_DOWN:
                    this.down.showVoice(isShow);
                    break;
                case RoomPanel.LOC_LEFT:
                    this.left.showVoice(isShow);
                    break;
                case RoomPanel.LOC_RIGHT:
                    this.right.showVoice(isShow);
                    break;
                case RoomPanel.LOC_TOP:
                    this.top.showVoice(isShow);
                    break;
            }
        }
    }
}
