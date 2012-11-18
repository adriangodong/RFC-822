using Rfc822;
using System;
using System.Collections.Generic;

namespace Rfc822Tests
{
    public partial class DateTimeTests
    {
        // Date time strings with all different day names in mixed case
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

        // Date time strings with all different month names in mixed case
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

        // Date time strings with all different abbreviated time zones
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

        // Date time strings with all different military time zones
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

        // Date time strings with several numeric time zones
        public static IEnumerable<object[]> NumericTimeZones
        {
            get
            {
                string[] zones = "+0000,+0100,+0200,+1000,+1100,-0100,-0200,-1000,-1100".Split(',');
                int[] offsets = { 0, 1, 2, 10, 11, -1, -2, -10, -11 };

                for (int i = 0; i < zones.Length; i++)
                {
                    var d = new DateTimeOffset(2012, 5, 1, 12, 34, 0, new TimeSpan(offsets[i], 0, 0));
                    yield return new object[] { "1 May 12 12:34 " + zones[i], DateTimeSyntax.NumericTimeZone, d };
                }
            }
        }

        // Date time string with several numeric time zones that include minutes in the offset
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

        // Date time strings with all different syntax combinations, except numeric time zone
        public static IEnumerable<object[]> SyntaxCombinations
        {
            get
            {
                for (int syntax = 0; syntax < (int)DateTimeSyntax.NumericTimeZone; syntax++)
                {
                    var d = new DateTimeOffset(2012, 5, 1, 12, 34, 0, new TimeSpan(0, 0, 0));

                    yield return new object[] { new Rfc822.DateTime(d).ToString((DateTimeSyntax)syntax), syntax, d };
                }
            }
        }

        // 100 random dates between 2000 and 2100
        public static IEnumerable<object[]> RandomDates
        {
            get
            {
                System.DateTime start = new System.DateTime(2000, 1, 1);
                System.DateTime end = start.AddYears(100);
                
                Random generator = new Random();

                for (int i = 0; i < 100; i++)
                {
                    var d = new DateTimeOffset(
                        start
                            .AddDays(generator.Next((end - start).Days))
                            .AddHours(generator.Next(0, 24))
                            .AddMinutes(generator.Next(0, 60))
                            .AddSeconds(generator.Next(0, 60)),
                        new TimeSpan(generator.Next(-13, 14), generator.Next(-59, 60), 0));

                    yield return new object[] { d };
                }
            }
        }
    }
}
