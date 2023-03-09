using System;
using UnityEngine;

namespace cambrian.common
{
    public class TimeKit
    {
        /* static fields */
        /// <summary> 年、月、日单位时间（秒） </summary>
        public const int DAY = 86400, HOUR = 3600, MIN = 60;
        /// <summary> 年、月、日单位时间（毫秒） </summary>
        public const long DAY_MILLS = DAY * 1000L, HOUR_MILLS = HOUR * 1000L, MIN_MILLS = MIN * 1000L, SECOND_MILLS = 1000L, TWO_SECOND_MILLS = 2000L;
        /** 格林威治时间UTC参照点：1970年1月1日0时0分0秒 */
        public static readonly DateTime greenwich = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /** 格林威治时间UTC参照点：1970年1月1日0时0分0秒 */
        public static readonly DateTime greenwich1 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).ToLocalTime();

        /// <summary>  </summary>
        public static long _remoteCurrentTimeMillisDistance=-1;

        /* static methods */

        /// <summary> 服务器端的当前时间毫秒数,-1时为未取得服务器的时间 </summary>
        public static long currentTimeMillis
        {
            get
            {
                if (_remoteCurrentTimeMillisDistance == -1) return -1;
                return _remoteCurrentTimeMillisDistance + (int)(Time.realtimeSinceStartup * 1000);
            }
            set
            {
                _remoteCurrentTimeMillisDistance = value - (int)(Time.realtimeSinceStartup * 1000);
            }
        }

        public static string format(int time)
        {
            return TimeKit.format(time * 1000L, "g");
        }


        public static long getTodayStart()
        {
            DateTime t = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            long time = (t.ToUniversalTime().Ticks - greenwich.Ticks) / 10000;
            return time;
        }

