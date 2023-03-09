using cambrian.common;

namespace cambrian.game
{
    /// <summary>
    /// 用户的登录设备
    /// </summary>
    public class UserDevice
    {
        public static UserDevice device = new UserDevice();

        /** 设备类型 */
        public int type;
        /** 设备OS版本 */
        public string osVersion;
        /** 设备厂商 */
        public string manufacturer;
        /** 设备型号 */
        public string devModel;

        private UserDevice()
        {
            this.type = Platform.getPlatFormType();
            this.osVersion = WXManager.getInstance().getSystemVersion();
            this.manufacturer = WXManager.getInstance().getDeviceBrand();
            this.devModel = WXManager.getInstance().getSystemModel();
        }


        public void bytesWrite(ByteBuffer data)
        {
            data.writeInt(type);
            data.writeUTF(this.osVersion);
            data.writeUTF(this.manufacturer);
            data.writeUTF(this.devModel);
        }

        public override string ToString()
        {
            return "UserDevice [osVersion=" + this.osVersion + ", manufacturer=" + this.manufacturer + ", devModel=" + this.devModel + ", type=" + this.type + "]";
        }
    }
}
