using System.Reflection;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Model.Reflection.MetadataModels;

namespace Model.Reflection
{
    [DataContract( IsReference = true )]
    public class Reflector
    {
        [DataMember]
        public AssemblyMetadata AssemblyModel { get; private set; }

        public async Task Reflect( string assemblyFile )
        {
            Assembly assembly = await Task.Run( () =>/* AppDomain.CurrentDomain.Load(*/ Assembly.LoadFile( assemblyFile /*).GetName()*/ ) );
            AssemblyModel = new AssemblyMetadata( assembly );
        }
    }
}