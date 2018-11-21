using System.Collections.Generic;
using System.Globalization;
using Model.Reflection.MetadataModels;

namespace Model.Reflection.NewSurrogates
{
    public class ReproducedTypes : Dictionary<string, TypeMetadataSurrogate>
    {
        private static ReproducedTypes _reproducedTypes = null;

        private ReproducedTypes()
        {
        }

        public static ReproducedTypes Instance => _reproducedTypes ?? ( _reproducedTypes = new ReproducedTypes() );

        public TypeMetadataSurrogate GetType( TypeMetadata typeMetadata )
        {
            string typeId = typeMetadata.FullName ?? typeMetadata.NamespaceName + " . " + typeMetadata.TypeName;
            if ( !Instance.ContainsKey( typeId ) )
            {
                Instance.Add( typeId, new TypeMetadataSurrogate( typeMetadata ) );
            }

            return Instance[typeId];
        }
    }
}