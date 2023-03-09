using System.IO;

namespace scene.game
{
    public class LuaLoadModule : Module
    {
        private string name = "luas";

        public string getPropertyName()
        {
            return name;
        }

        public void loadBugFix(string[] name)
        {
            if (name == null) return;
            string path = CacheLocalPath.AB_RESROUCE_PATH;
            for (int i = 0; i < name.Length; i++)
            {
                string luapath = path + name[i];
                LuaUtil.luaEnv.AddLoader((ref string filepath) =>
                {
                    if (File.Exists(filepath))
                    {
                        byte[] data = File.ReadAllBytes(filepath);
                        return data;
                    }
                    else
                    {
                        return null;
                    }
                });
                LuaUtil.luaEnv.DoString(@"
                require '" + luapath + "'");
            }
        }
    }
}