        public static long getTodayServerStart()
        {
            long ticks = greenwich.Ticks + currentTimeMillis * 10000L;
            DateTime dt = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(ticks, DateTimeKind.Utc));
            DateTime t = Convert.ToDateTime(dt.ToString("yyyy-MM-dd 00:00:00"));
            long time = (t.ToUniversalTime().Ticks - greenwich.Ticks) / 10000;
            return time;
        }

        public static bool isToday(long time1,long time2)
        {

            long ticks = greenwich.Ticks + time1 * 10000L;
            DateTime dt1 = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(ticks, DateTimeKind.Utc));

            ticks = greenwich.Ticks + time2 * 10000L;
            DateTime dt2 = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(ticks, DateTimeKind.Utc));

            if (dt1.Date == dt2.Date) return true;

            return false;
        }



        /// <summary>
        /// 通过指定时间获取DateTime
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime getDateTime(long time)
        {
            long ticks = greenwich1.Ticks + time * 10000;
            return new DateTime(ticks,DateTimeKind.Utc);
        }

        public static long getTime(DateTime time)
        {
            return (time.Ticks - greenwich1.Ticks) / 10000;
        }

        public static string format(long time)
        {
            return TimeKit.format(time, "g");
        }
        /// <summary>
        /// 获取时间(s)的字符串表示
        /// </summary>
        /// <param name="time">时间(s)</param>
        /// <param name="format">格式</param>
        /// <returns></returns>
        public static string format(int time, string format)
        {
            return TimeKit.format(time*1000L,format);
        }
        /// <summary> 格式化时间，参数：格林威治时间，格式化格式（具体见文件末尾） </summary>
        public static string format(long time, string format)
        {
            if (format == null) format = "g";
            long ticks = greenwich.Ticks + time * 10000L;
            DateTime dt = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(ticks, DateTimeKind.Utc));
            return dt.ToString(format);
        }
        /// <summary>
        /// 获取时间倒计时字符串表示(ms)（例如：01:59:08）
        /// </summary>
        /// <param name="time">ms</param>
        /// <returns></returns>
        public static string getCountdown(long time)
        {
            return getCountdown((int)(time/1000));
        }

        public static DateTime[] getDateTimesArray(DateTime time)
        {
            DateTime[] dates = new DateTime[42];
            int days = DateTime.DaysInMonth(time.Year, time.Month);
            DateTime firstDay = new DateTime(time.Year, time.Month, 1);
            int weekDay = (int)firstDay.DayOfWeek;
            int lastDays = weekDay == 0 ? 7 : weekDay;
            int index = 0;
            DateTime temp;
            for (int i = weekDay; i > 0; i--)
            {
                temp = firstDay.AddDays(-i);
                dates[index++] = temp;
            }
            dates[index++] = firstDay;
            for (int i = 1; i < 42 - lastDays; i++)
            {
                temp = firstDay.AddDays(i);
                dates[index++] = temp;
            }
            return dates;
        }

        public static string getCurrTime()
        {
            return format(currentTimeMillis, "yyyy/MM/dd HH:mm:ss");
        }

        /// <summary>
        /// 获取时间倒计时字符串表示(s)（例如：01:59:08）
        /// </summary>
        /// <param name="time">s</param>
        /// <returns></returns>
        public static string getCountdown(int time)
        {
            if (time <= 0) return "00:00:00";
            int hour = time / HOUR;
            int min = (time % HOUR) / MIN;
            int sec = time % MIN;
            return getCountdown(hour, min, sec);
        }
        /// <summary>
        /// 多少时间前
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string getPreHumanityTime(long time)
        {
            if (time <= 0) return "--";
            time = currentTimeMillis - time;
            if (time <= MIN_MILLS) return "";
            long day = time/DAY_MILLS;
            time %= DAY_MILLS;
            long hour = time / HOUR_MILLS;
            time %= HOUR_MILLS;
            long min = time / MIN_MILLS;
            CharBuffer buff = new CharBuffer();
            if (day > 0) buff.append(day).append("天 ");
            if (day > 0||hour > 0) buff.append(hour).append("小时 ");
            if (day > 0||hour > 0||min > 0) buff.append(min).append("分前");;
            return buff.getString();
        }
        /// <summary> 获取时间倒计时字符串表示（例如：01:59:08） </summary>
        public static string getCountdown(int hour, int min, int sec)
        {
            if (hour < 0) hour = 0;
            if (min < 0) min = 0;
            if (sec < 0) sec = 0;
            CharBuffer buff=new CharBuffer();
            if(hour < 10) buff.append('0');
            buff.append(hour).append(':');
            if (min < 10) buff.append('0');
            buff.append(min).append(':');
            if (sec < 10) buff.append('0');
            buff.append(sec);
            return buff.getString();
        }
    }
}

