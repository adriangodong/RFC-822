using System;
using System.Text;

namespace Rfc822
{
    /// <summary>
    /// Utility class to create a format specifier for parsing a DateTime object 
    /// from a date time string that is based on RFC 822 syntax rules.
    /// </summary>
    internal class DateTimeFormat
    {
        private readonly string specifier;

        /// <summary>
        /// Creates a new DateTimeFormat object based on the syntax rules 
        /// provided in the syntax parameter.
        /// </summary>
        /// <param name="syntax">The syntax rules that should apply when using 
        /// this DateTimeFormat object.</param>
        public DateTimeFormat(DateTimeSyntax syntax)
        {
            StringBuilder specifier = new StringBuilder();

            specifier.Append(syntax.HasFlag(DateTimeSyntax.WithDayName) ? "ddd," : String.Empty);
            specifier.Append(syntax.HasFlag(DateTimeSyntax.TwoDigitDay) ? " dd" : " d");
            specifier.Append(" MMM");
            specifier.Append(syntax.HasFlag(DateTimeSyntax.FourDigitYear) ? " yyyy" : " yy");
            specifier.Append(syntax.HasFlag(DateTimeSyntax.WithSeconds) ? " HH:mm:ss" : " HH:mm");
            specifier.Append("");

            this.specifier = specifier.ToString();
        }

        /// <summary>
        /// Format specifier that can be used to parse or format a 
        /// DateTime object according to RFC 822 date time syntax rules. 
        /// </summary>
        public string FormatSpecifier { get { return specifier; } }
    }
}
