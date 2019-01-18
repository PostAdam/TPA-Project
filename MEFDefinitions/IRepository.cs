using System.Threading.Tasks;
using System.Threading;
using ModelBase;

namespace MEFDefinitions
{
    public interface IRepository
    {
       /* Task Write<T>( T type, string fileName ) where T : class;
        Task<T> Read<T>( string fileName ) where T : class;*/
    
        Task Write( AssemblyMetadataBase type, CancellationToken cancellationToken );
        Task<object> Read( CancellationToken cancellationToken );
    }
}