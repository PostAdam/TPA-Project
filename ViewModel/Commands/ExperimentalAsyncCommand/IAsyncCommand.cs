using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel.Commands.ExperimentalAsyncCommand
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync( object parameter );
    }
}