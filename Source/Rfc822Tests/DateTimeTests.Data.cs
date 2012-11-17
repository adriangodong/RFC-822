using Rfc822;
using System;
using System.Collections.Generic;

namespace Rfc822Tests
{
    public partial class DateTimeTests
    {
        public static IEnumerable<object[]> DayNames
        {
            get
            {
                DateTimeOffset d = new DateTimeOffset(2012, 5, 1, 12, 34, 0, new TimeSpan(0, 0, 0));
                string[] days = "Tue,Wed,Thu,Fri,Sat,SUN,mon".Split(',');

                for (var i = 0; i < days.Length; i++)
                {
                    string input = String.Format("{0}, {1} May 12 12:34 UT", days[i], 1 + i);
                    yield return new object[] { input, DateTimeSyntax.WithDayName, d.AddDays(i)};
                }
            }
        }

        public static IEnumerable<object[]> MonthNames
        {
            get
            {
                DateTimeOffset d = new DateTimeOffset(2012, 1, 1, 12, 34, 0, new TimeSpan(0, 0, 0));
                string[] months = "Jan,Feb,Mar,Apr,May,Jun,Jul,Aug,Sep,Oct,NOV,dec".Split(',');

                for (var i = 0; i < months.Length; i++)
                {
                    string input = String.Format("1 {0} 12 12:34 UT", months[i]);
                    yield return new object[] { input, DateTimeSyntax.None, d.AddMonths(i) };
                }
            }
        }

        public static IEnumerable<object[]> TimeZones
        {
            get
            {
                string[] zones = "GMT,UT,EDT,EST,CDT,CST,MDT,MST,pdt,Pst".Split(',');
                int[] offsets = { 0, 0, -4, -5, -5, -6, -6, -7, -7, -8 };

                for (int i = 0; i < zones.Length; i++)
                {
                    var d = new DateTimeOffset(2012, 5, 1, 12, 34, 0, new TimeSpan(offsets[i], 0, 0));
                    yield return new object[] { "1 May 12 12:34 " + zones[i], DateTimeSyntax.None, d };
                }
            }
        }

        public static IEnumerable<object[]> MilitaryTimeZones
        {
            get
            {
                char[] zones = "ABCDEFGHIKLMNOPQRSTUVWXYz".ToCharArray();
                int[] offsets = { -1,-2,-3,-4,-5,-6,-7,-8,-9,-10,-11,-12,1,2,3,4,5,6,7,8,9,10,11,12,0 };

                for (int i = 0; i < zones.Length; i++)
                {
                    var d = new DateTimeOffset(2012, 5, 1, 12, 34, 0, new TimeSpan(offsets[i], 0, 0));
                    yield return new object[] { "1 May 12 12:34 " + zones[i], DateTimeSyntax.None, d };
                }
            }
        }

        public static IEnumerable<object[]> NumericTimeZones
        {
            get
            {
                string[] zones = "+0000,-0000,+0100,+0200,+1000,+1100,-0100,-0200,-1000,-1100".Split(',');
                int[] offsets = { 0, 0, 1, 2, 10, 11, -1, -2, -10, -11 };

                for (int i = 0; i < zones.Length; i++)
                {
                    var d = new DateTimeOffset(2012, 5, 1, 12, 34, 0, new TimeSpan(offsets[i], 0, 0));
                    yield return new object[] { "1 May 12 12:34 " + zones[i], DateTimeSyntax.NumericTimeZone, d };
                }
            }
        }

        public static IEnumerable<object[]> NumericTimeZonesWithMinutes
        {
            get
            {
                string[] zones = "+0012,-0012,+0112,+0212,+1012,+1112,-0112,-0212,-1012,-1112".Split(',');
                int[] offsets = { 0, 0, 1, 2, 10, 11, -1, -2, -10, -11 };

                for (int i = 0; i < zones.Length; i++)
                {
                    var d = new DateTimeOffset(2012, 5, 1, 12, 34, 0, new TimeSpan(offsets[i], zones[i].StartsWith("+") ? 12 : -12, 0));
                    yield return new object[] { "1 May 12 12:34 " + zones[i], DateTimeSyntax.NumericTimeZone, d };
                }
            }
        }

        public static IEnumerable<object[]> SyntaxCombinations
        {
            get
            {
                var d = new DateTimeOffset(2012, 5, 1, 12, 34, 0, new TimeSpan(0, 0, 0));

                yield return new object[] { "1 May 12 12:34 UT", 0, d };
                yield return new object[] { "01 May 12 12:34 UT", 1, d };
                yield return new object[] { "1 May 2012 12:34 UT", 2, d };
                yield return new object[] { "01 May 2012 12:34 UT", 3, d };
                yield return new object[] { "1 May 12 12:34:54 UT", 4, d.AddSeconds(54) };
                yield return new object[] { "01 May 12 12:34:54 UT", 5, d.AddSeconds(54) };
                yield return new object[] { "1 May 2012 12:34:54 UT", 6, d.AddSeconds(54) };
                yield return new object[] { "01 May 2012 12:34:54 UT", 7, d.AddSeconds(54) };
                yield return new object[] { "Tue, 1 May 12 12:34 UT", 8, d };
                yield return new object[] { "Tue, 01 May 12 12:34 UT", 9, d };
                yield return new object[] { "Tue, 1 May 2012 12:34 UT", 10, d };
                yield return new object[] { "Tue, 01 May 2012 12:34 UT", 11, d };
                yield return new object[] { "Tue, 1 May 12 12:34:54 UT", 12, d.AddSeconds(54) };
                yield return new object[] { "Tue, 01 May 12 12:34:54 UT", 13, d.AddSeconds(54) };
                yield return new object[] { "Tue, 1 May 2012 12:34:54 UT", 14, d.AddSeconds(54) };
                yield return new object[] { "Tue, 01 May 2012 12:34:54 UT", 15, d.AddSeconds(54) };
            }
        }
    }
}
