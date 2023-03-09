using basef.player;
using cambrian.common;
using cambrian.game;
using platform;
using scene.game;

namespace basef.rule
{
    public class CreateRoomProcess: MouseClickProcess
    {
        public override void execute()
        {
            this.getRoot<SpotRoomRulePanel>().getRulesView().getRule().save(CacheLocalPath.RULE_INFO_PATH);

            int createRoomStauts = this.getRoot<SpotRoomRulePanel>().getCreatRoomStauts();
            if (createRoomStauts== SpotRoomRulePanel.PROMTER_CREATE_ROOM)
            {
                CommandManager.addCommand(new PromoterCreateRoomCommand(this.getRoot<SpotRoomRulePanel>().getRulesView().getRule()), callback_1);
            }
            else
            {
                WXManager.getInstance().getGPSLocation("Root", "call_back_gps");
                CommandManager.addCommand(new CreateRoomCommand(this.getRoot<SpotRoomRulePanel>().getRulesView().getRule()), callback_2);
            }
           
            root.setVisible(false);
            SoundManager.playButton();
        }


        public void callback_2(object obj)
        {
            if (obj == null) return;
            new ShowWaiteRoomCallBackProcess().execute();
        }

        public void callback_1(object obj)
        {
            if (obj == null) return;
            if((bool)obj)
                new GetPromoterRoomListProcess().execute();

        }
    }
}
