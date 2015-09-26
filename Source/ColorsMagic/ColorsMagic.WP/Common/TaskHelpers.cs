using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ColorsMagic.WP.Common
{
    internal static class TaskHelpers
    {
        public static void SuppressExceptions(this Task task, [CallerMemberName] string callerName = null)
        {
            task.ContinueWith(t =>
            {
                var error = t.Exception;

                if (ReferenceEquals(error, null))
                {
                    return;
                }

                Logger.LogError("Unhandled exception in method {0}: {1}", callerName, error);
            });
        }
    }
}
