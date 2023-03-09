using cambrian.uui;
using UnityEngine;

namespace platform.spotred.room
{
    /// <summary>
    /// 单局结束界面
    /// </summary>
    public class OverPanel:UnrealLuaPanel
    {
        /// <summary>
        /// 房间
        /// </summary>
        [HideInInspector] Room room;
        /// <summary>
        /// 容器
        /// </summary>
        UnrealTableContainer container;
        /// <summary>
        /// 下一局
        /// </summary>
        [HideInInspector] public UnrealTextButton button;
        /// <summary>
        /// 规则标题
        /// </summary>
        UnrealTextField title;
        /// <summary>
        /// 游戏结束标题
        /// </summary>
        SpritesList over_titles;
        /// <summary>
        /// 底牌视图
        /// </summary>
        [HideInInspector] public RemaincardsView remainView;
        protected override void xinit()
        {
            base.xinit();
            this.container = this.content.Find("container").GetComponent<UnrealTableContainer>();
            this.container.init();
            this.title = this.content.Find("title").GetComponent<UnrealTextField>();
            this.title.init();
            this.button = this.content.Find("next").GetComponent<UnrealTextButton>();
            this.button.init();
            this.over_titles = this.content.Find("over_titles").GetComponent<SpritesList>();
            this.remainView = this.content.Find("remainingcards").GetComponent<RemaincardsView>();
            this.remainView.init();

            this.resizeDelta(new Margin());
        }
        
        public virtual void show(Room room, CPMatch scene,int card,int index)
        {
            this.room = room;
            title.text = this.room.roomRule.getPlayRule(false);
            button.text.text = room.roomRule.isOver() ? "结算" : "下一局";
            int num = room.players.Length;
            container.resize(num);
            long score = 0;
           
            for (int i = 0; i < num; i++)
            {
                if (scene.mindex == i)
                {
                    score = scene.getPCards()[i].point;
                    if (score == 0)
                    {
                        this.over_titles.ShowIndex(0);                  
                    }
                    else if(score<0)
                    {
                        this.over_titles.ShowIndex(2);
                    }
                    else if(score>0)
                    {
                        this.over_titles.ShowIndex(3);
                    }

                    if (card <= 0)
                    {
                        this.over_titles.ShowIndex(1);
                    }
                }
                OverBar bar = (OverBar)this.container.bars[i];

                bool b = false;
                if (index != RecvOverProcess.NORMAL_OVER&&index==i)
                {
                    b = true;
                    this.over_titles.ShowIndex(2);
                }

                bar.setInfo(room, room.players[i].player, scene.getPlayerCardss<CPPlayerCards>()[i], i == scene.dangjia,room.getSpotRedCount().getIndexCount(i),card,b);
                bar.refresh();

                this.remainView.setCards(scene.getRemainingCards());
                this.remainView.refresh();
            }
        }
    }
}
