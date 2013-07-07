If you need to parse an RFC 822 formated date time string, in an RSS feed for instance, you can use the Rfc822.DateTime wrapper to parse the string and get a System.DateTimeOffset back that even preserved time zone information. It supports all possible syntax variations:

- Day names, e.g. "Sun, 4 Nov 12 21:16 PST"
- Two digit day, e.g. "04 Nov 12 21:16 PST"
- Four digit year, e.g. "4 Nov 2012 21:16 PST"
- Time with seconds, e.g. "4 Nov 12 21:16:38 PST"
- Military time zones, e.g. "4 Nov 12 21:16 H"
- Numeric time zone format, e.g. "4 Nov 12 21:16 -0800"

Combinations are also supported, so a "4 Nov 2012 21:16:38 PST" which uses four digits for the year and seconds in the time, can be parsed as well. See the first example below to see how to go about parsing such a date time string.

_The code is tested with over 200 unit tests of various date time formats and time zones._

#### Parsing an RFC 822 date time string

```c#
var input = "4 Nov 2012 21:16:38 PST";

// Combine syntax options using a flag enumeration
var syntax = Rfc822.DateTimeSyntax.WithSeconds | Rfc822.DateTimeSyntax.FourDigitYear;

// Get to the date, time and time zone information
DateTimeOffset d = new Rfc822.DateTime(input, syntax).Instant;
```

#### Formatting a System.DateTimeOffset as an RFC 822 date time string

```c#
// Local system time zone is PST (-0800)
var dateTime = new DateTimeOffset(DateTime.Now);
var rfcDateTime = new Rfc822.DateTime(d);
```
Using default syntax:
```c#
Console.WriteLine(rfcDateTime.ToString());
// Result: 4 Nov 2012 21:16:00 -0800
Setting syntax options manually:

var syntax = Rfc822.DateTimeSyntax.FourDigitYear | Rfc822.DateTimeSyntax.NumericTimeZone;
Console.WriteLine(rfcDateTime.ToString(syntax));
// Result: 4 Nov 2012 21:16 -0800
```
Using Universal Time:
```
var syntax = Rfc822.DateTimeSyntax.FourDigitYear;
Console.WriteLine(rfcDateTime.ToString(syntax));
// Result: 5 Nov 2012 05:16 UT
```
As you can see from the last example, if you need to preserve the local date time information, you should format using Rfc822.DateTimeSyntax.NumericTimeZone, otherwise the date time will be translated to Universal Time and the time zone in the output will always be "UT".

#### Further reading

See RFC 822 specification [here](http://www.w3.org/Protocols/rfc822/#z28).
