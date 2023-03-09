namespace cambrian.game
{
    /// <summary>
    /// 平台:android,ios,pc
    /// </summary>
    public class Platform
    {
        public const int Android = 2, IOS = 1, PC = 3;

        public static int getPlatFormType()
        {
#if UNITY_EDITOR
            return PC;
#elif UNITY_ANDROID
        return Android;
#elif UNITY_IOS
        return IOS;
#else
        return PC;
#endif
        }

        public static bool isOpenGuest()
        {
            return false;
        }

    }
}
