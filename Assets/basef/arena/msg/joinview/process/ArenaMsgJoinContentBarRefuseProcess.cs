﻿namespace basef.arena
{
    /// <summary> 加入申请 圈主拒绝 </summary>
    public class ArenaMsgJoinContentBarRefuseProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaMsgJoinContentData data = transform.parent.GetComponent<ArenaMsgJoinContentBar>().getData();
            Alert.show("已拒绝 用户ID : " + data.usersid + " 加入本赛场!!!");
            
            ArenaMsgPanel panel = UnrealRoot.root.getDisplayObject<ArenaMsgPanel>();
            panel.managerView.joinView.getList().remove(data);

            //panel.managerView.showJoinApplyView();

            data = null;
            panel = null;
        }
    }
}
