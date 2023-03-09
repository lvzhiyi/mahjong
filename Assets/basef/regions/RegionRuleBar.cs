namespace basef.regions
{
    public class RegionRuleBar:UnrealLuaSpaceObject
    {
        private UnrealTextField text;

        protected override void xinit()
        {
            this.text = this.transform.Find("rulename").GetComponent<UnrealTextField>();
        }

        public void setText(string name)
        {
            this.text.text = name;
        }
    }
}
