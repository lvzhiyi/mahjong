using cambrian.common;
using UnityEngine;

namespace basef.arena
{
    public class HandArenaExtensionTaskApplyProcess:MouseClickProcess
    {
        public bool agree;

        private ArenaExtensionTaskApplyBar bar;
        public override void execute()
        {
            bar= transform.parent.parent.GetComponent<ArenaExtensionTaskApplyBar>();
            CommandManager.addCommand(new ArenaMsgApplyContentEventCommand(agree,bar.data.uuid,ArenaEvent.EVENT_TYPE_AGENT),back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            if (agree)
            {
                bar.data.setStatus(ArenaEvent.STATUS_AGREE);
            }
            else
            {
                bar.data.setStatus(ArenaEvent.STATUS_REFUSE);
            }

            bar.refresh();
        }
    }
}
