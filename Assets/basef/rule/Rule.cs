using cambrian.common;
using System;
using XLua;

namespace basef.rule
{

    public class Rule
    {
        /** 房卡支付模式：房主支付，AA支付，立即代付（代开房间，不加入） */
        public const int TYPE_PAY_MASTER = 0, TYPE_PAY_AA = 1, TYPE_PAY_PROXY = 2;

        /// <summary>
        /// 点炮类型
        /// </summary>
        public static int DIAO_PAO_TYPE = 1;

        /// <summary>
        /// 麻将自摸增益
        /// </summary>
        public static int ZI_MO_ADDTION_TYPE = 2;

        /// <summary>
        /// 麻将杠上花增益
        /// </summary>
        public static int KONG_ADDTING_TYPE = 3;

        /// <summary>
        /// 麻将换牌增益
        /// </summary>
        public static int REPLACE_COUNT_TYPE = 4;

        /// <summary>
        /// 人数
        /// </summary>
        public static int PLAYER_COUNT_TYPE = 5;

        /// <summary>
        /// 查叫
        /// </summary>
        public static int CHA_JIAO_TYPE = 6;

        /// <summary>
        /// 底分
        /// </summary>
        public static int DI_FEN = 7;

        /// <summary>
        /// 翻型
        /// </summary>
        public static int FAN_XING = 8;

        /// <summary>
        /// 几番起胡
        /// </summary>
        public static int CAN_HU_POINT = 9;

        /// <summary>
        /// 飘
        /// </summary>
        public static int PIAO = 10;

        /// <summary>
        /// 游戏模式
        /// </summary>
        public static int MODEL_TYPE = 11;
        /// <summary>
        /// 游戏倍数
        /// </summary>
        public static int MAX_MULTIPLE = 12;
        /// <summary>
        /// 顺序
        /// </summary>
        public static int ORDER = 13;
        /// <summary>
        /// 手牌数
        /// </summary>
        public static int HANDSNUM = 14;
        /// <summary>
        /// 打法
        /// </summary>
        public static int DAFA = 15;    

        //lua中的规则对象  
        public object luarule;

        /// <summary>
        /// 获取规则属性
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [CSharpCallLua]
        public delegate int getRuleIntAtribute(object obj, string name);
        public static getRuleIntAtribute attribute;

        /// <summary>
        /// 获取指定的属性
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int getIntAtribute(string name)
        {
            return attribute(luarule, name);
        }


        [CSharpCallLua]
        public delegate void setRuleIntAtribute(object obj, string name, int v);

        public static setRuleIntAtribute SetIntAtribute;

        /// <summary>
        /// 设置int，值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="v"></param>
        public void setRuleIntAtributeMethod(string name,int v)
        {
            SetIntAtribute(luarule,name,v);
        }

        [CSharpCallLua]
        public delegate bool getRuleboolAtribute(object obj, string name);

        public static getRuleboolAtribute boolattribute;

        /// <summary>
        /// 获取是否有该属性
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool getRuleAtribute(string name)
        {
            return boolattribute(luarule, name);
        }

        [CSharpCallLua]
        public delegate bool setRuleboolAtribute(object obj, string name,bool b);

        public static setRuleboolAtribute setattribute;

        /// <summary>
        /// 设置bool属性
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool setRuleBoolAtribute(string name,bool b)
        {
            return setattribute(luarule, name, b);
        }



        [CSharpCallLua]
        public delegate string getRulestringAtribute(object obj, string name);

        public static getRulestringAtribute stringAttribute;

        [CSharpCallLua]
        public delegate int[] getRuleIntsAtribute(object obj, string name);

        public static getRuleIntsAtribute intsAttibute;


        [CSharpCallLua]
        public delegate int[][] getRule2IntsAttribute(object obj, string name);

        public static getRule2IntsAttribute ints2Attibute;


