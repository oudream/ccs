using System;
using System.Collections.Generic;
using System.Text;

namespace CCS
{
 	public class CsTime
	{
        public static readonly DateTime OriginalUnix = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        public const Int64 MaxMsepoch = 253402271999000;
        public const Int64 MinMsepoch = -2177481600000;

        public static Int64 currentMsepoch()
        {
            return (Int64)DateTime.UtcNow.Subtract(OriginalUnix).TotalMilliseconds;
        }

        public static int currentSepoch()
        {
            return (int)DateTime.UtcNow.Subtract(OriginalUnix).TotalSeconds;
        }

        public static Int64 getMsepoch(DateTime dt)
        {
            return (Int64)dt.Subtract(OriginalUnix).TotalMilliseconds;
        }

        public static int getSepoch(DateTime dt)
        {
            return (int)dt.Subtract(OriginalUnix).TotalSeconds;
        }

        public static DateTime msepochToDateTime(Int64 dt)
        {
            return OriginalUnix.AddMilliseconds(dt);
        }

        public static DateTime sepochToDateTime(int dt)
        {
            return OriginalUnix.AddSeconds(dt);
        }
	}
}
