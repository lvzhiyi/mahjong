namespace basef.arena
{
    public class RemoveLimitRenaMemberProcess:MouseClickProcess
    {
        public override void execute()
        {
            ArenaAddLimitDeskPanel panel = getRoot<ArenaAddLimitDeskPanel>();
            ArenaAddLimitRightBar bar = transform.GetComponent<ArenaAddLimitRightBar>();

            if (panel.isUpdate&& panel.rightmembers.count<=2)
            {
                Alert.show("至少保留两个成员!如需移除，请在限制组列表中删除。");
                return;
            }

            panel.removeRigthArenaMember(bar.member.userid);
            panel.refrshleftMemberIsSelect(bar.member.userid);
        }
    }
}
