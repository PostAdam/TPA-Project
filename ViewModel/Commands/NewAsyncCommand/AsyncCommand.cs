using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModel.Annotations;

namespace ViewModel.Commands.NewAsyncCommand
{
    public class AsyncCommand : CommandBase, INotifyPropertyChanged
    {
        private readonly Func<Task> _action;

        private CancellationTokenSource _cancellationTokenSource;

        private bool _isRunning;

        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                _isRunning = value;
                OnPropertyChanged();
            }
        }

        public override bool CanExecute( object parameter )
        {
            return ( _canExecute == null || _canExecute() ) && !IsRunning;
        }

        private ICommand _cancelCommand;
        public ICommand CancelCommand => _cancelCommand ?? ( _cancelCommand = new DelegateCommand( Cancel ) );

        public AsyncCommand( Func<Task> action )
        {
            _action = action ?? throw new ArgumentNullException( nameof( action ) );
        }

        public AsyncCommand( Func<Task> action, Func<bool> canExecute ) : base( canExecute )
        {
            _action = action ?? throw new ArgumentNullException( nameof( action ) );
        }

        public void Cancel()
        {
            _cancellationTokenSource?.Cancel();
        }

        public override async void Execute( object parameter )
        {
            #region comment

            /*IsRunning = true;
            _cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = _cancellationTokenSource.Token;

            try
            {
                using ( token.Register( token.ThrowIfCancellationRequested ) )
                {
                    await ExecuteAsync( token );
//                    await Task.Run( () => ExecuteAsync( token ), token );
                }
            }
            catch ( AggregateException )
            {
            }
            catch ( TaskCanceledException )
            {
            }
            catch ( OperationCanceledException )
            {
            }
            finally
            {
                _cancellationTokenSource = null;
                IsRunning = false;
            }*/

            IsRunning = true;
            try
            {
                using ( var tokenSource = new CancellationTokenSource() )
                {
                    _cancellationTokenSource = tokenSource;

                    await ExecuteAsync( tokenSource.Token );
                }
            }
            catch ( TaskCanceledException )
            {
            }
            catch ( OperationCanceledException )
            {
            }
            finally
            {
                _cancellationTokenSource = null;
                IsRunning = false;
            }

            #endregion

            /*IsRunning = true;
            try
            {
                using ( var tokenSource = new CancellationTokenSource() )
                {
                    _cancellationTokenSource = tokenSource;
                    Task executeTask = ExecuteAsync( tokenSource.Token );
                    Task cancellationTask = ListenCancellation( tokenSource.Token );
                    await Task.Run( () => Task.WhenAny( executeTask, cancellationTask ), _cancellationTokenSource.Token );
                    tokenSource.Cancel();

                    if ( cancellationTask.Status == TaskStatus.Canceled )
                    {
                        throw new OperationCanceledException();
                    }

                    //                    await ExecuteAsync( tokenSource.Token );
                }
            }
            catch ( OperationCanceledException )
            {
            }
            finally
            {
                _cancellationTokenSource = null;
                IsRunning = false;
            }*/
        }

        private async Task ListenCancellation( CancellationToken tokenSourceToken )
        {
            while ( !tokenSourceToken.IsCancellationRequested )
            {
                await Task.Delay( TimeSpan.FromMilliseconds( 100 ), tokenSourceToken );
            }

            tokenSourceToken.ThrowIfCancellationRequested();
        }

        public async Task Execute()
        {
            await Task.Run( () => Execute( new object() ) );
        }

        private Task ExecuteAsync( CancellationToken cancellationToken )
        {
            return Task.Run( () => _action(), cancellationToken );
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged( [CallerMemberName] string propertyName = null )
        {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
        }
    }
}