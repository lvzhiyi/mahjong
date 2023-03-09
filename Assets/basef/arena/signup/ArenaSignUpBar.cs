namespace basef.arena
{
    /// <summary> 报名内容 bar </summary>
    public class ArenaSignUpBar : UnrealLuaSpaceObject
    {
        UnrealTextField info;

        /// <summary> 数量 </summary>
        public int num;

        protected override void xinit()
        {
            info = transform.Find("info").GetComponent<UnrealTextField>();
            info.init();
        }

        public void setData(int num)
        {
            this.info.text = num.ToString();
            this.num = num;
        }
    }
}
