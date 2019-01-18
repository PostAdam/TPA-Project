using System.Reflection;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Model.ModelDTG;

namespace Model.Reflection
{
    public class Reflector
    {
        public AssemblyMetadata AssemblyModel { get; private set; }

        public async Task Reflect( string assemblyFile )
        {
            Assembly assembly = Assembly.LoadFile( assemblyFile ) ;
            AssemblyModel = new AssemblyMetadata( assembly );
        }
    }
}