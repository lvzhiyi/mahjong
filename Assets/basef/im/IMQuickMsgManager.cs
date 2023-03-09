using cambrian.game;
using platform;
using System;
using XLua;

namespace basef.im
{
    public class IMQuickMsgManager
    {
        public static string[] textinfo;
        /// <summary>
        /// 游戏类型
        /// </summary>
        public static int game_type;
        /// <summary>
        /// 接收快捷方式回调
        /// </summary>
        [CSharpCallLua]
        public static Action<IMQuickMsg> quickback;
        /// <summary>
        /// 接收文本回调
        /// </summary>
        [CSharpCallLua]
        public static Action<IMTextMsg> textback;

        public static void executeMsg(IMQuickMsg msg)
        {
            if (msg.type == IMQuickMsg.TYPE_TEXT)
            {
                msg.info = textinfo[msg.value];
                if (game_type == GamePlatform.CP_GAME)
                    SoundManager.playPhrase(msg.sex, msg.value);
                else if(game_type == GamePlatform.MJ_GAME)
                    SoundManager.playPhrase(msg.sex, msg.value);
                else if (game_type == GamePlatform.PK_GAME)
                    SoundManager.playPhrase(msg.sex, msg.value);
            }

            if (quickback != null)
            {
                quickback(msg);
            }
        }


        public static void showTextMsg(IMTextMsg msg)
        {
            if (textback != null)
                textback(msg);
        }
    }
}
