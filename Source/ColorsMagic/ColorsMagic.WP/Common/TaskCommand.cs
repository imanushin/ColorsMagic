using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ColorsMagic.WP.Common
{
    public sealed class TaskCommand : TaskCommand<object>
    {
        public TaskCommand(Func<Task> action, Predicate<object> canExecute = null) : base(o => action(), canExecute)
        {
        }
    }

    public class TaskCommand<T> : RelayCommand<T>
    {
        public TaskCommand(Func<T, Task> action, Predicate<T> canExecute = null) : base(GetExecution(action), canExecute)
        {
        }

        private static Action<T> GetExecution(Func<T, Task> action)
        {
            return new Action<T>(obj =>
            {
                action(obj).SuppressExceptions();
            });
        }
    }
}