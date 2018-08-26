// MIT License
// 
// Copyright (c) 2018 Jan Steffen
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
using System.Globalization;

namespace System
{
    /// <summary>
    /// Extensions for the DateTime class
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Get week with year
        /// </summary>
        /// <param name="t"></param>
        /// <param name="format">
        ///     cc - week
        ///     yyyy - year
        /// </param>
        /// <returns></returns>
        public static string GetWeekWithYear(this DateTime t, string format)
        {
            return format.Replace("yyyy", GetIso8601Year(t).ToString()).Replace("cc", GetIso8601WeekOfYear(t).ToString("00"));
        }

        /// <summary>
        /// Get week of year
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetIso8601WeekOfYear(this DateTime date)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            var day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(date);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                date = date.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        /// <summary>
        /// Returns the year
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetIso8601Year(this DateTime date)
        {
            var week = GetIso8601WeekOfYear(date);

            if (week > 50 && date.Month == 1)
            {
                return date.Year - 1;
            }

            if (week < 2 && date.Month == 12)
            {
                return date.Year + 1;
            }

            return date.Year;
        }

        /// <summary>
        /// Returns the first day of the week that the specified
        /// date is in using the current culture.
        /// </summary>
        public static DateTime GetFirstDayOfWeek(this DateTime dayInWeek)
        {
            var defaultCultureInfo = CultureInfo.CurrentCulture;
            var firstDay = defaultCultureInfo.DateTimeFormat.FirstDayOfWeek;
            var firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);
            return firstDayInWeek;
        }

        /// <summary>
        /// Returns the first day of the month from the given DateTime
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// Returns the last day of the month from the given DateTime
        public static DateTime LastDayOfMonth(this DateTime date)
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1).AddDays(-1);
        }
    }
}
