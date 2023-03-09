namespace Framework.Tool
{
    using System.IO;
    using System.Text;
    using UnityEditor;
    using UnityEngine;

    /*  hint
    *   const string no file path '/', attention please !
    */

    /// <summary> 生成文件信息 </summary>
    public class ScriptChange : AssetModificationProcessor
    {
        /// <summary> 文件路径加文件名 </summary>
        private static StringBuilder FileName = new StringBuilder();

        /// <summary> 游戏文件夹路径 </summary>
        private const string HeaderFile = "Assets";

        /// <summary>  
        /// 此函数在Asset被创建完，文件已经生成到磁盘上，但是没有生成.meta文件和import之前被调用  
        /// </summary>
        public static void OnWillCreateAsset(string newFileMeta)
        {
            var newFilePath = newFileMeta.Replace(".meta", "");

            if (Path.GetExtension(newFilePath) != ".cs") { newFilePath = null; return; }

            FileName = new StringBuilder();
            FileName.Append(newFilePath);

            var realPath = GetRealPath();

            File.WriteAllText(realPath, SetScriptContent(File.ReadAllText(realPath, Encoding.UTF8)).ToString(), Encoding.UTF8);

            newFilePath = realPath = null;
        }

        /// <summary> 获取真正的文件路径 </summary>
        private static string GetRealPath()
        {
#if UNITY_EDITOR || UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            return Application.dataPath.Replace(HeaderFile, "") + FileName.ToString();
#elif UNITY_ANDROID
     return    "";
#elif UNITY_IOS
     return    "";
#endif //注意，Application.datapath会根据使用平台不同而不同
        }

        /// <summary> 作者名 </summary>
        public static string AUTHOR = "XiNan";

        /// <summary> 邮箱 </summary>
        private const string EMAIL = "1398581458@qq.com";

        /// <summary> 这里实现自定义的一些规则 </summary>
        private static StringBuilder SetScriptContent(string content)
        {                                       
            string times = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var str = new StringBuilder().Append(content);
            str.Replace("#TEAR#", times.Substring(0, 4));                                                   //时间  年
            str.Replace("#DATE#", times.Substring(0, 10));                                                  //时间日期
            str.Replace("#NOWTIME#", times.Substring(11, 8));                                               //详细时间
            str.Replace("#NAMESPACE#", GetFileNameSpace(FileName).ToString());                              //命名空间
            str.Replace("#SCRIPTFULLNAME#", Path.GetFileName(FileName.Replace(".meta", "").ToString()));    //脚本名称
            str.Replace("#COMPANY#", PlayerSettings.companyName);                                           //游戏名称
            str.Replace("#AUTHOR#", AUTHOR);                                                                //编辑作者
            str.Replace("#VERSION#", PlayerSettings.bundleVersion);                                         //脚本版本
            str.Replace("#UNITYVERSION#", Application.unityVersion);                                        //UNITY版本
            str.Replace("#EMAIL#", EMAIL);                                                                  //邮箱

            times = null;
            return str;
        }

        /// <summary> 获取文件命名空间 </summary>
        private static StringBuilder GetFileNameSpace(StringBuilder filename)
        {
            filename = FileName.Replace('/' + Path.GetFileName(FileName.ToString()), "");
            filename.Replace(HeaderFile + '/', "");
            filename.Replace("Editor" + '/', "");
            return filename.Replace('/', '.');
        }
    }
}
