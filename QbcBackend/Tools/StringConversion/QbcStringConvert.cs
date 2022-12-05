using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace QbcBackend.Tools.StringConversion
{
    public static class QbcStringConvert
    {
        public  static Decimal ToDecimal(string input)
        {
            Decimal retval = Decimal.Zero;
            if (!Decimal.TryParse(input.Replace(",","."),
                                    NumberStyles.Float, 
                                    CultureInfo.CreateSpecificCulture("en-US"), 
                                    out retval))
            {
                throw new ArgumentException($"Invalid input {input}");
            }
            return retval;
        }


    }
}
