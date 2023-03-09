using XLua;

namespace basef.rule
{
    public class RuleManager
    {
        public static RuleManager manager { get; set; }

        public static string name1;

        public static void instance(string name)
        {
            manager=new RuleManager(name);
            name1 = name;
        }

        private RuleManager(string name)
        {
            LuaUtil.luaEnv.Global.Get(name,out newSample);
        }

        [CSharpCallLua]
        public delegate Rule newRule(int sid);

        public newRule newSample;

        public static void clear()
        {
            if (manager != null)
            {
                newRule b = LuaUtil.luaEnv.Global.Get<newRule>(name1);
                b = null;
                manager.newSample = b;
                LuaUtil.luaEnv.Global.Set(name1,b);
            }
            
        }
    }
}
