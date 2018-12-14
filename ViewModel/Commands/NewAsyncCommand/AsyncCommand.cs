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

        private void Cancel()
        {
            _cancellationTokenSource?.Cancel();
        }

        public override async void Execute( object parameter )
        {
            IsRunning = true;
            try
            {
                using ( var tokenSource = new CancellationTokenSource() )
                {
                    _cancellationTokenSource = tokenSource;

                    await ExecuteAsync( tokenSource.Token );
                }
            }
            finally
            {
                _cancellationTokenSource = null;
                IsRunning = false;
            }
        }

        public async Task Execute()
        {
           await Task.Run( () => Execute( new object() ) );
        }

        private Task ExecuteAsync( CancellationToken cancellationToken )
        {
            return _action();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged( [CallerMemberName] string propertyName = null )
        {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
        }
    }
}