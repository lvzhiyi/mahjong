using basef.notice;
using cambrian.common;
using UnityEngine;

namespace basef.arena
{
    /// <summary>
    /// 下方视图
    /// </summary>
    public class ArenaBottomView : UnrealLuaSpaceObject
    {
        private ArenaBottomBtnView normal;

        private ArenaBottomBtnView master;

        private NoticeListView noticeview;

        private ArrayList<ScrollNotice> notics = new ArrayList<ScrollNotice>();

        protected override void xinit()
        {
            normal = transform.Find("normal").GetComponent<ArenaBottomBtnView>();
            normal.init();
            master = transform.Find("master").GetComponent<ArenaBottomBtnView>();
            master.init();
            noticeview = transform.Find("notices").GetComponent<NoticeListView>();
            noticeview.init();
        }

        protected override void xrefresh()
        {
            bool isAgent = Arena.arena.getMember().isAgent();
            master.setVisible(isAgent);
            normal.setVisible(!isAgent);
            master.refresh();
            normal.refresh();

            if (noticeview.notics == null|| noticeview.notics.count==0)
            {
                setNotic();
            }
            else if (noticeview.notics.getLast().content != Arena.arena.getNotice())
            {
                setNotic();
            }
        }

        public void setNotic()
        {
            ScrollNotice notice = new ScrollNotice();
            notice.content = Arena.arena.getNotice();
            if (noticeview.notics != null)
                noticeview.notics.clear();
            notics.add(notice);
            noticeview.setNotics(notics);
            noticeview.refresh();
        }
    }
}
