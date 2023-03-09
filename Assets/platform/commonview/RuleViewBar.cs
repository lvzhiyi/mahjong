using UnityEngine.UI;

namespace platform
{
    /// <summary>
    /// 这个类准备房间和比赛房间都在用
    /// </summary>
    public class RuleViewBar: UnrealLuaSpaceObject
    {
        private Text text;
        protected override void xinit()
        {
            this.text = this.transform.Find("text").GetComponent<Text>();
        }

        public void setText(string info)
        {
            this.text.text = info;
        }
    }
}