        public static void clearDelegate()
        {
            Delegate b = LuaUtil.luaEnv.Global.Get<getRuleIntAtribute>("getRuleIntAtribute");
            b = null;
            LuaUtil.luaEnv.Global.Set("getRuleIntAtribute", b);

            b = LuaUtil.luaEnv.Global.Get<setRuleIntAtribute>("setRuleIntAtribute");
            b = null;
            LuaUtil.luaEnv.Global.Set("setRuleIntAtribute", b);


            b = LuaUtil.luaEnv.Global.Get<getRuleboolAtribute>("getRuleboolAtribute");
            b = null;
            LuaUtil.luaEnv.Global.Set("getRuleboolAtribute", b);
            b = LuaUtil.luaEnv.Global.Get<getRulestringAtribute>("getRulestringAtribute");
            b = null;
            LuaUtil.luaEnv.Global.Set("getRulestringAtribute", b);

            b = LuaUtil.luaEnv.Global.Get<getRuleIntsAtribute>("getRuleIntsAtribute");
            b = null;

            LuaUtil.luaEnv.Global.Set("getRulestringAtribute", b);

            b = LuaUtil.luaEnv.Global.Get<getRule2IntsAttribute>("getRule2IntsAttribute");
            b = null;
            LuaUtil.luaEnv.Global.Set("getRule2IntsAttribute", b);
            b = LuaUtil.luaEnv.Global.Get<getHelpUrlMethode>("getHelpUrlMethode");
            b = null;
            LuaUtil.luaEnv.Global.Set("getHelpUrlMethode", b);
            b = LuaUtil.luaEnv.Global.Get<bytesReadLocalMethode>("bytesReadLocalMethode");
            b = null;
            LuaUtil.luaEnv.Global.Set("bytesReadLocalMethode", b);
            b = LuaUtil.luaEnv.Global.Get<saveMethode>("saveMethode");
            b = null;
            LuaUtil.luaEnv.Global.Set("saveMethode", b);
            b = LuaUtil.luaEnv.Global.Get<setWanFanMethode>("setWanFanMethode");
            b = null;
            LuaUtil.luaEnv.Global.Set("setWanFanMethode", b);
            b = LuaUtil.luaEnv.Global.Get<isSelectedMethode>("isSelectedMethode");
            b = null;
            LuaUtil.luaEnv.Global.Set("isSelectedMethode", b);
            b = LuaUtil.luaEnv.Global.Get<getFanShuMethode>("getFanShuMethode");
            b = null;
            LuaUtil.luaEnv.Global.Set("getFanShuMethode", b);
            b = LuaUtil.luaEnv.Global.Get<getTrusteeshipMethod>("getTrusteeship");
            b = null;
            LuaUtil.luaEnv.Global.Set("getTrusteeship", b);
            b = LuaUtil.luaEnv.Global.Get<setTrusteeshipMethod>("setTrusteeship");
            b = null;
            LuaUtil.luaEnv.Global.Set("setTrusteeship", b);
            b = LuaUtil.luaEnv.Global.Get<getTrusteeshipTimeMethod>("getTrusteeshiptimearray");
            b = null;
            LuaUtil.luaEnv.Global.Set("getTrusteeshiptimearray", b);
            b = LuaUtil.luaEnv.Global.Get<getTrusteeTimeMethod>("getTrusteetime");
            b = null;
            LuaUtil.luaEnv.Global.Set("getTrusteetime", b);
            b = LuaUtil.luaEnv.Global.Get<setTrusteeTimeMethod>("setTrusteetime");
            b = null;
            LuaUtil.luaEnv.Global.Set("setTrusteetime", b);
            b = LuaUtil.luaEnv.Global.Get<getTrusteejstatustypesMethod>("getTrusteejstatustypes");
            b = null;
            LuaUtil.luaEnv.Global.Set("getTrusteejstatustypes", b);
            b = LuaUtil.luaEnv.Global.Get<getTrusteejstatusMethod>("getTrusteejsstatus");
            b = null;
            LuaUtil.luaEnv.Global.Set("getTrusteejsstatus", b);
            b = LuaUtil.luaEnv.Global.Get<setTrusteejstatusMethod>("setTrusteejstype");
            b = null;
            LuaUtil.luaEnv.Global.Set("setTrusteejstype", b);
            b = LuaUtil.luaEnv.Global.Get<getIpLimitMethod>("getIpLimit");
            b = null;
            LuaUtil.luaEnv.Global.Set("getIpLimit", b);
            b = LuaUtil.luaEnv.Global.Get<setIpLimitMethod>("setIpLimit");
            b = null;
            LuaUtil.luaEnv.Global.Set("setIpLimit", b);
            b = LuaUtil.luaEnv.Global.Get<getGpsLimitMethod>("getGpsLimit");
            b = null;
            LuaUtil.luaEnv.Global.Set("getGpsLimit", b);
            b = LuaUtil.luaEnv.Global.Get<setGpsLimitMethod>("setGpsLimit");
            b = null;
            LuaUtil.luaEnv.Global.Set("setGpsLimit", b);
            b = LuaUtil.luaEnv.Global.Get<getGpsLimitDistanceMethod>("getGpsLimitDistance");
            b = null;
            LuaUtil.luaEnv.Global.Set("getGpsLimitDistance", b);
            b = LuaUtil.luaEnv.Global.Get<setGpsLimitDistanceMethod>("setGpsLimitDistance");
            b = null;
            LuaUtil.luaEnv.Global.Set("setGpsLimitDistance", b);
            b = LuaUtil.luaEnv.Global.Get<getLimittypesMethod>("getLimittypes");
            b = null;
            LuaUtil.luaEnv.Global.Set("getLimittypes", b);

            b = LuaUtil.luaEnv.Global.Get<getRulePointMethode>("getRulePoint");
            b = null;
            LuaUtil.luaEnv.Global.Set("getRulePoint", b);
            b = LuaUtil.luaEnv.Global.Get<getRuleSingleNames>("getRuleSingleNames");
            b = null;
            LuaUtil.luaEnv.Global.Set("getRuleSingleNames", b);
            b = LuaUtil.luaEnv.Global.Get<setRuleSingleNames>("setRuleSingValue");
            b = null;
            LuaUtil.luaEnv.Global.Set("setRuleSingValue", b);
            b = LuaUtil.luaEnv.Global.Get<getWanFaMethode>("getWanFaMethode");
            b = null;
            LuaUtil.luaEnv.Global.Set("getWanFaMethode", b);
            b = LuaUtil.luaEnv.Global.Get<isCheckedRuleMethode>("isCheckedRuleMethode");
            b = null;
            LuaUtil.luaEnv.Global.Set("isCheckedRuleMethode", b);
            b = LuaUtil.luaEnv.Global.Get<getTipsMethode>("getTipsMethode");
            b = null;
            LuaUtil.luaEnv.Global.Set("getTipsMethode", b);
            b = LuaUtil.luaEnv.Global.Get<getPlayerNumMethode>("getPlayerNumMethode");
            b = null;
            LuaUtil.luaEnv.Global.Set("getPlayerNumMethode", b);
            b = LuaUtil.luaEnv.Global.Get<getPlayRuleMethode>("getPlayRuleMethode");
            b = null;
            LuaUtil.luaEnv.Global.Set("getPlayRuleMethode", b);
            b = LuaUtil.luaEnv.Global.Get<getBetMethode>("getBet");
            b = null;
            LuaUtil.luaEnv.Global.Set("getBet", b);
            b = LuaUtil.luaEnv.Global.Get<setBetMethode>("setBet");
            b = null;
            LuaUtil.luaEnv.Global.Set("setBet", b);
            b = LuaUtil.luaEnv.Global.Get<getBetMethode>("getChip");
            b = null;
            LuaUtil.luaEnv.Global.Set("getChip", b);
            b = LuaUtil.luaEnv.Global.Get<getTaxesRateMethod>("getTaxesRate");
            b = null;
            LuaUtil.luaEnv.Global.Set("getTaxesRate", b);
            b = LuaUtil.luaEnv.Global.Get<getPlatForm>("getPlatForm");
            b = null;
            LuaUtil.luaEnv.Global.Set("getPlatForm", b);
            b = LuaUtil.luaEnv.Global.Get<getRuleInfoMethode>("getRuleInfoMethode");
            b = null;
            LuaUtil.luaEnv.Global.Set("getRuleInfoMethode", b);
            b = LuaUtil.luaEnv.Global.Get<bytesReadRule>("bytesReadRule");
            b = null;
            LuaUtil.luaEnv.Global.Set("bytesReadRule", b);
            b = LuaUtil.luaEnv.Global.Get<bytesWriteRule>("bytesWriteRule");
            b = null;
            LuaUtil.luaEnv.Global.Set("bytesWriteRule", b);

            b = LuaUtil.luaEnv.Global.Get<setRuleboolAtribute>("setRuleboolAtribute");
            b = null;
            LuaUtil.luaEnv.Global.Set("setRuleboolAtribute", b);

            ccc();
        }

