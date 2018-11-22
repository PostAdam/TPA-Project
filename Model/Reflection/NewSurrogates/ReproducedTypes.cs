using System.Collections.Generic;
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
    }
}