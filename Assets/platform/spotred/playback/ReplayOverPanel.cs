using platform.spotred.room;
using UnityEngine;

namespace platform.spotred.playback
{
    /// <summary>
    /// 回放结束界面
    /// </summary>
    public class ReplayOverPanel : UnrealLuaPanel
    {
        /// <summary>
        /// 房间
        /// </summary>
        Room room;

        /// <summary>
        /// 容器
        /// </summary>
        UnrealTableContainer container;

        /// <summary>
        /// 下一局
        /// </summary>
        [HideInInspector] public UnrealTextButton button;

        /// <summary>
        /// 
        /// </summary>
        UnrealTextField title;


        protected override void xinit()
        {
            this.container = this.transform.Find("Canvas").Find("container").GetComponent<UnrealTableContainer>();
            this.container.init();
            this.title = this.transform.Find("Canvas").Find("title").GetComponent<UnrealTextField>();
            this.title.init();
            this.button = this.transform.Find("Canvas").Find("quit").GetComponent<UnrealTextButton>();
            this.button.init();

            this.resizeDelta(new Margin());
        }

        public void show(Room room, CPMatch scene, int card, int index)
        {
            this.room = room;
            this.title.text = this.room.roomRule.getPlayRule(false);

            this.button.text.text = "返回";
            
            int num = room.players.Length;
            this.container.resize(num);


            for (int i = 0; i < num; i++)
            {
                OverBar bar = (OverBar) this.container.bars[i];

                bool b = false;
                if (index != RecvOverProcess.NORMAL_OVER && index == i)
                {
                    b = true;
                }

                bar.setInfo(room, room.players[i].player, scene.getPCards()[i], i == scene.dangjia,
                    room.getSpotRedCount().getIndexCount(i), card, b);
                bar.refresh();
            }
        }
    }
}
