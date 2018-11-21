using System.Collections.Generic;
using Model.Reflection.MetadataModelBases;

namespace Model.Reflection.NewMetadataModels
{
    public class ReflectedTypes : Dictionary<string, TypeMetadataBase>
    {
        private static ReflectedTypes _reflectedTypes = null;

        private ReflectedTypes()
        {
        }

        public static ReflectedTypes Instance => _reflectedTypes ?? ( _reflectedTypes = new ReflectedTypes() );
    }
}