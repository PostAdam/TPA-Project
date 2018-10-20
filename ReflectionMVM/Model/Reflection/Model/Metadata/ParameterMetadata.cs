using System.Runtime.Serialization;

namespace Project.Model.Reflection.Model
{
    [DataContract( IsReference = true )]
    public class ParameterMetadata
    {
        #region Constructor

        internal ParameterMetadata( string name, TypeMetadata typeMetadata )
        {
            Name = name;
            TypeMetadata = typeMetadata;
        }

        #endregion

        #region Internals

        [DataMember] internal string Name;
        [DataMember] internal TypeMetadata TypeMetadata;

        #endregion
    }
}