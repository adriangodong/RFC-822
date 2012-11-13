using System;
using System.Linq;
using System.Collections.Generic;

namespace Rfc822
{
    /// <summary>
    /// Maps the time zone identifiers defined by RFC 822 to their corresponding UTC offsets.
    /// </summary>
    public class TimeZoneTable : ITimeZoneMapper
    {
        protected Dictionary<string, int> table;

        public TimeZoneTable()
        {
            table = new Dictionary<string, int> { 
                // Abbreviated time zone identifiers
                { "GMT", 0 }, 
                { "UT", 0 }, 
                { "EDT", -4 }, 
                { "EST", -5 }, 
                { "CDT", -5 }, 
                { "CST", -6 }, 
                { "MDT", -6 }, 
                { "MST", -7 }, 
                { "PDT", -7 }, 
                { "PST", -8 }, 
                // Military time zone identifiers
                { "A", -1 }, 
                { "B", -2 }, 
                { "C", -3 }, 
                { "D", -4 }, 
                { "E", -5 }, 
                { "F", -6 }, 
                { "G", -7 }, 
                { "H", -8 }, 
                { "I", -9 }, 
                { "K", -10 }, 
                { "L", -11 }, 
                { "M", -12 }, 
                { "N", 1 }, 
                { "O", 2 }, 
                { "P", 3 }, 
                { "Q", 4 }, 
                { "R", 5 }, 
                { "S", 6 }, 
                { "T", 7 }, 
                { "U", 8 }, 
                { "V", 9 }, 
                { "W", 10 }, 
                { "X", 11 }, 
                { "Y", 12 }, 
                { "Z", 0 }
            };
        }

        /// <summary>
        /// Maps an RFC 822 time zone identifier to a time zone using an 
        /// internal table of identifiers and offsets.
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public TimeZone Map(string identifier)
        {
            int hours;
            table.TryGetValue(identifier, out hours);

            TimeSpan offset = new TimeSpan(hours, 0, 0);

            return new TimeZone(identifier, offset);
        }
    }
}
