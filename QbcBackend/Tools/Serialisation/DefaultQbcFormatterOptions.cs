using System;
using System.Collections.Generic;
using System.Text;

namespace QbcBackend.Tools.Serialisation
{
    /// <summary>
    /// Default formatter option can be overriden
    /// </summary>
    public class DefaultQbcFormatterOptions : IQbcFormatterOptions
    {
        /// <summary>
        /// String format of date string
        /// </summary>
        public string DateTimeFormatString
        {
            get => "yyyy-MM-ddTHH:mm:ssZ";
        }
    }
}