        public static void ccc()
        {
            attribute = null;
            SetIntAtribute = null;
            boolattribute = null;
            stringAttribute = null;
            intsAttibute = null;
            ints2Attibute = null;
            gethelpurl = null;
            bytesreadlocal = null;
            save1 = null;
            setWanFan1 = null;
            isSelected1 = null;
            getFanShu1 = null;
            trusteeship = null;
            settrusteeship = null;
            trusteeshiptime = null;
            trusteestime = null;
            settrusteestime = null;
            trusteejstatustypes = null;
            trusteejstatus = null;
            settrusteejstatus = null;
            getiplimit = null;
            setiplimit = null;
            getgpslimit = null;
            setgpslimit = null;
            getgpslimitdistance = null;
            setgpslimitdistance = null;
            getlimittypes = null;
            getRulePoint = null;
            getLuaRuleSingleNamesMethod = null;
            settLuaRuleSingleValueMethod = null;
            getWanFa1 = null;
            isCheckedRule1 = null;
            getTips1 = null;
            getPlayerNum1 = null;
            getplayrule = null;
            getBetMeth = null;
            setBetMeth = null;
            getChipMeth = null;
            getTaxesRateMeth = null;
            getPlatform = null;
            getRuleInfo1 = null;
            br = null;
            bw = null;
            setattribute = null;
        }

