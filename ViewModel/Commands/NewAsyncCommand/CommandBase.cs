using System;
using System.Windows.Input;

namespace ViewModel.Commands.NewAsyncCommand
{
    public abstract class CommandBase : ICommand
    {
        private readonly Func<bool> _canExecute;

        protected CommandBase() { }

        protected CommandBase( Func<bool> canExecute )
        {
            _canExecute = canExecute ?? throw new ArgumentNullException( nameof( canExecute ) );
        }

        public bool CanExecute( object parameter )
        {
            return _canExecute == null || _canExecute();
        }

        public abstract void Execute( object parameter );

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke( this, EventArgs.Empty );
        }

        public event EventHandler CanExecuteChanged;
    }
}
