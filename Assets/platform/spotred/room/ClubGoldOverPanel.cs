using cambrian.common;
using platform.spotred.waitroom;
using UnityEngine;

namespace platform.spotred.room
{
    public class ClubGoldOverPanel:OverPanel
    {
        public static int counttime = 0;

        [HideInInspector] public int time = counttime;
        public override void show(Room room, CPMatch scene, int card, int index)
        {
            base.show(room, scene, card, index);
            this.button.text.text = "下一局";
            this.button.setVisible(true);
            time = counttime;
            curtime = 0;
        }

        public override void setVisible(bool b)
        {
            base.setVisible(b);
            if (b)
            {
                IpGPS3Panel gps3Panel = UnrealRoot.root.checkDisplayObject<IpGPS3Panel>();
                if (gps3Panel != null)
                    gps3Panel.setVisible(false);

                PlayerPropInfoPanel propInfo = UnrealRoot.root.checkDisplayObject<PlayerPropInfoPanel>();
                if (propInfo != null)
                {
                    propInfo.setVisible(false);
                }
            }
        }


        private long curtime = 0;
        protected override void xupdate()
        {
            base.xupdate();

            if (time <= 0) return;

            long cu = TimeKit.currentTimeMillis;

            if (curtime == 0)
            {
                curtime = cu;
                return;
            }

            if (cu - curtime > 1000)
            {
                curtime = cu;
                time--;
                if (time <= 0)
                {
                    this.button.text.text = "下一局";
                }
                else
                {
                    this.button.text.text = "下一局(" + time + "s)";
                }
                
            }

        }
    }
}
