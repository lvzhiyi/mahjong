using System;

namespace cambrian.common
{

    public class UnitKit
    {
        /// <summary>
        ///  价格转换为字符串显示
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static String toString(long price)
        {
            long v = price % 100;
            if (v < 10)
            {
                return (price / 100) + ".0" + v;
            }
            else
            {
                return (price / 100) + "." + v;
            }
        }

        /// <summary>
        /// 将数字转换成int数组（10000已内,不包含10000）,后面做成递归，
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static long[] intToInts(long number)
        {
            if (number > 10000 || number < -10000) return null;
            if (number == 0) return new long[1]{0};
            if (number < 0) number = MathKit.abs(number);
            ArrayList<long> list=new ArrayList<long>();
            long value = number/1000;
            if (value > 0)
            {
                list.add(value);
                number -= (value * 1000);
            }

            value = number / 100;
            if (value > 0)
            {
                list.add(value);
                number -= (value * 100);
            }
            else if (value == 0 && list.count > 0)
                list.add(0);

            value = number / 10;
            if (value > 0)
            {
                list.add(value);
                number -= (value * 10);
            }
            else if (value == 0&&list.count>0)
                list.add(0);

            list.add(number);

            return list.toArray();
        }
    }
}