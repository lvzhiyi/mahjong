using System;
using UnityEngine;
using XLua;

namespace cambrian.common
{
    /// <summary>
    /// 命令执行管理器
    /// </summary>
    [Hotfix]
    public class CommandManager : MonoBehaviour
    {
        /** 日志记录 */
        private static Logger log = LogFactory.getLogger<CommandManager>(true);

        static ArrayList<Port> commands = new ArrayList<Port>();

        //用于后台运行一段时间后，自动重连
        public static long time1;

        [CSharpCallLua]
        public static void addCommand(Port command, Action<object> func)
        {
            time1 = TimeKit.currentTimeMillis;
            command.callback = func;
            CommandManager.addCommand(command);
        }

        public static void addCommand(Port command)
        {
            if (log.isDebug)
                Debug.Log(log.getMessage(" ,addCommand  , " + command.id + " , " + command.GetType().FullName));
            time1 = TimeKit.currentTimeMillis;
            CommandManager.commands.add(command);
        }

        [CSharpCallLua]
        public Action<Port, Exception> onCommandException;

        void Update()
        {
            if (CommandManager.commands.count <= 0) return;
            Port[] commands = CommandManager.commands.removeAll();
            foreach (Port command in commands)
            {
                excuteCommand(command);
            }
        }
        void excuteCommand(Port command)
        {
            try
            {
                command.excute();
            }
            catch (Exception e)
            {
                if(log.isDebug)
                    Debug.LogError("e="+e.ToString());
                this.onCommandException(command, e);
            }
        }
    }
}