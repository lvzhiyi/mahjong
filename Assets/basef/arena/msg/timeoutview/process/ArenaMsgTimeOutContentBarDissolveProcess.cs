using cambrian.common;

namespace basef.arena
{
    /// <summary> 消息 房间超时 解散 </summary>
    public class ArenaMsgTimeOutContentBarDissolveProcess : MouseClickProcess
    {
        ArenaRoomEvent data;

        public override void execute()
        {
            data = transform.parent.GetComponent<ArenaMsgTimeOutContentBar>().getData();
            CommandManager.addCommand(new ArenaMsgTimeOutRoomDissolveCommand(data.uuid, ArenaEvent.EVENT_TYPE_ROOM),back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            if ((bool)obj)
            {
                Alert.show("房间" + data.room + " 解散成功");
                ArenaMsgPanel panel = UnrealRoot.root.getDisplayObject<ArenaMsgPanel>();
                panel.managerView.timeoutView.getList().remove(data);
                panel.managerView.timeoutView.refresh();
            }
        }
    }
}
