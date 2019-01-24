using System.Reflection;
using System.Threading.Tasks;
using Model.ModelDTG;

namespace Model.Reflection
{
    public class Reflector
    {
        public AssemblyMetadata AssemblyModel { get; private set; }

        public async Task Reflect( string assemblyFile )
        {
            Assembly assembly = Assembly.ReflectionOnlyLoadFrom( assemblyFile );
            await Task.Run( () => AssemblyModel = new AssemblyMetadata( assembly ) );
        }
    }
}