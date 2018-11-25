using System;
using System.Globalization;

namespace Gravity.Tools.DateTools
{
    public static class DateUtils
    {
        private static readonly PersianCalendar Pcal = new PersianCalendar();

        public static string ToPersianDate(this DateTime date)
        {
            var result = Pcal.GetYear(date) + "/" +
                         (Pcal.GetMonth(date) < 10
                             ? "0" + Pcal.GetMonth(date)
                             : Pcal.GetMonth(date).ToString()) + "/" +
                         (Pcal.GetDayOfMonth(date) < 10
                             ? "0" + Pcal.GetDayOfMonth(date)
                             : Pcal.GetDayOfMonth(date).ToString());
            return result;
        }

        public static DateTime ToPersianDate(this string date)
        {
            if (date.Length < 10) date = FixDateTo10Character(date);
            var persiandate = date.Split(Convert.ToChar("/"));
            if (persiandate.Length < 3) throw new Exception("Date Foramt invalid.");
            var perDate = new DateTime(persiandate[0].ToInt(), persiandate[1].ToInt(), persiandate[2].ToInt(), new PersianCalendar());
            return perDate;
        }

        public static int ToPersianYear(this DateTime date)
        {
            return Pcal.GetYear(date);
        }

        public static DateTime ToGregorianDate(this string date)
        {
            if (date.Length < 10) throw new Exception("Date Foramt invalid.");
            var persiandate = date.Split(Convert.ToChar("/"));
            if (persiandate.Length < 3) throw new Exception("Date Foramt invalid.");
            var gregDate = new DateTime(persiandate[0].ToInt(), persiandate[1].ToInt(), persiandate[2].ToInt(), new PersianCalendar());
            return gregDate;
        }

        public static string GetPersianDayName(this DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Friday: return "جمعه";
                case DayOfWeek.Monday: return "دوشنبه";
                case DayOfWeek.Saturday: return "شنبه";
                case DayOfWeek.Sunday: return "یکشنبه";
                case DayOfWeek.Thursday: return "پنج شنبه";
                case DayOfWeek.Wednesday: return "چهارشنبه";
                case DayOfWeek.Tuesday: return "سه شنبه";
            }
            return "";
        }

        public static string GetPersianDayName(this string date)
        {
            return date.Length != 10 ? "" : ToGregorianDate(date).ToPersianDayName();
        }

        public static int ToPersianDay(this DateTime date)
        {
            return Pcal.GetDayOfMonth(date);
        }

        public static int ToPersianDayOfWeek(this DateTime date)
        {
            return (int)Pcal.GetDayOfWeek(date) + 1;
        }

        public static string ToPersianDayName(this DateTime date)
        {
            return GetPersianDayName(Pcal.GetDayOfWeek(date));
        }

        public static int ToPersianMonth(this DateTime date)
        {
            return Pcal.GetMonth(date);
        }

        public static int ToPersianMonthLength(this DateTime date)
        {
            switch (Pcal.GetMonth(date))
            {
                case 1: return 31;
                case 2: return 31;
                case 3: return 31;
                case 4: return 31;
                case 5: return 31;
                case 6: return 31;
                case 7: return 30;
                case 8: return 30;
                case 9: return 30;
                case 10: return 30;
                case 11: return 30;
                case 12: return 30;
            }
            return 0;
        }

        public static string FixDateTo10Character(this string date)
        {
            if (date.Length != 6) return date;
            return "13" + date.Insert(2, "/").Insert(5, "/");
        }

        public static string DateTo6Character(this string date)
        {
            if (date.Length != 10) return date;
            return date.Replace("/", "").Substring(2, 6);
        }

        public static string ToPersianMonthName(this DateTime date)
        {
            return MonthName(Pcal.GetMonth(date));
        }

        private static string MonthName(int mon)
        {
            switch (mon)
            {
                case 1: return "فروردین";
                case 2: return "اردیبهست";
                case 3: return "خرداد";
                case 4: return "تیر";
                case 5: return "مرداد";
                case 6: return "شهریور";
                case 7: return "مهر";
                case 8: return "آبان";
                case 9: return "آذر";
                case 10: return "دی";
                case 11: return "بهمن";
                case 12: return "اسفند";
            }
            return "";
        }

        public static string GetTime(this DateTime inputtime)
        {
            var hour = inputtime.Hour;
            var minute = inputtime.Minute;
            var time = hour < 10 ? "0" + hour : hour.ToString();
            time += ":" + (minute < 10 ? "0" + minute : minute.ToString());
            return time;
        }

        public static string GetTimeWithSecond(this DateTime inputtime)
        {
            var hour = inputtime.Hour;
            var minute = inputtime.Minute;
            var second = inputtime.Second;
            var time = hour < 10 ? "0" + hour : hour.ToString();
            time += ":" + (minute < 10 ? "0" + minute : minute.ToString());
            time += ":" + (second < 10 ? "0" + second : second.ToString());
            return time;
        }
    }
}
