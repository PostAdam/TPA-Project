using System;
using System.Collections.Generic;
using ModelBase;
using ModelBase.Enums;
using static Model.ModelDTG.Accessors.CollectionOriginalTypeAccessor;
using static Model.ModelDTG.Accessors.CollectionTypeAccessor;

namespace Model.ModelDTG
{
    public class PropertyMetadataSurrogate
    {
        #region Constructor

        public PropertyMetadataSurrogate( PropertyMetadataBase propertyMetadata )
        {
            Name = propertyMetadata.Name;
            PropertyAttributes = GetTypesMetadata( propertyMetadata.PropertyAttributes );
            Modifiers = Modifiers;
            TypeMetadata = TypeMetadataSurrogate.EmitSurrogateTypeMetadata( propertyMetadata.TypeMetadata );
            Getter = propertyMetadata.Getter == null ? null : new MethodMetadataSurrogate( propertyMetadata.Getter );
            Setter = propertyMetadata.Setter == null ? null : new MethodMetadataSurrogate( propertyMetadata.Setter );
        }

        #endregion

        #region Properties


        public string Name { get; set; }
        public IEnumerable<TypeMetadataSurrogate> PropertyAttributes { get; set; }
        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }
        public TypeMetadataSurrogate TypeMetadata { get; set; }
        public MethodMetadataSurrogate Getter { get; set; }
        public MethodMetadataSurrogate Setter { get; set; }

        #endregion

        public PropertyMetadataBase GetOriginalPropertyMetadata()
        {
            return new PropertyMetadataBase()
            {
                Name = Name,
                PropertyAttributes = GetOriginalTypesMetadata( PropertyAttributes ),
                Modifiers = Modifiers,
                TypeMetadata = TypeMetadata?.EmitOriginalTypeMetadata(),
                Getter = Getter?.GetOriginalMethodMetadata(),
                Setter = Setter?.GetOriginalMethodMetadata()
            };
        }
    }
}