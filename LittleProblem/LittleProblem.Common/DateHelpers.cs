using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LittleProblem.Common
{
    public static class DateHelpers
    {

        public static TimeSpan Days(this int span)
        {
            return new TimeSpan(days: span, hours: 0, minutes: 0, seconds: 0);
        }

        public static TimeSpan Hours(this int span)
        {
            return new TimeSpan(hours: span, minutes: 0, seconds: 0);
        }

        public static TimeSpan Minutes(this int span)
        {
            return new TimeSpan(hours: 0, minutes: span, seconds: 0);
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
