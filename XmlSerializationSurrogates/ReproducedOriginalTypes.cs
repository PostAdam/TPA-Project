using System.Collections.Generic;
using ModelBase;

namespace XmlSerializationSurrogates
{
    public class ReproducedOriginalTypes : Dictionary<string, TypeMetadataBase>
    {
        private static ReproducedOriginalTypes _reproducedOriginalTypes = null;

        private ReproducedOriginalTypes()
        {
        }

        public static ReproducedOriginalTypes Instance =>
            _reproducedOriginalTypes ?? ( _reproducedOriginalTypes = new ReproducedOriginalTypes() );
    }
}