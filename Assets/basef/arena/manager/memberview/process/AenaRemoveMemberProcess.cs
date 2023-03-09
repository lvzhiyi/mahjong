using cambrian.common;
using UnityEngine;

namespace basef.arena
{
    public class AenaRemoveMemberProcess:MouseClickProcess
    {
        private ArenaMemberDetailView view;
        public override void execute()
        {
            view = transform.parent.parent.GetComponent<ArenaMemberDetailView>();
            Alert.confirmShow("你确认要移除 " + view.member.name+" 玩家吗?",alertback);
        }

        public void alertback(Transform t)
        {
            CommandManager.addCommand(new ArenaRemoveMemberCommand(Arena.arena.getId(), view.member.userid), back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            if ((bool)obj)
            {
                Alert.autoShow("操作成功!");
            }
            view.setVisible(false);
            UnrealRoot.root.getDisplayObject<ArenaManagerPanel>().managerView.memberView.removeMember(view.member);
        }
    }
}
