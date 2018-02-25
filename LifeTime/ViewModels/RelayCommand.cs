using System;
using System.Windows.Input;

namespace LifeTime.ViewModels
{
    public class RelayCommand : System.Windows.Input.ICommand
    {
        Action<object> execute;
        Func<object, bool> canExecute;
        public event EventHandler CanExecuteChanged { add { CommandManager.RequerySuggested += value; } remove { CommandManager.RequerySuggested -= value; } }

        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
            {
                return canExecute.Invoke(parameter);
            }
            return true;
        }

        public void Execute(object parameter)
        {
            if (execute != null)
                execute.Invoke(parameter);
        }
        public RelayCommand(Action<object> executeAction) : this(executeAction, null)
        {

        }
        public RelayCommand(Action<object> executeAction, Func<object, bool> canExecute)
        {
            this.canExecute = canExecute;
            this.execute = executeAction;
        }
    }
}