        public Rule()
        {
            LuaUtil.luaEnv.Global.Get("getRuleIntAtribute", out attribute);
            LuaUtil.luaEnv.Global.Get("setRuleIntAtribute", out SetIntAtribute);
            LuaUtil.luaEnv.Global.Get("getRuleboolAtribute", out boolattribute);
            LuaUtil.luaEnv.Global.Get("getRulestringAtribute", out stringAttribute);
            LuaUtil.luaEnv.Global.Get("getRuleIntsAtribute", out intsAttibute);
            LuaUtil.luaEnv.Global.Get("getRule2IntsAttribute", out ints2Attibute);


            LuaUtil.luaEnv.Global.Get("getHelpUrlMethode", out gethelpurl);
            LuaUtil.luaEnv.Global.Get("bytesReadLocalMethode", out bytesreadlocal);
            LuaUtil.luaEnv.Global.Get("saveMethode", out save1);
            LuaUtil.luaEnv.Global.Get("setWanFanMethode", out setWanFan1);
            LuaUtil.luaEnv.Global.Get("isSelectedMethode", out isSelected1);
            LuaUtil.luaEnv.Global.Get("getFanShuMethode", out getFanShu1);
            LuaUtil.luaEnv.Global.Get("getTrusteeship", out trusteeship);



            LuaUtil.luaEnv.Global.Get("setTrusteeship", out settrusteeship);
            LuaUtil.luaEnv.Global.Get("getTrusteeshiptimearray", out trusteeshiptime);
            LuaUtil.luaEnv.Global.Get("getTrusteetime", out trusteestime);
            LuaUtil.luaEnv.Global.Get("setTrusteetime", out settrusteestime);
            LuaUtil.luaEnv.Global.Get("getTrusteejstatustypes", out trusteejstatustypes);
            LuaUtil.luaEnv.Global.Get("getTrusteejsstatus", out trusteejstatus);
            LuaUtil.luaEnv.Global.Get("setTrusteejstype", out settrusteejstatus);



            LuaUtil.luaEnv.Global.Get("getIpLimit", out getiplimit);
            LuaUtil.luaEnv.Global.Get("setIpLimit", out setiplimit);
            LuaUtil.luaEnv.Global.Get("getGpsLimit", out getgpslimit);
            LuaUtil.luaEnv.Global.Get("setGpsLimit", out setgpslimit);
            LuaUtil.luaEnv.Global.Get("getGpsLimitDistance", out getgpslimitdistance);
            LuaUtil.luaEnv.Global.Get("setGpsLimitDistance", out setgpslimitdistance);
            LuaUtil.luaEnv.Global.Get("getLimittypes", out getlimittypes);



            LuaUtil.luaEnv.Global.Get("getRulePoint", out getRulePoint);
            LuaUtil.luaEnv.Global.Get("getRuleSingleNames", out getLuaRuleSingleNamesMethod);
            LuaUtil.luaEnv.Global.Get("setRuleSingValue", out settLuaRuleSingleValueMethod);
            LuaUtil.luaEnv.Global.Get("getWanFaMethode", out getWanFa1);
            LuaUtil.luaEnv.Global.Get("isCheckedRuleMethode", out isCheckedRule1);
            LuaUtil.luaEnv.Global.Get("getTipsMethode", out getTips1);



            LuaUtil.luaEnv.Global.Get("getPlayerNumMethode", out getPlayerNum1);
            LuaUtil.luaEnv.Global.Get("getPlayRuleMethode", out getplayrule);
            LuaUtil.luaEnv.Global.Get("getBet", out getBetMeth);
            LuaUtil.luaEnv.Global.Get("setBet", out setBetMeth);
            LuaUtil.luaEnv.Global.Get("getChip", out getChipMeth);
            LuaUtil.luaEnv.Global.Get("getTaxesRate", out getTaxesRateMeth);
            LuaUtil.luaEnv.Global.Get("getPlatForm", out getPlatform);



            LuaUtil.luaEnv.Global.Get("getRuleInfoMethode", out getRuleInfo1);
            LuaUtil.luaEnv.Global.Get("bytesReadRule", out br);
            LuaUtil.luaEnv.Global.Get("bytesWriteRule", out bw);

            LuaUtil.luaEnv.Global.Get("setRuleboolAtribute", out setattribute);
            

        }

