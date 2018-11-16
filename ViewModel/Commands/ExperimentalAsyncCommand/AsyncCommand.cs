using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel.Commands.ExperimentalAsyncCommand
{
    public class AsyncCommand : AsyncCommandBase
    {
        private readonly Func<CancellationToken, Task> _command;
        private readonly CancelAsyncCommand _cancelCommand;
        private NotifyTaskCompletion _execution;

        public AsyncCommand( Func<CancellationToken, Task> command )
        {
            _command = command;
            _cancelCommand = new CancelAsyncCommand();
        }

        public override bool CanExecute( object parameter )
        {
            return _execution == null || _execution.IsCompleted;
        }

        public override async Task ExecuteAsync( object parameter )
        {
            _cancelCommand.NotifyCommandStarting();
            _execution = new NotifyTaskCompletion( _command( _cancelCommand.Token ) );
            RaiseCanExecuteChanged();
            await _execution.TaskCompletion;
            _cancelCommand.NotifyCommandFinished();
            RaiseCanExecuteChanged();
        }

        public ICommand CancelCommand => _cancelCommand;

        private sealed class CancelAsyncCommand : ICommand
        {
            private CancellationTokenSource _cts = new CancellationTokenSource();
            private bool _commandExecuting;

            public CancellationToken Token => _cts.Token;

            public void NotifyCommandStarting()
            {
                _commandExecuting = true;
                if ( !_cts.IsCancellationRequested )
                    return;
                _cts = new CancellationTokenSource();
                RaiseCanExecuteChanged();
            }

            public void NotifyCommandFinished()
            {
                _commandExecuting = false;
                RaiseCanExecuteChanged();
            }

            public bool CanExecute( object parameter )
            {
                return _commandExecuting && !_cts.IsCancellationRequested;
            }

            public void Execute( object parameter )
            {
                _cts.Cancel();
                RaiseCanExecuteChanged();
            }

            public event EventHandler CanExecuteChanged;
        }
    }
}