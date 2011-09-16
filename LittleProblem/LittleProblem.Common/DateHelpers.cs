using System;

namespace LittleProblem.Common
{
    public static class DateHelpers
    {

        public static TimeSpan Days(this int span)
        {
            return new TimeSpan(span, 0, 0, 0);
        }

        public static TimeSpan Hours(this int span)
        {
            return new TimeSpan(span, 0, 0);
        }

        public static TimeSpan Minutes(this int span)
        {
            return new TimeSpan(0, span, 0);
        }

        public static DateTime AfterNow(this TimeSpan span)
        {
            return DateTime.Now.Add(span);
        }

        public static DateTime BeforeNow(this TimeSpan span)
        {
            return DateTime.Now.Add(span);
        }
    }
}