/*
  format参数格式详细用法:
  1.标准格式代表字符
    格式字符    关联属性/说明
    d         短日期 (样式示例：08/30/2006)
    D         长日期 (样式示例：Wednesday, 30 August 2006)
    f         完整日期和时间（长日期和短时间） (样式示例：Wednesday, 30 August 2006 23:21)
    F         完整日期和时间（长日期和长时间） (样式示例：Wednesday, 30 August 2006 23:22:02)
    g         常规（短日期和短时间） (样式示例：08/30/2006 23:22)
    G         常规（短日期和长时间） (样式示例：08/30/2006 23:23:11)
    m,M       MonthDayPattern
    r,R       RFC1123Pattern
    s         使用当地时间的 SortableDateTimePattern（基于 ISO 8601）
    t         短时间（无秒） (样式示例：23:24
    T         长时间（带秒） (样式示例：23:24:30
    u         使用通用时间的完整日期和时间（长日期和长时间） (样式示例：2006-08-30 23:25:10Z)
    U         使用通用时间的完整日期和时间（长日期和长时间） (样式示例：Wednesday, 30 August 2006 15:25:37)
    y,Y       YearMonthPattern
  
  2.自定义格式符号
    下表列出了可被合并以构造自定义模式的模式。这些模式是区分大小写的；例如，识别“MM”，但不识别“mm”。
    如果自定义模式包含空白字符或用单引号括起来的字符，则输出字符串页也将包含这些字符。
    未定义为格式模式的一部分或未定义为格式字符的字符按其原义复制。
    格式符号    说明
    d         月中的某一天。一位数的日期没有前导零。
    dd        月中的某一天。一位数的日期有一个前导零。
    ddd       周中某天的缩写名称，在 AbbreviatedDayNames 中定义。
    dddd      周中某天的完整名称，在 DayNames 中定义。
    M         月份数字。一位数的月份没有前导零。
    MM        月份数字。一位数的月份有一个前导零。
    MMM       月份的缩写名称，在 AbbreviatedMonthNames 中定义。
    MMMM      月份的完整名称，在 MonthNames 中定义。
    y         不含纪元的年份，且无前导零。（例如：2008年，纪元为20，非纪元年份为08，显示8）
    yy        不含纪元的年份。且有前导零。（例如：2008年，纪元为20，非纪元年份为08，显示08）
    yyyy      包含纪元的四位数年份。（例如：2008年，纪元为20，非纪元年份为08，显示2008）
    gg        时期或纪元。如果要设置格式的日期不具有关联的时期或纪元字符串，则忽略该模式。
    h         12 小时制的小时。一位数的小时数无前导零。
    hh        12 小时制的小时。一位数的小时数有前导零。
    H         24 小时制的小时。一位数的小时数无前导零。
    HH        24 小时制的小时。一位数的小时数有前导零。
    m         分钟。一位数的分钟数无前导零。
    mm        分钟。一位数的分钟数有前导零。
    s         秒。一位数的秒数无前导零。
    ss        秒。一位数的秒数有前导零。
    f         秒的小数精度为一位。其余数字被截断。
    ff        秒的小数精度为两位。其余数字被截断。
    fff       秒的小数精度为三位。其余数字被截断。
    ffff      秒的小数精度为四位。其余数字被截断。
    fffff     秒的小数精度为五位。其余数字被截断。
    ffffff    秒的小数精度为六位。其余数字被截断。
    fffffff   秒的小数精度为七位。其余数字被截断。
    t         在 AMDesignator 或 PMDesignator 中定义的 AM/PM 指示项的第一个字符（如果存在）。
    tt        在 AMDesignator 或 PMDesignator 中定义的 AM/PM 指示项（如果存在）。
    z         时区偏移量（“+”或“-”后面仅跟小时）。一位数的小时数无前导零。例如，太平洋标准时间是“-8”。
    zz        时区偏移量（“+”或“-”后面仅跟小时）。一位数的小时数有前导零。例如，太平洋标准时间是“-08”。
    zzz       时区偏移量（“+”或“-”后面跟有小时和分钟）。一位数的小时数和分钟数有前导零。例如，太平洋标准时间是“-08:00”。
    :         在 TimeSeparator 中定义的默认时间分隔符。
    /         在 DateSeparator 中定义的默认日期分隔符。

  3.特殊情况注意：
    只有2中列出的格式符号才能用于创建自定义模式；在1中列出的标准格式字符不能用于创建自定义模式。
    1中定义的标准格式简称使用时都是一个字符，2中自定义模式使用时至少需要两个字符；
    若使用自定义格式符号为一个字符的为避免和标准格式简称冲突，需要在前面加上“%”
    例如:
    format(time,"d");       此时的“d”是标准格式简称，返回1中定义的短日期模式。
    format(time,"%d");      此时的“d”是自定义符号，“%”指定自定义模式，返回月中的某天。
    format(time,"d ");      此时的“d”是自定义符号，字符数大于等于2，返回后面跟有一个空白字符的月中的某天。
    2中的格式符号可以随意组合
    例如:
    format(time,"yyyy年MM月")
    format(time,"yyyy/MM/dd HH:mm:ss") 
*/
