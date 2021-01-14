using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.Framework.Validations
{
    public static class Checks
    {
        //datetime
        public static bool CheckDate(string date)
        {
            return DateTime.TryParse(date, out var temp) == true;
        }

        public static bool IsDate(string date)
        {
            return DateTime.TryParse(date, out var temp) &&
                   temp.Hour == 0 &&
                   temp.Minute == 0 &&
                   temp.Second == 0 &&
                   temp.Millisecond == 0 &&
                   temp > DateTime.MinValue;
        }
    }
}
