using UnityEngine;
using UnityEngine.UI;

namespace cambrian.common
{
    /// <summary>
    /// 颜色
    /// </summary>
    public class ColorKit
    {
        public static Color Color_title = getColor(0xf5, 0xa6, 0x23);
        public static Color Color_enable = getColor(0x7e, 0xd3, 0x21);
        public static Color Color_unable = getColor(0xd0, 0x02, 0x1b);

        /// <summary>
        /// 颜色
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <returns></returns>
        public static Color getColor(int red, int green, int blue)
        {
            return getColor(red, green, blue, 255);
        }
        /// <summary>
        /// 带透明度的颜色
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public static Color getColor(int red, int green, int blue, int alpha)
        {
            return new Color(red / 255f, green / 255f, blue / 255f, alpha / 255f);
        }
        /// <summary>
        /// 设置图片透明度 eg:ColorKit.setAlpha(bar.bar_bg, 0.5f + (i % 2) * 0.5f);
        /// </summary>
        /// <param name="image"></param>
        /// <param name="alpha"></param>
        public static void setAlpha(Image image, float alpha)
        {
            if (image == null) return;
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
        /// <summary>
        /// 所在图片的颜色
        /// </summary>
        /// <param name="image"></param>
        /// <param name="r">[0,1]</param>
        /// <param name="g">[0,1]</param>
        /// <param name="b">[0,1]</param>
        public static void setColor(Image image, float r, float g, float b)
        {
            if (image == null) return;
            Color color = image.color;
            color.r = r;
            color.g = g;
            color.b = b;
            image.color = color;
        }
        /// <summary>
        /// 设置所在图片的颜色
        /// </summary>
        /// <param name="image"></param>
        /// <param name="color"></param>
        public static void setColor(Image image, Color color)
        {
            if (image == null) return;
            image.color = color;
        }

        /// <summary>
        /// 将颜色对象转换为#FFFFFFFF格式的16进制字符串
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string toHex(Color32 color)
        {
            CharBuffer cb = new CharBuffer("#");
            // RGBA 顺序不可改
            HexKit.toHex(color.r, cb);
            HexKit.toHex(color.g, cb);
            HexKit.toHex(color.b, cb);
            HexKit.toHex(color.a, cb);
            return cb.getString();
        }

        /// <summary>
        /// 将颜色对象转换为 #FFFFFFFF 格式的16进制字符串
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string toHex(int red, int green, int blue, int alpha)
        {
            CharBuffer cb = new CharBuffer("#");
            // RGBA 顺序不可改
            HexKit.toHex((byte) red, cb);
            HexKit.toHex((byte) green, cb);
            HexKit.toHex((byte) blue, cb);
            HexKit.toHex((byte) alpha, cb);
            return cb.getString();
        }

        /// <summary>
        /// 通过 #FFFFFFFF 格式的 16 进制获得颜色对象
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static Color32 getColor(string hex)
        {
            int v = (int) HexKit.parseLong(hex, 1, hex.Length - 1);
            if (v > 0xFFFFFF) //带Alpha值
                return new Color32((byte) (v >> 24), (byte) (v >> 16), (byte) (v >> 8), (byte) v);
            else //带Alpha值
                return new Color32((byte) (v >> 16), (byte) (v >> 8), (byte) v,255);
        }
    }
}