using cambrian.common;
using cambrian.game;
using System;
using UnityEngine;

namespace basef.notice
{
    public class OpenNewsListPanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            CommandManager.addCommand(new NewsCommand(), this.onCommand);
        }

        public void onCommand(object obj)
        {
            string[] strs = ((string)obj).Split(new string[] { "\n</chapter>\n" }, StringSplitOptions.RemoveEmptyEntries);

            ArrayList<News> list = new ArrayList<News>();

            News[] arr = new News[strs.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = JsonUtility.FromJson<News>(strs[i]);
                if (arr[i].isEffective())
                {
                    list.add(arr[i]);
                }
            }

            NewsListPanel panel = UnrealRoot.root.getDisplayObject<NewsListPanel>();
          //  panel.setTitle(this.getTitle());
            panel.setNotics(list.toArray());
            panel.refresh();
            panel.setVisible(true);
            SoundManager.playButton();

        }
        public override string getTitle()
        {
            return "公告";
        }
    }
}
