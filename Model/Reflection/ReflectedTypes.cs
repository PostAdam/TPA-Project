using System.Collections.Generic;
using Model.Reflection.MetadataModels;

namespace Model.Reflection
{
    public class ReflectedTypes : Dictionary<string, TypeMetadata>
    {
        private static ReflectedTypes _reflectedTypes = null;

        private ReflectedTypes()
        {
        }

        public static ReflectedTypes Instance => _reflectedTypes ?? ( _reflectedTypes = new ReflectedTypes() );
    }
}