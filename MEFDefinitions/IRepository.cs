using System.Threading.Tasks;
using System.Threading;
using ModelBase;

namespace MEFDefinitions
{
    public interface IRepository
    {    
        Task Write( AssemblyMetadataBase type, CancellationToken cancellationToken );
        Task<AssemblyMetadataBase> Read( CancellationToken cancellationToken );
    }
}