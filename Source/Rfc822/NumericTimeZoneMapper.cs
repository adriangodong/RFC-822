using System;

namespace Rfc822
{
    /// <summary>
    /// Maps a numeric time zone to its UTC offset.
    /// </summary>
    public class NumericTimeZoneMapper : ITimeZoneMapper
    {
        public TimeZone Map(string identifier)
        {
            int hours, minutes;

            string sign = identifier.Substring(0, 1);
            Int32.TryParse(sign + identifier.Substring(1, 2), out hours);
            Int32.TryParse(sign + identifier.Substring(3, 2), out minutes);

            TimeSpan offset = new TimeSpan(hours, minutes, 0);

            return new TimeZone(identifier, offset);
        }
    }
}
