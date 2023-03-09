using cambrian.common;

namespace basef.arena
{
    public class SearchArenMemberProcess:MouseClickProcess
    {
        private ArenaMemberView view;
        public override void execute()
        {
            view= transform.parent.parent.GetComponent<ArenaMemberView>();
            string str= view.inputTextField.text;
            if (str == null || str.Length == 0) return;
            long userid = StringKit.parseLong(str);
            view.setSingleMember(userid);
         }
    }
}
