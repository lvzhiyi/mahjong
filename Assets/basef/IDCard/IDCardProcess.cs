using basef.IDCard;
using cambrian.common;
using cambrian.game;
using System;

namespace basef.authname
{
    public class IDCardProcess : MouseClickProcess
    {
        IDCardPanel panel;

        public override void execute()
        {
            this.panel = this.getRoot<IDCardPanel>();
            string nickname = this.panel.nickName.text;
            string idcard = this.panel.idcard.text;


            if (!StringKit.isName(nickname))
            {
                Alert.show("姓名为汉字!");
                this.panel.nickName.text = "";
                this.panel.idcard.text = "";
                return;
            }
            //CheckCidInfo18(idcard);

           // Debug.Log("------"+CheckCidInfo18(idcard));

            if (!CheckCidInfo18(idcard))
            {
                Alert.show("您的身份证号码格式有误!");
                this.panel.nickName.text = "";
                this.panel.idcard.text = "";
                return;
            }


            CommandManager.addCommand(new IDCardCommand(nickname, idcard), callback);
        }

        private void callback(object obj)
        {
            if (obj == null) return;
            ByteBuffer data = (ByteBuffer)obj;
            bool b = data.readBoolean();
            if (b)
            {
                Alert.autoShow("认证成功");
                User.user.status = data.readInt();
                this.root.setVisible(false);
            }
        }


        public bool CheckCidInfo18(string cid)
        {
            string[] aCity = new string[] { null, null, null, null, null, null, null, null, null, null, null,
                "北京", "天津", "河北", "山西", "内蒙古",
                null, null, null, null, null, "辽宁", "吉林", "黑龙江", null, null, null, null, null, null, null,
                "上海", "江苏", "浙江", "安微", "福建", "江西", "山东", null, null, null,
                "河南", "湖北", "湖南", "广东", "广西", "海南", null, null, null,
                "重庆", "四川", "贵州", "云南", "西藏", null, null, null, null, null, null,
                "陕西", "甘肃", "青海", "宁夏", "新疆", null, null, null, null, null, "台湾",
                null, null, null, null, null, null, null, null, null, "香港", "澳门",
                null, null, null, null, null, null, null, null, "国外" };
            double iSum = 0;
            System.Text.RegularExpressions.Regex rg = new System.Text.RegularExpressions.Regex(@"^\d{17}(\d|X|x)$");
            System.Text.RegularExpressions.Match mc = rg.Match(cid);
            if (!mc.Success)
            {
                //Alert.show( "- 您的身份证号码格式有误!");
                return false;
            }
            cid = cid.ToLower();
            cid = cid.Replace("x", "a");
            if (aCity[int.Parse(cid.Substring(0, 2))] == null)
            {
                return false;//非法地区
            }
            try
            {
                DateTime.Parse(cid.Substring(6, 4) + "-" + cid.Substring(10, 2) + "-" + cid.Substring(12, 2));
            }
            catch
            {
                return false;//非法生日
            }
            for (int i = 17; i >= 0; i--)
            {
                iSum += (System.Math.Pow(2, i) % 11) * int.Parse(cid[17 - i].ToString(), System.Globalization.NumberStyles.HexNumber);

            }
            if (iSum % 11 != 1)
                return false;//非法证号

            return true;

        }
    }
}

