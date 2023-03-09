using UnityEngine;

namespace basef.arena
{
    public class ArenaBottomBtnView : UnrealLuaSpaceObject
    {

        RectTransform rank;

        RectTransform playbackbtn;

        RectTransform coffer;

        RectTransform group;

        RectTransform wf;

        RectTransform agent;

        RectTransform tongji;

        protected override void xinit()
        {
            group = transform.Find("group").GetComponent<RectTransform>();

            playbackbtn = group.Find("playbackbtn").GetComponent<RectTransform>();

            rank = group.Find("rank").GetComponent<RectTransform>();

            coffer = group.Find("coffer").GetComponent<RectTransform>();

            if (group.Find("rule") != null) wf = group.Find("rule").GetComponent<RectTransform>();

            if (group.Find("agent") != null) agent = group.Find("agent").GetComponent<RectTransform>();

            if (group.Find("tongji") != null) tongji = group.Find("tongji").GetComponent<RectTransform>();
        }

        float width = 0;

        protected override void xrefresh()
        {
            rank.gameObject.SetActive(Arena.arena.hasStatus(Arena.STATE_NO_SHOW_RANK));
            if (wf != null) wf.gameObject.SetActive(Arena.arena.getMember().isMaster());
            width = 0;
            addGroupWidth(coffer);
            addGroupWidth(rank);
            addGroupWidth(playbackbtn);

            //group.sizeDelta = new Vector2(this.width,70);
        }

        private void addGroupWidth(RectTransform gameobj)
        {
            if (gameobj == null) return;
            if (gameobj.gameObject.activeSelf)
            {
                this.width += gameobj.rect.width;
            }
            else
            {

            }
        }
    }
}
