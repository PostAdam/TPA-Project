using System.Runtime.Serialization;

namespace Project.Model.Reflection.Model
{
    [DataContract( IsReference = true )]
    internal class ParameterMetadata
    {
        #region Constructor

        public ParameterMetadata( string name, TypeMetadata typeMetadata )
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