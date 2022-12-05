using System;
using System.Collections.Generic;
using System.Text;

namespace QbcBackend.Tools.Serialisation
{
    /// <summary>
    /// Formatter options
    /// </summary>
    public interface IQbcFormatterOptions
    {
        /// <summary>
        /// String format of date string
        /// </summary>
        string DateTimeFormatString
        {
            get;
        }
    }
}
