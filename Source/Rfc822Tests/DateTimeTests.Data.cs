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
                DateTimeOffset d = new DateTimeOffset(2012, 4, 30, 12, 34, 0, new TimeSpan(0, 0, 0));

                yield return new object[] { "Mon, 30 Apr 12 12:34 UT", 
                    DateTimeSyntax.WithDayName, d};
                yield return new object[] { "Tue, 1 May 12 12:34 UT", 
                    DateTimeSyntax.WithDayName, d.AddDays(1)};
                yield return new object[] { "Wed, 2 May 12 12:34 UT", 
                    DateTimeSyntax.WithDayName, d.AddDays(2)};
                yield return new object[] { "Thu, 3 May 12 12:34 UT", 
                    DateTimeSyntax.WithDayName, d.AddDays(3)};
                yield return new object[] { "Fri, 4 May 12 12:34 UT", 
                    DateTimeSyntax.WithDayName, d.AddDays(4)};
                yield return new object[] { "Sat, 5 May 12 12:34 UT", 
                    DateTimeSyntax.WithDayName, d.AddDays(5)};
                yield return new object[] { "Sun, 6 May 12 12:34 UT", 
                    DateTimeSyntax.WithDayName, d.AddDays(6)};
            }
        }

        public static IEnumerable<object[]> MonthNames
        {
            get
            {
                DateTimeOffset d = new DateTimeOffset(2012, 1, 1, 12, 34, 0, new TimeSpan(0, 0, 0));

                yield return new object[] { "1 Jan 12 12:34 UT", 
                    DateTimeSyntax.None, d};
                yield return new object[] { "1 Feb 12 12:34 UT", 
                    DateTimeSyntax.None, d.AddMonths(1)};
                yield return new object[] { "1 Mar 12 12:34 UT", 
                    DateTimeSyntax.None, d.AddMonths(2)};
                yield return new object[] { "1 Apr 12 12:34 UT", 
                    DateTimeSyntax.None, d.AddMonths(3)};
                yield return new object[] { "1 May 12 12:34 UT", 
                    DateTimeSyntax.None, d.AddMonths(4)};
                yield return new object[] { "1 Jun 12 12:34 UT", 
                    DateTimeSyntax.None, d.AddMonths(5)};
                yield return new object[] { "1 Jul 12 12:34 UT", 
                    DateTimeSyntax.None, d.AddMonths(6)};
                yield return new object[] { "1 Aug 12 12:34 UT", 
                    DateTimeSyntax.None, d.AddMonths(7)};
                yield return new object[] { "1 Sep 12 12:34 UT", 
                    DateTimeSyntax.None, d.AddMonths(8)};
                yield return new object[] { "1 Oct 12 12:34 UT", 
                    DateTimeSyntax.None, d.AddMonths(9)};
                yield return new object[] { "1 Nov 12 12:34 UT", 
                    DateTimeSyntax.None, d.AddMonths(10)};
                yield return new object[] { "1 Dec 12 12:34 UT", 
                    DateTimeSyntax.None, d.AddMonths(11)};
            }
        }

        public static IEnumerable<object[]> TimeZones
        {
            get
            {
                string[] zones = "GMT,UT,EDT,EST,CDT,CST,MDT,MST,PDT,PST".Split(',');
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
                char[] militaryZones = "ABCDEFGHIKLMNOPQRSTUVWXYZ".ToCharArray();
                int[] militaryOffsets = { -1,-2,-3,-4,-5,-6,-7,-8,-9,-10,-11,-12,1,2,3,4,5,6,7,8,9,10,11,12,0 };
                for (int i = 0; i < militaryZones.Length; i++)
                {
                    var d = new DateTimeOffset(2012, 5, 1, 12, 34, 0, new TimeSpan(militaryOffsets[i], 0, 0));
                    yield return new object[] { "1 May 12 12:34 " + militaryZones[i], DateTimeSyntax.None, d };
                }
            }
        }

        // TODO: syntax (day, year, seconds), numeric time zones, case sensitivity
    }
}
