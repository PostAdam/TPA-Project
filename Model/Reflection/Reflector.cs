using System.Reflection;
using System.Runtime.Serialization;
using Model.Reflection.MetadataModels;

namespace Model.Reflection
{
    [DataContract( IsReference = true )]
    public class Reflector
    {
        [DataMember]
        public AssemblyMetadata AssemblyModel { get; private set; }

        public void Reflect( string assemblyFile )
        {
            Assembly assembly = Assembly.LoadFrom( assemblyFile );
            AssemblyModel = new AssemblyMetadata( assembly );
        }
    }
}