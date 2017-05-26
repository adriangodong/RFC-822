using System;
using System.Globalization;
using Xunit;

namespace Rfc822.Tests
{
    public partial class DateTimeTests
    {
        [Theory]
        [MemberData("DayNames")]
        [MemberData("MonthNames")]
        [MemberData("TimeZones")]
        [MemberData("MilitaryTimeZones")]
        [MemberData("NumericTimeZones")]
        [MemberData("NumericTimeZonesWithMinutes")]
        [MemberData("SyntaxCombinations")]
        public void Can_parse_RFC_822_formatted_dates(string input, DateTimeSyntax syntax, DateTimeOffset expected)
        {
            var d = new Rfc822.DateTime(input, syntax);

            Assert.Equal(expected, d.Instant);
        }

        [Theory]
        [MemberData("SyntaxCombinations")]
        [MemberData("NumericTimeZones")]
        [MemberData("NumericTimeZonesWithMinutes")]
        public void Can_write_RFC_822_formatted_dates(string expected, DateTimeSyntax syntax, DateTimeOffset d)
        {
            var output = new Rfc822.DateTime(d).ToString(syntax);

            Assert.Equal(expected, output);
        }

        [Fact]
        public void Parse_supports_negative_and_positive_UTC_offset()
        {
            var d = new DateTimeOffset(2012, 5, 1, 12, 34, 00, new TimeSpan(0, 0, 0));

            Assert.Equal(d, new Rfc822.DateTime("1 May 12 12:34 -0000", DateTimeSyntax.None).Instant);
            Assert.Equal(d, new Rfc822.DateTime("1 May 12 12:34 +0000", DateTimeSyntax.None).Instant);
        }

        [Fact]
        public void Default_format_is_four_digit_year_with_seconds_and_numeric_time_zone()
        {
            var d = new DateTimeOffset(2012, 5, 1, 12, 34, 56, new TimeSpan(2, 0, 0));
            var expected = "1 May 2012 12:34:56 +0200";

            Assert.Equal(expected, new Rfc822.DateTime(d).ToString());
        }

        [Fact]
        public void ToString_translates_to_universal_time()
        {
            var d = new DateTimeOffset(2012, 5, 1, 12, 34, 56, new TimeSpan(2, 0, 0));
            var output = new Rfc822.DateTime(d).ToString(DateTimeSyntax.None);

            Assert.Equal("1 May 12 10:34 UT", output);
        }

        [Fact]
        public void Local_time_is_preserved_with_time_zone_offset()
        {
            var now = new DateTimeOffset(System.DateTime.Now);

            string expected = String.Format("{0} {1}{2}",
                now.ToString("d MMM yy HH:mm", CultureInfo.InvariantCulture),
                now.Offset.Ticks >= 0 ? "+" : "-", 
                now.Offset.ToString("hhmm"));

            var output = new Rfc822.DateTime(now).ToString(DateTimeSyntax.None | DateTimeSyntax.NumericTimeZone);

            Assert.Equal(expected, output);
        }

        [Theory]
        [MemberData("RandomDates")]
        public void Can_round_trip(DateTimeOffset d)
        {
            var syntax = DateTimeSyntax.FourDigitYear | DateTimeSyntax.WithSeconds | DateTimeSyntax.NumericTimeZone;
            var output = new Rfc822.DateTime(d).ToString(syntax);

            Assert.Equal(d, new Rfc822.DateTime(output, syntax).Instant);
        }
    }
}