        public int sid
        {
            get { return attribute(luarule, "sid"); }
            set { SetIntAtribute(luarule, "sid", value); }
        }

        public string name
        {
            get { return stringAttribute(luarule, "name"); }
        }

        /// <summary>
        /// 声音路径
        /// </summary>
        public string soundPath
        {
            get { return stringAttribute(luarule, "name"); }
        }

        public int payType
        {
            get { return attribute(luarule, "payType"); }
            set { SetIntAtribute(luarule, "payType", value); }
        }

        public int matchCount
        {
            get { return attribute(luarule, "matchCount"); }
            set { SetIntAtribute(luarule, "matchCount", value); }
        }

        public int playerCount
        {
            get { return attribute(luarule, "playerCount"); }
            set { SetIntAtribute(luarule, "playerCount", value); }
        }

        public int gameModel
        {
            get { return attribute(luarule, "gameModel"); }
            set { SetIntAtribute(luarule, "gameModel", value); }
        }

        public bool isFree
        {
            get { return boolattribute(luarule, "isFree"); }
        }

        public int[][] jushu
        {
            get { return ints2Attibute(luarule, "jushu"); }
        }


        public bool iscomplete
        {
            get { return boolattribute(luarule, "iscomplete"); }
        }

        public bool newadd
        {
            get { return boolattribute(luarule, "newadd"); }
        }

        public int maxPoint
        {
            get { return attribute(luarule, "maxPoint"); }
            set { SetIntAtribute(luarule, "maxPoint", value); }
        }

        [CSharpCallLua]
        public delegate string getPlayRuleMethode(object obj, bool isShowMatchCount);

