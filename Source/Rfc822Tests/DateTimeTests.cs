using Rfc822;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Extensions;

namespace Rfc822Tests
{
    public partial class DateTimeTests
    {
        [Theory]
        [PropertyData("DayNames")]
        [PropertyData("MonthNames")]
        [PropertyData("TimeZones")]
        [PropertyData("MilitaryTimeZones")]
        [PropertyData("NumericTimeZones")]
        [PropertyData("NumericTimeZonesWithMinutes")]
        [PropertyData("SyntaxCombinations")]
        public void Can_parse_RFC_822_formatted_dates(string input, DateTimeSyntax syntax, DateTimeOffset expected)
        {
            var d = new Rfc822.DateTime(input, syntax);

            Assert.Equal(expected, d.Instant);
        }

        [Theory]
        [PropertyData("SyntaxCombinations")]
        [PropertyData("NumericTimeZones")]
        [PropertyData("NumericTimeZonesWithMinutes")]
        public void Can_write_RFC_822_formatted_dates(string expected, DateTimeSyntax syntax, DateTimeOffset d)
        {
            var output = new Rfc822.DateTime(d).ToString(syntax);

            Assert.Equal(expected, output);
        }

        [Fact]
        public void Parse_supports_negative_UTC_offset()
        {
            var d = new DateTimeOffset(2012, 5, 1, 12, 34, 00, new TimeSpan(0, 0, 0));
            var input = "1 May 12 12:34 -0000";

            Assert.Equal(d, new Rfc822.DateTime(input, DateTimeSyntax.None).Instant);
        }

        [Fact]
        public void Default_format()
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

        [Theory]
        [PropertyData("RandomDates")]
        public void Can_round_trip(DateTimeOffset d)
        {
            var syntax = DateTimeSyntax.FourDigitYear | DateTimeSyntax.WithSeconds | DateTimeSyntax.NumericTimeZone;
            var output = new Rfc822.DateTime(d).ToString(syntax);

            Assert.Equal(d, new Rfc822.DateTime(output, syntax).Instant);
        }
    }
}
