using System;
using System.Globalization;

namespace Rfc822
{
    /// <summary>
    /// Parses and formats date time according to RFC 822 syntax.
    /// </summary>
    public class DateTime
    {
        private readonly DateTimeOffset instant;

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

            instant = new DateTimeOffset(dateTime, timeZoneMap.Offset);
        }

        /// <summary>
        /// Creates an Rfc822.DateTime object based on the given date time 
        /// offset.
        /// </summary>
        /// <param name="dateTimeOffset">The date time offset value that 
        /// should be converted to an RFC 822 formatted date time.</param>
        public DateTime(System.DateTimeOffset dateTimeOffset)
        {
            instant = dateTimeOffset;
        }

        /// <summary>
        /// Formats a date time offset according to RFC 822 syntax using four 
        /// digits for the year, including seconds and a numeric time zone.
        /// </summary>
        /// <returns>An RFC 822 formatted date time.</returns>
        public override string ToString()
        {
            return ToString(DateTimeSyntax.FourDigitYear | DateTimeSyntax.WithSeconds | DateTimeSyntax.NumericTimeZone);
        }

        /// <summary>
        /// Formats a date time offset according to the RFC 822 syntax specified 
        /// in the syntax parameter.
        /// </summary>
        /// <param name="syntax">The syntax that is ued to format the date time.</param>
        /// <returns>An RFC 822 formatted date time.</returns>
        public string ToString(DateTimeSyntax syntax)
        {
            var format = new DateTimeFormat(syntax).FormatSpecifier;
            string result;

            if (syntax.HasFlag(DateTimeSyntax.NumericTimeZone))
            {
                result = Instant.DateTime.ToString(format, CultureInfo.InvariantCulture);

                string timeZone = Instant.Offset.Ticks >= 0 ? "+" : "-";
                timeZone += Instant.Offset.ToString("hhmm");
                result += " " + timeZone;
            }
            else
            {
                result = Instant.UtcDateTime.ToString(format, CultureInfo.InvariantCulture);
                result += " UT";
            }

            return result;
        }

        /// <summary>
        /// A point in time relative to UTC.
        /// </summary>
        public DateTimeOffset Instant { get { return instant; } }
    }
}
