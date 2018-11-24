using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;

namespace DataBaseSerializationSurrogates.MetadataSurrogates
{
    public class PropertyMetadataSurrogate
    {
        #region Constructor

        public PropertyMetadataSurrogate( PropertyMetadata propertyMetadata )
        {
            Name = propertyMetadata.Name;
            PropertyAttributes = CollectionTypeAccessor.GetTypesMetadata( propertyMetadata.PropertyAttributes );
            Modifiers = Modifiers;
            TypeMetadata = TypeMetadataSurrogate.EmitSurrogateTypeMetadata( propertyMetadata.TypeMetadata );
            PropertyInfo = propertyMetadata.PropertyInfo;
            Getter = propertyMetadata.Getter == null ? null : new MethodMetadataSurrogate( propertyMetadata.Getter );
            Setter = propertyMetadata.Setter == null ? null : new MethodMetadataSurrogate( propertyMetadata.Setter );
        }

        #endregion

        #region Properties

        [Key]
        public string Name { get; set; }

        public virtual IEnumerable<TypeMetadataSurrogate> PropertyAttributes { get; set; }

        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }

        public TypeMetadataSurrogate TypeMetadata { get; set; }

        public PropertyInfo PropertyInfo { get; set; }

        public MethodMetadataSurrogate Getter { get; set; }

        public MethodMetadataSurrogate Setter { get; set; }

        public virtual TypeMetadataSurrogate PropertyAttribute { get; set; }

        #endregion

        public PropertyMetadata GetOriginalPropertyMetadata()
        {
            return new PropertyMetadata()
            {
                Name = Name,
                PropertyAttributes = CollectionOriginalTypeAccessor.GetOriginalTypesMetadata( PropertyAttributes ),
                Modifiers = Modifiers,
                TypeMetadata = TypeMetadata?.EmitOriginalTypeMetadata(),
                PropertyInfo = PropertyInfo,
                Getter = Getter?.GetOriginalMethodMetadata(),
                Setter = Setter?.GetOriginalMethodMetadata()
            };
        }
    }
}