using System;
using System.Windows.Input;

namespace CrystalFramework.Commands
{
    public class RelayCommand : ICommand
    {
        private Action _action;
        private Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action action, Func<bool> canExecute = null)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