        public static getPlayRuleMethode getplayrule;

        public string getPlayRule(bool isShowMatchCount)
        {
            return getplayrule(luarule, isShowMatchCount);
        }

        [CSharpCallLua]
        public delegate int getBetMethode(object obj);

        public static getBetMethode getBetMeth;

        /// <summary>
        /// 一分对应的金币
        /// </summary>
        /// <returns></returns>
        public int getBet()
        {
            return getBetMeth(luarule);
        }

        [CSharpCallLua]
        public delegate int setBetMethode(object obj, int baseScore);

        public static setBetMethode setBetMeth;

        /// <summary>
        /// 一分对应的金币（设置底分）
        /// </summary>
        /// <returns></returns>
        public void setBet(int baseScore)
        {
            setBetMeth(luarule, baseScore);
        }


        [CSharpCallLua]
        public delegate int getChipMethode(object obj);

        public static getBetMethode getChipMeth;

        public long getChip()
        {
            return getChipMeth(luarule);
        }

        [CSharpCallLua]
        public delegate int getTaxesRateMethod(object obj);

        public static getTaxesRateMethod getTaxesRateMeth;

        [CSharpCallLua]
        public delegate int getPlatForm(object obj);

        public static getPlatForm getPlatform;

        /// <summary>
        /// 获取游戏平台
        /// </summary>
        /// <returns></returns>
        public int getPlatFormValue()
        {
            return getPlatform(luarule);
        }



        [CSharpCallLua]
        public delegate string[] getRuleSingleNames(object obj, int type);

        public static getRuleSingleNames getLuaRuleSingleNamesMethod;

        /// <summary>
        /// 获取规则单选项的描述数组
        /// </summary>
        /// <returns></returns>
        public string[] getLuaRuleSingleNames(int type)
        {
            return getLuaRuleSingleNamesMethod(luarule, type);
        }


        [CSharpCallLua]
        public delegate void setRuleSingleNames(object obj, int type, int index);

        public static setRuleSingleNames settLuaRuleSingleValueMethod;

        /// <summary>
        /// 设置规则单选项的值
        /// </summary>
        /// <returns></returns>
        public void setLuaRuleSingleNames(int type, int index)
        {
            settLuaRuleSingleValueMethod(luarule, type, index);
        }




        [CSharpCallLua]
        public delegate string getHelpUrlMethode(object obj);

        public static getHelpUrlMethode gethelpurl;

        public string getHelpUrl()
        {
            return gethelpurl(luarule) + "?" + MathKit.random(1, 10000);
        }

        [CSharpCallLua]
        public delegate void bytesReadLocalMethode(object obj, ByteBuffer data);

        public static bytesReadLocalMethode bytesreadlocal;

        public void bytesReadLocal(ByteBuffer data)
        {
            bytesreadlocal(luarule, data);
        }

        [CSharpCallLua]
        public delegate void saveMethode(object obj, string save_url);

        public static saveMethode save1;

        public void save(string save_url)
        {
            save1(luarule, save_url);
        }

        [CSharpCallLua]
        public delegate void setWanFanMethode(object obj, bool b, int index);

        public static setWanFanMethode setWanFan1;

        public void setWanFan(bool b, int index)
        {
            setWanFan1(luarule, b, index);
        }

        [CSharpCallLua]
        public delegate bool isSelectedMethode(object obj, int index);

        public static isSelectedMethode isSelected1;

        public bool isSelected(int index)
        {
            return isSelected1(luarule, index);
        }

        [CSharpCallLua]
        public delegate int[] getFanShuMethode(object obj);

        public static getFanShuMethode getFanShu1;

        public int[] getFanShu()
        {
            return getFanShu1(luarule);
        }

        [CSharpCallLua]
        public delegate int getTrusteeshipMethod(object obj);

        /// <summary>
        /// 托管方式
        /// </summary>
        public static getTrusteeshipMethod trusteeship;

        /// <summary>
        /// 获取托管方式
        /// </summary>
        /// <returns></returns>
        public int getTrusteeship()
        {
            return trusteeship(luarule);
        }

