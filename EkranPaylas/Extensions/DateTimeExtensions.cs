using System;

namespace EkranPaylas.Extensions
{
    public static class DateTimeExtensions
    {
        public static int GetTime(this DateTime time)
        {
            return Convert.ToInt32(new TimeSpan((time - new DateTime(1970, 1, 1)).Ticks).TotalSeconds);
        }
    }
}
