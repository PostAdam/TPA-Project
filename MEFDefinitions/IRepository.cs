using System.Threading.Tasks;

namespace MEFDefinitions
{
    public interface IRepository
    {
        Task Write<T>( T type, string fileName ) where T : class;
        Task<T> Read<T>( string fileName ) where T : class;
    }
}