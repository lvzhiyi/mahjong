namespace basef.arena
{
    /// <summary>
    /// 增加成员到限制组
    /// </summary>
    public class AddLimitrenaMemberProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaAddLimitDeskPanel panel = getRoot<ArenaAddLimitDeskPanel>();
            ArenaAddLimitLeftBar bar = transform.GetComponent<ArenaAddLimitLeftBar>();
            if (bar.member.isSelect)
            {
                bar.showIcon(false);
                bar.member.isSelect = false;
                panel.removeRigthArenaMember(bar.member.userid);
            }
            else
            {
                bar.showIcon(true);
                bar.member.isSelect = true;
                ArenaMutexMember member = new ArenaMutexMember(bar.member.userid, bar.member.name, bar.member.head);
                panel.addRightArenaMember(member);
            }
        }
    }
}
