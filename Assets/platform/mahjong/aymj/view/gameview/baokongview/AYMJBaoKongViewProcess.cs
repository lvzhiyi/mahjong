using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 向后端发送报杠数据
    /// </summary>
    public class AYMJBaoKongViewProcess : MouseClickProcess
    {
        public override void execute()
        {
            int type = SendMJMatchCommand.BAO_KONG;
            int step = AYMJMatch.match.step;
            AYMJGameView view = getRoot<AYMJRoomPanel>().getGameView<AYMJGameView>();
            int[] cards = view.getBaoKongView().selectCard.toArray();
            if (cards.Length == 0)
            {
                Alert.show("请选中需要报杠的牌！");
                return;
            }
            view.getBaoKongView().setVisible(false);
            view.hideOperateView();
            CommandManager.addCommand(new SendMJMatchCommand(type, step, cards));
            UnrealRoot.root.getDisplayObject<AYMJRoomPanel>().gameView.setTingView(null);
        }
    }
}
