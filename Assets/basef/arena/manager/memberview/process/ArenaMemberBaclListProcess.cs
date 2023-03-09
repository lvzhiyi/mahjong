using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 黑名单操作
    /// </summary>
    public class ArenaMemberBaclListProcess:MouseClickProcess
    {
        private ArenaMemberDetailView view;
        public override void execute()
        {
            view= transform.parent.parent.GetComponent<ArenaMemberDetailView>();
            bool b = view.member.hasStatus(ArenaMember.STATUS_ARENA_LIMIT);
            CommandManager.addCommand(new LaHeiMemberCommand(Arena.arena.getId(),view.member.userid,!b),back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            if ((bool) obj)
            {
                Alert.autoShow("操作成功!");
                bool b = view.member.hasStatus(ArenaMember.STATUS_ARENA_LIMIT);
                view.member.setStatus(ArenaMember.STATUS_ARENA_LIMIT,!b);
                view.setCallBack();
            }
            view.refresh();
            view.setVisible(false);
        }
    }
}
