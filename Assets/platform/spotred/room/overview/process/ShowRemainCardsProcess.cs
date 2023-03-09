using UnityEngine;

namespace platform.spotred.room
{
    /// <summary>
    /// 控制显示剩余牌
    /// </summary>
    public class ShowRemainCardsProcess:MouseClickProcess
    {
        public override void execute()
        {
            OverPanel panel= getRoot<OverPanel>();
            
            UnrealCheckBox box= GetComponent<UnrealCheckBox>();
            if (box.getState())
            {
                panel.remainView.setVisible(false);
            }
            else
            {
                panel.remainView.setVisible(true);
            }

            box.reverse();
        }
    }
}
