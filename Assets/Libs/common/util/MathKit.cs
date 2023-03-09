using System;
using System.Text;
using UnityEngine;

namespace cambrian.common
{
    /// <summary>
    /// 常用方法
    /// </summary>
	public class MathKit
	{
        /* static fields */

        private static System.Random _random = new System.Random();
		/* static methods */
        /// <summary>
        /// 返回一个指定范围内的随机数。[min,max)
        /// </summary>
        /// <param name="min">返回的随机数的下界（随机数可取该下界值）。</param>
        /// <param name="max">返回的随机数的上界（随机数不能取该上界值）。maxValue 必须大于等于 minValue。</param>
        /// <returns></returns>
        public static int random(int min,int max)
        {
            return _random.Next(min,max);
        }

        /// <summary>
        /// 两点距离
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static int distance(int x1, int y1, int x2, int y2)
        {
            return abs(x1 - x2) + abs(y1 - y2);
        }
        /// <summary>
        /// 矩形相交
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="width1"></param>
        /// <param name="height1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="width2"></param>
        /// <param name="height2"></param>
        /// <returns></returns>
        public static bool isRect(float x1,float y1,float width1,float height1,float x2,float y2,float width2,float height2)
        {
            Rect rect1 = new Rect(x1,y1,width1,height1);
            Rect rect2 = new Rect(x2,y2,width2,height2);
            Vector2 point = new Vector2(x2,y2);//左上角
            for (int i = 0; i < 4;i++)
            {
                if (rect1.Contains(point))
                    return true;
                if (i == 0)
                    point = new Vector2(x2, y2 + height2);
                else if (i == 1)
                    point = new Vector2(x2 + width2, y2);
                else if (i == 2)
                    point = new Vector2(x2+width2,y2+width2);
            }
            point = new Vector2(x1,y1);
            for (int i = 0; i < 4; i++)
            {

                if (rect2.Contains(point))
                    return true;
                if (i == 0)
                    point = new Vector2(x1, y1 + height1);
                else if (i == 1)
                    point = new Vector2(x1 + width1, y1);
                else if (i == 2)
                    point = new Vector2(x1 + width1, y1 + width1);
            }
            return false;
        }

        /// <summary> 根据余弦值求角度 </summary>
        public static float sin(float f)
        {
            return Mathf.Sin(f * Mathf.Deg2Rad);
        }

        /// <summary> 根据余弦值求角度 </summary>
        public static float Asin(float f)
        {
            return Mathf.Asin(f) * Mathf.Rad2Deg;
        }

        /// <summary> 根据角度求余弦值 </summary>
        public static float cos(float f)
        {
            return Mathf.Cos(f * Mathf.Deg2Rad);
        }
        /// <summary> 根据余弦值求角度 </summary>
        public static float Acos(float f)
        {
            return Mathf.Acos(f) * Mathf.Rad2Deg;
        }
        /// <summary>
        /// 绝对值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int abs(int value)
        {
           return Mathf.Abs(value);
        }
        /// <summary>
        /// 绝对值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float abs(float value)
        {
            return Mathf.Abs(value);
        }

        public static long abs(long value)
        {
            return (long)Mathf.Abs(value);
        }

        /// <summary>
        /// 四舍五入
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int round(float value)
        {
            return (int)Mathf.Round(value);
        }
        /// <summary>
        /// 大于等于该数字的最接近的整数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ceil(float value)
        {
            return (int)Mathf.Ceil(value);
        }
        /// <summary>
        /// 小于等于指定数字的最接近的整数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int floor(float value)
        {
            return (int)Mathf.Floor(value);
        }


        /// <summary>
        /// 余数(正)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="space"></param>
        /// <returns></returns>
        public static int mod(int value, int space)
        {
            value %= space;
            if (value < 0) value += space;
            return value;
        }

        private const double EARTH_RADIUS = 6371137;

        /// <summary>  
        /// 计算两个经纬度之间的距离
        /// 计算两点位置的距离，返回两点的距离，单位 米  
        /// 该公式为GOOGLE提供，误差小于0.2米  
        /// </summary>  
        /// <param name="lat1">第一点纬度</param>  
        /// <param name="lng1">第一点经度</param>  
        /// <param name="lat2">第二点纬度</param>  
        /// <param name="lng2">第二点经度</param>  
        /// <returns></returns>  
        public static double GetGPSDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = Rad(lat1);
            double radLng1 = Rad(lng1);
            double radLat2 = Rad(lat2);
            double radLng2 = Rad(lng2);
            double a = radLat1 - radLat2;
            double b = radLng1 - radLng2;
            double result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2))) * EARTH_RADIUS;
            return result;
        }
        /// <summary>  
        /// 经纬度转化成弧度  
        /// </summary>  
        /// <param name="d"></param>  
        /// <returns></returns>  
        private static double Rad(double d)
        {
            return (double)d * Math.PI / 180d;
        }

        /// <summary>
        /// 获取距离，大于1000时，显示km
        /// </summary>
        /// <returns></returns>
        public static string getDistance(double value)
        {
            if (value < 0) return "0m";
            if (value>1000)
            {
                value = value / 1000;
                value = Math.Round(value, 2);
                return value +"km";
            }
            else
            {
                value = Math.Round(value);
                return value + "m";
            }
        }

        /// <summary>
        /// 转换成千，万
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
	    public static string getDecimal(long number)
	    {
            long temp = Math.Abs(number);//取绝对值
	        if (temp < 100000) return number.ToString();
            if (temp >= 100000000)
                return number / 100000000 + (number / 10000000 % 100 * 0.01) + "亿";//大于1亿保留到百万位
            if (temp > 10000000)
                return number / 10000 + (number / 1000 % 10 * 0.1) + "万";//大于1000W保留到千位
            if (temp >= 100000)
                return number / 10000 + (number / 100 % 100 * 0.01) + "万";//大于1W 保留到百位
            return "";
	    }


        public static string getRound2LongStr(long number)
        {
            return getRound2Long(number).ToString();
        }


        /// <summary>
        /// 保留两位小数
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static double getRound2Long(long number)
        {
            return Math.Round(number / 100f, 2);
        }

        /// <summary>
        /// 保留两位小数
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static double getRound2Double(double number)
        {
            return Math.Round(number / 100f, 2);
        }


        /// <summary>
        /// 保留两位小数
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static double getRound2Int(int number)
        {
            return Math.Round(number/100f,2);
        }

        /// <summary>
        /// 放大100倍
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int getIntEnlarge100(double number,int size=100)
        {
            return (int)(number * size);
        }

        /// <summary>
        /// 放大100倍
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static long getLongEnlarge100(double number, int size = 100)
        {
            return (long)(number * size);
        }

        /* fields */


        /* constructors */

        /* properties */

        /* init start */

        /* methods */

        /* common methods */

        /* inner class */
    }
}
