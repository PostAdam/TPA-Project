using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel.Commands.AsyncCommand
{
    public interface IAsyncCommand : ICommand
    {
        /// <summary>
        /// Executes the asynchronous command.
        /// </summary>
        /// <param name="parameter">The parameter for the command.</param>
        Task ExecuteAsync( object parameter );
    }
}