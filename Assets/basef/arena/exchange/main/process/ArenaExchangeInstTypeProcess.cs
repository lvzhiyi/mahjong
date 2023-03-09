using cambrian.common;

namespace basef.arena
{
    /// <summary> 界面选择 </summary>
    public class ArenaExchangeInstTypeProcess : MouseClickProcess
    {
        ArenaExchangePanel panel;

        int index;

        public override void execute()
        {
            index = GetComponent<ArenaExchangeTypeBtnBar>().index_space;

            panel = getRoot<ArenaExchangePanel>();
            switch (index)
            {
                case ArenaExchange.Prize_Match:
                    panel.showView(index);
                    break;
                case ArenaExchange.Prize_Physical:
                    CommandManager.addCommand(new GetArenaExchangePhysicalCommand(),physicalBack);
                    break;
                default: break;
            }
        }

        public void physicalBack(object obj)
        {
            if (obj == null) return;
            panel.showView(index,obj);
        }
    }
}
