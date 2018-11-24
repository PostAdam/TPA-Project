using System.Collections.Generic;
using Model.Reflection.MetadataModels;

namespace DataBaseSerializationSurrogates
{
    public class ReproducedOriginalTypes : Dictionary<string, TypeMetadata>
    {
        private static ReproducedOriginalTypes _reproducedOriginalTypes = null;

        private ReproducedOriginalTypes()
        {
        }

        public static ReproducedOriginalTypes Instance =>
            _reproducedOriginalTypes ?? ( _reproducedOriginalTypes = new ReproducedOriginalTypes() );
    }
}