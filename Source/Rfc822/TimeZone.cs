using System;
using System.Collections.Generic;

namespace Rfc822
{
    /// <summary>
    /// Simple read-only structure to hold a time zone identifier and its UTC offset.
    /// </summary>
    public struct TimeZone
    {
        // Used to hold the date internally
        private readonly KeyValuePair<string, TimeSpan> zone;

        public TimeZone(string identifier, TimeSpan offset)
        {
            zone = new KeyValuePair<string, TimeSpan>(identifier, offset);
        }

        /// <summary>
        /// The time zone identifier, e.g. "EDT" (abbreviated format), "-0400" (numeric 
        /// format) or "D" (military format).
        /// </summary>
        public string Identifier { get { return zone.Key; } }

        /// <summary>
        /// The UTC offset of the time zone.
        /// </summary>
        public TimeSpan Offset { get { return zone.Value; } }
    }

}
