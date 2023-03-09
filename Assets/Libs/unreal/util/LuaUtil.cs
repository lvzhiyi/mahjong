using basef.rule;
using XLua;

[Hotfix]
public class LuaUtil
{
    public static LuaEnv luaEnv;

    public static void update()
    {
        if (luaEnv != null)
            luaEnv.Tick();
    }

    public static void instance()
    {
        if (luaEnv != null)
        {
            a();
        }

        luaEnv = new LuaEnv();
    }

    public static void onDestroy()
    {
        if (luaEnv != null)
        {
            RuleManager.clear();
            clearDelegate();
            a();
        }
    }

    public static void a()
    {
        if (luaEnv != null)
            b();
    }

    public static void b()
    {
        if (luaEnv != null)
        {
            luaEnv.Dispose();
            luaEnv = null;
        }
    }

    public  static void clearDelegate()
    {
//        for (int i = 0; i < 10; i++)
//        {
            Rule.clearDelegate();
//        }
        
    }
}