        [CSharpCallLua]
        public delegate int setTrusteeshipMethod(object obj, int type);

        /// <summary>
        /// 设置托管方式
        /// </summary>
        public static setTrusteeshipMethod settrusteeship;

        /// <summary>
        /// 设置托管方式
        /// </summary>
        /// <returns></returns>
        public void setTrusteeship(int type)
        {
            settrusteeship(luarule, type);
        }

        [CSharpCallLua]
        public delegate int[] getTrusteeshipTimeMethod(object obj);

        /// <summary>
        /// 托管时间数组
        /// </summary>
        public static getTrusteeshipTimeMethod trusteeshiptime;

        /// <summary>
        /// 获取托管时间数组
        /// </summary>
        /// <returns></returns>
        public int[] getTrusteeshipTimeArray()
        {
            return trusteeshiptime(luarule);
        }


        [CSharpCallLua]
        public delegate int getTrusteeTimeMethod(object obj);

        /// <summary>
        /// 托管时间
        /// </summary>
        public static getTrusteeTimeMethod trusteestime;

        /// <summary>
        /// 获取托管时间
        /// </summary>
        /// <returns></returns>
        public int getTrusteeTime()
        {
            return trusteestime(luarule);
        }

        [CSharpCallLua]
        public delegate int setTrusteeTimeMethod(object obj, int time);

        /// <summary>
        /// 设置托管时间
        /// </summary>
        public static setTrusteeTimeMethod settrusteestime;

        /// <summary>
        /// 设置托管时间
        /// </summary>
        /// <returns></returns>
        public void setTrusteeTime(int time)
        {
            settrusteestime(luarule, time);
        }

        [CSharpCallLua]
        public delegate int[] getTrusteejstatustypesMethod(object obj);

        /// <summary>
        /// 托管解散类型
        /// </summary>
        public static getTrusteejstatustypesMethod trusteejstatustypes;

        /// <summary>
        /// 获取托管解散类型
        /// </summary>
        /// <returns></returns>
        public int[] getTrusteejstatustypes()
        {
            return trusteejstatustypes(luarule);
        }

        [CSharpCallLua]
        public delegate int getTrusteejstatusMethod(object obj);

        /// <summary>
        /// 托管解散类型
        /// </summary>
        public static getTrusteejstatusMethod trusteejstatus;

        /// <summary>
        /// 获取托管解散类型
        /// </summary>
        /// <returns></returns>
        public int getTrusteejstatus()
        {
            return trusteejstatus(luarule);
        }

        [CSharpCallLua]
        public  delegate int setTrusteejstatusMethod(object obj, int type);

        /// <summary>
        /// 设置托管解散类型
        /// </summary>
        public static setTrusteejstatusMethod settrusteejstatus;

        /// <summary>
        /// 设置托管解散类型
        /// </summary>
        /// <returns></returns>
        public void setTrusteejstatus(int type)
        {
            settrusteejstatus(luarule, type);
        }


        /// <summary>
        /// 获取当前计数索引对应的牌类型，例如:第3个计数数组对应的是花牌
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int getCountsTypeFromIndex(int i)
        {
            return i;
        }




        [CSharpCallLua]
        public delegate bool getIpLimitMethod(object obj);

        /// <summary>
        /// 获取IP限制是否开启
        /// </summary>
        public static getIpLimitMethod getiplimit;

        /// <summary>
        /// 获取IP限制是否开启
        /// </summary>
        /// <returns></returns>
        public bool getIpLimit()
        {
            return getiplimit(luarule);
        }

        [CSharpCallLua]
        public delegate void setIpLimitMethod(object obj, bool b);

        /// <summary>
        /// 设置IP限制是否开启
        /// </summary>
        public static setIpLimitMethod setiplimit;

        /// <summary>
        /// 设置IP限制是否开启
        /// </summary>
        /// <returns></returns>
        public void setIpLimit(bool b)
        {
            setiplimit(luarule, b);
        }

        [CSharpCallLua]
        public delegate bool getGpsLimitMethod(object obj);

