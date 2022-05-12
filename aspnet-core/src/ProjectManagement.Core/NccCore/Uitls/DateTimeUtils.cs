using Abp.Timing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
namespace NccCore.Uitls
{
    public class DateTimeUtils
    {
        // All now function use Clock.Provider.Now
        public static DateTime GetNow()
        {
            return Clock.Provider.Now;
        }

        public static string ToString(DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy");
        }

        public static DateTime FirstDayOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime LastDayOfMonth(DateTime date)
        {
            return FirstDayOfMonth(date).AddMonths(1).AddDays(-1);
        }

        public static List<DateTime> GetListMonth(DateTime startDate, DateTime endDate)
        {
            var date = FirstDayOfMonth(startDate);
            var result = new List<DateTime>();
            while (date <= endDate)
            {
                result.Add(date);
                date = date.AddMonths(1);
            }
            return result;
        }

        public static string GetMonthName(DateTime date)
        {
            return date.ToString("MM-yyyy");
        }
        public static string GetFullNameOfMonth(int month)
        {
            return CultureInfo.CurrentCulture.
                DateTimeFormat.GetMonthName
                (month);
        }

        public static string FormatDateToInvoice(DateTime date)
        {
            var s = date.ToString("D", CultureInfo.GetCultureInfo("en-US"));
            return s.Substring(s.IndexOf(",") + 2);
        }

        public static long DateDiff(DateTime date1, DateTime date2)
        {
            return date1.Ticks - date2.Ticks;
        }

        public static long DateDiffAbs(DateTime date1, DateTime date2)
        {
            return Math.Abs(DateDiff(date1, date2));
        }

        public static DateTime MinDate(DateTime date1, DateTime date2)
        {
            return date1.Ticks < date2.Ticks ? date1 : date2; ;
        }



    }
}
