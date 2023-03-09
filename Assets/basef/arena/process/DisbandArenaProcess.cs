using basef.player;
using cambrian.common;
using UnityEngine;

namespace basef.arena
{
    /// <summary>
    /// 解散竞赛场
    /// </summary>
    public class DisbandArenaProcess : MouseClickProcess
    {
        public override void execute()
        {
            Alert.confirmShow("您确认要解散休闲场吗?", ok);
        }

        public void ok(Transform transform)
        {
            Arena arena = Arena.arena;
            CommandManager.addCommand(new DisbandArenaCommand(arena.getId()), (obj) =>
            {
                if (obj == null) return;
                if ((bool)obj)
                {
                    Alert.autoShow("你已经解散了休闲场!");
                    //刷新茶馆列表
                    CommandManager.addCommand(new GetArenaListCommand(), (obj1) =>
                    {
                        if (obj1 == null) return;
                        ArrayList<Arena> list = (ArrayList<Arena>)obj1;
                        ArrayList<Arena> createList = new ArrayList<Arena>();
                        ArrayList<Arena> joinList = new ArrayList<Arena>();

                        Arena temp = null;
                        for (int i = 0; i < list.count; i++)
                        {
                            temp = list.get(i);
                            if (temp.getMaster() == Player.player.userid)
                                createList.add(temp);
                            else
                                joinList.add(temp);
                        }

                        ArenaListPanel panel = UnrealRoot.root.getDisplayObject<ArenaListPanel>();
                        panel.setData(createList, joinList);
                        panel.refresh();
                        panel.setVisible(true);
                    }
                    );
                    this.root.setVisible(false);
                    UnrealRoot.root.getDisplayObject<ArenaPanel>().setCVisible(false);
                }
                else
                {
                    Alert.show("解散失败!");
                }
            });
        }
    }
}