        /// <summary>
        /// 获取GPS限制是否开启
        /// </summary>
        public static getGpsLimitMethod getgpslimit;

        /// <summary>
        /// 获取GPS限制是否开启
        /// </summary>
        /// <returns></returns>
        public bool getGpsLimit()
        {
            return getgpslimit(luarule);
        }


        [CSharpCallLua]
        public delegate void setGpsLimitMethod(object obj, bool b);

        /// <summary>
        /// 设置GPS限制是否开启
        /// </summary>
        public static setGpsLimitMethod setgpslimit;

        /// <summary>
        /// 设置GPS限制是否开启
        /// </summary>
        /// <returns></returns>
        public void setGpsLimit(bool b)
        {
            setgpslimit(luarule, b);
        }


        [CSharpCallLua]
        public delegate int getGpsLimitDistanceMethod(object obj);

        /// <summary>
        /// 获取限制距离
        /// </summary>
        public static getGpsLimitDistanceMethod getgpslimitdistance;

        /// <summary>
        /// 获取限制距离
        /// </summary>
        /// <returns></returns>
        public int getGpsLimitDistance()
        {
            return getgpslimitdistance(luarule);
        }

        [CSharpCallLua]
        public delegate void setGpsLimitDistanceMethod(object obj, int distance);

        /// <summary>
        /// 设置限制距离
        /// </summary>
        public static setGpsLimitDistanceMethod setgpslimitdistance;

        /// <summary>
        /// 设置限制距离
        /// </summary>
        /// <returns></returns>
        public void setGpsLimitDistance(int distance)
        {
            setgpslimitdistance(luarule, distance);
        }

        [CSharpCallLua]
        public delegate int[] getLimittypesMethod(object obj);

        /// <summary>
        /// 获取限制距离数组
        /// </summary>
        public static getLimittypesMethod getlimittypes;

        /// <summary>
        /// 获取限制距离数组
        /// </summary>
        /// <returns></returns>
        public int[] getLimittypes()
        {
            return getlimittypes(luarule);
        }




        [CSharpCallLua]
        public delegate string[] getWanFaMethode(object obj);

        public static getWanFaMethode getWanFa1;

        public string[] getWanFa()
        {
            return getWanFa1(luarule);
        }


        [CSharpCallLua]
        public delegate bool isCheckedRuleMethode(object obj, int index);

        public static isCheckedRuleMethode isCheckedRule1;

        public bool isDianPaoRule(int index)
        {
            return isCheckedRule1(luarule, index);
        }

        [CSharpCallLua]
        public delegate string getTipsMethode(object obj);

        public static getTipsMethode getTips1;

        public string getTips()
        {
            return getTips1(luarule);
        }

        [CSharpCallLua]
        public delegate int[] getPlayerNumMethode(object obj);

        public static getPlayerNumMethode getPlayerNum1;

        public int[] getPlayerNum()
        {
            return getPlayerNum1(luarule);
        }


        [CSharpCallLua]
        public delegate int getRulePointMethode(object obj, int type);

        /// <summary>
        /// 获取指定类型的番数
        /// </summary>
        public static getRulePointMethode getRulePoint;

        /// <summary>
        /// 获取指定类型的番数
        /// </summary>
        public int getRuleTypePoint(int type)
        {
            return getRulePoint(luarule, type);
        }

        [CSharpCallLua]
        public delegate string[] getRuleInfoMethode(object obj);

        public static getRuleInfoMethode getRuleInfo1;

        public string[] getRuleInfo()
        {
            return getRuleInfo1(luarule);
        }

        [CSharpCallLua]
        public delegate void bytesReadRule(object obj, ByteBuffer data);

        public static bytesReadRule br;

        public void bytesRead(ByteBuffer data)
        {
            br(luarule, data);
        }

        [CSharpCallLua]
        public delegate void bytesWriteRule(object obj, ByteBuffer data);

        public static bytesWriteRule bw;

        public void bytesWrite(ByteBuffer data)
        {
            bw(luarule, data);
        }
    }
}
