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
        public void Test(string input, DateTimeSyntax syntax, DateTimeOffset d)
        {
            var result = new Rfc822.DateTime(input, syntax);

            Assert.Equal(d, result.Instant);
        }
    }
}
