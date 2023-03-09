using basef.notice;
using cambrian.common;

namespace basef.lobby
{
    public class ShowLobbyPanel
    {
        /// <summary>
        /// 显示大厅界面
        /// </summary>
        public static void showLobbyPanel(bool isOnlyShow=true)
        {
            SpotLobbyPanel panel = UnrealRoot.root.getDisplayObject<SpotLobbyPanel>();
            panel.refresh();
            if (isOnlyShow)
                UnrealRoot.root.showPanel<SpotLobbyPanel>();
            else
                panel.setVisible(true);

            
        }

        public static void refreshLobbyPanel()
        {
            UnrealRoot.root.getDisplayObject<SpotLobbyPanel>().refresh();
        }

        public static UnrealLuaPanel checkDisplayObject()
        {
            return UnrealRoot.root.checkDisplayObject<SpotLobbyPanel>();
        }

        public static UnrealLuaPanel getDisplayObject()
        {
            return UnrealRoot.root.getDisplayObject<SpotLobbyPanel>();
        }

        /// <summary>
        /// 刷新滚动公告内容
        /// </summary>
        public static void refreshScrollNotice(ArrayList<ScrollNotice> scrollnotices)
        {
            SpotLobbyPanel panel = UnrealRoot.root.getDisplayObject<SpotLobbyPanel>();
            panel.noticeList.setNotics(scrollnotices);
            panel.noticeList.refresh();
            panel.noticeList.setVisible(true);
        }

        public static void recvNoticeMsg(ScrollNotice notice)
        {
            NoticeListView panel = UnrealRoot.root.getDisplayObject<SpotLobbyPanel>().noticeList;

            for (int i = 0; i < panel.notics.count; i++)
            {
                if (panel.notics.get(i).id == notice.id)
                {
                    return;
                }
            }
            panel.notics.add(notice);
            panel.refresh();
        }

        public static void updateNoticeMsg(ScrollNotice notice)
        {
            NoticeListView panel = UnrealRoot.root.getDisplayObject<SpotLobbyPanel>().noticeList;

            for (int i = 0; i < panel.notics.count; i++)
            {
                if (panel.notics.get(i).id == notice.id)
                {
                    if (notice.isEffective())
                    {
                        panel.notics.set(notice, i);
                        panel.refresh();
                    }
                    else
                    {
                        panel.notics.removeAt(i);
                        panel.refresh();
                        return;
                    }
                }
            }
        }

        public static void delteNoticeMsg(int id)
        {
            NoticeListView panel = UnrealRoot.root.getDisplayObject<SpotLobbyPanel>().noticeList;

            for (int i = 0; i < panel.notics.count; i++)
            {
                if (panel.notics.get(i).id == id)
                {
                    panel.notics.removeAt(i);
                    panel.refresh();
                }
            }
        }
    }
}
