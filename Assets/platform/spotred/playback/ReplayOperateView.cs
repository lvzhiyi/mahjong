using cambrian.common;
using platform.spotred.room;

namespace platform.spotred.playback
{
    public class ReplayOperateView : OperateView
    {
        public override int showButton(int operate, int paidui)
        {
            hideAllBtn();
            if (operate == 0)
            {
                return 0;
            }

            ArrayList<int> ll = new ArrayList<int>();
            for (int i = 0; i < operatesType.Length; i++)
            {
                if ((operate & operatesType[i]) == operatesType[i])
                {
                    ll.add(operatesType[i]);
                }
            }

            list = new ArrayList<UnrealScaleButton>();

            for (int i = 0; i < ll.count; i++)
            {
                UnrealScaleButton btn = getShowButton(ll.get(i));
                if (btn != null) list.add(btn);
            }

            float btn_x = 0;

            for (int i = list.count - 1; i >= 0; i--)
            {
                UnrealScaleButton btn = list.get(i);
                float width = btn.getWidth();
                DisplayKit.setLocalXY(btn.transform, 0, -btn_x);
                btn.setVisible(true);
                btn_x += width;
            }

            float w = getWidth();
            if (list.count > 1)
            {
                float w_1 = w / 2;
                if (list.count % 2 != 0)
                    w_1 = 0;
                DisplayKit.setLocalY(this, w * (list.count / 2) - w_1);
            }
            else
            {
                DisplayKit.setLocalY(this, 0);
            }
            return list.count;
        }
    }
}
