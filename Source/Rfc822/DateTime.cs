using System;
using System.Globalization;

namespace Rfc822
{
    /// <summary>
    /// Wraps a <see cref="System.DateTimeOffset" />.
    /// </summary>
    public class DateTime
    {
        /// <summary>
        /// Converts the RFC 822 string representation of date and time to a 
        /// System.DateTime and saves it together with the time zone offset 
        /// in the Instant property.
        /// </summary>
        /// <param name="input">A date and time formatted according to RFC 822 
        /// syntax.</param>
        /// <param name="syntax">The syntax rules that should be used when 
        /// interpreting the input.</param>
        /// <param name="timeZoneMapper">An instant of a TimeZoneFormat object 
        /// that is used to convert the time zone part of input to a time zone 
        /// offset.</param>
        public DateTime(string input, DateTimeSyntax syntax)
        {
            ITimeZoneMapper timeZoneMapper;
            if(syntax.HasFlag(DateTimeSyntax.NumericTimeZone))
                timeZoneMapper = new NumericTimeZoneMapper();
            else
                timeZoneMapper = new TimeZoneTable();

            var timeZoneMap = timeZoneMapper.Map(input.Substring(input.LastIndexOf(' ') + 1));
            input = input.TrimEnd(timeZoneMap.Identifier.ToCharArray());

            var format = new DateTimeFormat(syntax);

            var dateTime = System.DateTime.ParseExact(input.Trim(), format.FormatSpecifier,
                CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind | DateTimeStyles.AllowWhiteSpaces);

            Instant = new DateTimeOffset(dateTime, timeZoneMap.Offset);
        }

        public DateTime(System.DateTimeOffset dateTimeOffset)
        {
            Instant = dateTimeOffset;
        }

        public override string ToString()
        {
            return ToString(DateTimeSyntax.None);
        }

        public string ToString(DateTimeSyntax syntax)
        {
            var format = new DateTimeFormat(syntax).FormatSpecifier;
            string result = Instant.DateTime.ToString(format, CultureInfo.InvariantCulture);

            if (syntax.HasFlag(DateTimeSyntax.NumericTimeZone))
            {
                string timeZone = Instant.Offset.Ticks >= 0 ? "+" : "-";
                timeZone += Instant.Offset.ToString("hhmm");
                result += " " + timeZone;
            }
            else
            {
                result += " UT";
            }

            return result;
        }

        /// <summary>
        /// A point in time of unspecified DateTimeKind and relative to UTC.
        /// </summary>
        public DateTimeOffset Instant { get; private set; }
    }
}
