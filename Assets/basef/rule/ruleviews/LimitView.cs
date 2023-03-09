using UnityEngine.UI;

namespace basef.rule
{
    /// <summary>
    /// 限制bar
    /// </summary>
    public class LimitView : UnrealLuaSpaceObject
    {
        Rule rule;

        WanFaBar ip;

        WanFaBar gps;

        bool islocked;

        Dropdown selectTypes;

        int[] types;

        protected override void xinit()
        {
            this.ip = this.transform.Find("ip").GetComponent<WanFaBar>();
            this.ip.init();
            this.ip.index_space = 0;
            this.gps = this.transform.Find("gps").GetComponent<WanFaBar>();
            this.gps.init();
            this.gps.index_space = 1;
            this.selectTypes = gps.transform.Find("types").GetComponent<Dropdown>();
        }

        public void setData(Rule rule,bool islock)
        {
            this.rule = rule;
            this.islocked = islock;
            types = rule.getLimittypes();
        }

        protected override void xrefresh()
        {
            ip.setData("IP限制",rule.getIpLimit(),islocked);
            gps.setData("GPS限制", rule.getGpsLimit(), islocked);
            updateDropDownItem();
        }

        public void updateDropDownItem()
        {
            selectTypes.options.Clear();
            Dropdown.OptionData temoData;
            for (int i = 0; i < types.Length; i++)
            {
                //给每一个option选项赋值
                temoData = new Dropdown.OptionData();
                temoData.text = types[i]+"米";
                selectTypes.options.Add(temoData);
            }
            selectTypes.captionText.text = rule.getGpsLimitDistance()+"米";//初始选项的显示
        }

        public void onSelect()
        {
            rule.setGpsLimitDistance(types[selectTypes.value]);
        }
    }
}
