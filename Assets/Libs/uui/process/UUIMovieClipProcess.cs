using XLua;

namespace cambrian.uui
{
    /// <summary>
    /// 帧动画播放驱动器
    /// </summary>
    [Hotfix]
    public class UUIMovieClipProcess : TimerListener<UnrealLuaPanel>
    {
        const long SPACE_MILLS = 33L;
        /// <summary>
        /// 上次刷新时间
        /// </summary>
        long lasttime;

        int index;

        UnrealSprite[] uuisprites;

        public void setSprites(UnrealSprite[] uuisprites)
        {
            this.uuisprites = uuisprites;
        }
        public override void update(long time)
        {
            if(uuisprites==null)return;
            if (time - this.lasttime < SPACE_MILLS) return;
            this.lasttime = time;
            if (this.index >= 0 && this.index < this.uuisprites.Length) this.uuisprites[this.index].setVisible(false);
            this.index++;
            if (this.index == this.uuisprites.Length)
            {
                index = -1;
                this.setVisible(false);
            }
            else this.uuisprites[this.index].setVisible(true);
        }
        public override void setVisible(bool b)
        {
            this.gameObject.SetActive(b);
        }
    }
}