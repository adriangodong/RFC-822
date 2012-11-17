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
        public void Can_write_RFC_822_formatted_dates(string expected, DateTimeSyntax syntax, DateTimeOffset d)
        {
            var output = new Rfc822.DateTime(d).ToString(syntax);

            Assert.Equal(expected, output);
        }
    }
}
