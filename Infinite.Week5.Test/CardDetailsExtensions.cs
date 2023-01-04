using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Week5.Test
{
    public static class CardDetailsExtensions
    {
        public static DateTime FirstDayOfMonth(this DateTime givenDate)
        {
            return new DateTime(givenDate.Year, givenDate.Month, 1);
        }
    }
}
