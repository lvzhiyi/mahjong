namespace platform.poker
{
    public class PKCancelType
    {
        /// <summary> 斗地主取消类型 : 错误 </summary>
        public const int Error = 0;

        /// <summary> 斗地主取消类型 : 叫地主 </summary>
        public const int Landlord_Call_NO = 1;

        /// <summary> 斗地主取消类型 : 抢地主 </summary>
        public const int Landlord_Grab_NO = 2;

        /// <summary> 斗地主取消类型 : 加倍 </summary>
        public const int JIABEI_NO = 3;

        /// <summary> 斗地主取消类型 : 取消 </summary>
        public const int PASS = 4;

        /// <summary> 斗地主取消类型 : 叫分取消 </summary>
        public const int CALLSCORE = 5;
    }
}
