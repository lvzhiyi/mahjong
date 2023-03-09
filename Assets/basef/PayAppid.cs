namespace basef
{
    public class PayAppid
    {
        /// <summary>
        /// 兄弟长牌
        /// </summary>
        public const int XDCP_PAY_XDCP_APPID = 1;
        /// <summary>
        /// 大熊猫长牌的appid
        /// </summary>
        public const int XDCP_PAY_DXM_CP_APPID = 2;

        public static int getPayAppid()
        {
            return XDCP_PAY_DXM_CP_APPID;
        }
    }
}
