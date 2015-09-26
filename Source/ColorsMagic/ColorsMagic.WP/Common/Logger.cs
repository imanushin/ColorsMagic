using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace ColorsMagic.WP.Common
{
    internal static class Logger
    {
        [StringFormatMethod("errorFormat")]
        public static void LogError(string errorFormat, params object[] args)
        {
            /**/
        }
    }
}
