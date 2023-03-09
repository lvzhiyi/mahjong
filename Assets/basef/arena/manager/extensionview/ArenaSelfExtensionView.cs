using UnityEngine.UI;

namespace basef.arena
{
    /// <summary>
    /// 自己的推广任务
    /// </summary>
    public class ArenaSelfExtensionView:UnrealLuaSpaceObject
    {
        /// <summary>
        /// 推广任务
        /// </summary>
        private Text count;

        private UnrealTextScaleButton add;

        protected override void xinit()
        {
            count = transform.Find("num").GetComponent<Text>();
            add = transform.Find("postion").Find("add").GetComponent<UnrealTextScaleButton>();
        }

        protected override void xrefresh()
        {
            count.text = Arena.arena.getMember().getTask().ToString();
            add.setVisible(Arena.arena.getMember().isMaster());
        }
    }
}
