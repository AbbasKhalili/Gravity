using System;
using System.Globalization;

namespace Gravity.Tools.DateTools
{
    public static class DateUtils
    {
        public static string ToPersianDate(this DateTime date)
        {
            var pcal = new PersianCalendar();
            var result = pcal.GetYear(date) + "/" +
                         (pcal.GetMonth(date) < 10
                             ? "0" + pcal.GetMonth(date).ToString()
                             : pcal.GetMonth(date).ToString()) + "/" +
                         (pcal.GetDayOfMonth(date) < 10
                             ? "0" + pcal.GetDayOfMonth(date).ToString()
                             : pcal.GetDayOfMonth(date).ToString());
            return result;
        }

        public static DateTime ToGregorianDate(this string date)
        {
            var fdate = Convert.ToDateTime(date);
            var dt = new DateTime(fdate.Year, fdate.Month, fdate.Day, new PersianCalendar());
            return dt;
        }
    }
}
