using UnityEngine.UI;

namespace platform.spotred.waitroom
{
    /// <summary>
    /// 距离显示
    /// </summary>
    public class GPSBar: UnrealLuaSpaceObject
    {
        private Text text;

        protected override void xinit()
        {
            this.text = this.transform.Find("text").GetComponent<Text>();
        }

        public void setDistance(string distance)
        {
            this.text.text = distance;
            this.text.gameObject.SetActive(true);
        }

        public void hideText()
        {
            this.text.gameObject.SetActive(false);
            this.setVisible(true);
        }
    }
}
