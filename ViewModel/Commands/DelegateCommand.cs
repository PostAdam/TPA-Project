using System;
using System.Windows.Input;

namespace ViewModel.Commands
{
    public class DelegateCommand : ICommand
    {
        #region Private

        private readonly Action _action;

        #endregion

        #region Constructor

        public DelegateCommand(Action action)
        {
            _action = action;
        }

        #endregion

        #region Command Functionality

        public void Execute(object parameter)
        {
            _action();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

#pragma warning disable 67
        public event EventHandler CanExecuteChanged;
#pragma warning restore 67

        #endregion
    }
}