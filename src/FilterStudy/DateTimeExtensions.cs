using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilterStudy
{
    public static class DateTimeExtensions
    {
        public static string ToStd(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
    }
}
