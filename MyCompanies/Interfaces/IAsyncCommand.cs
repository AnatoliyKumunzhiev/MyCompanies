using System.Threading.Tasks;
using System.Windows.Input;

namespace MyCompanies.Interfaces
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync();
        bool CanExecute();
    }
}
