using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;

namespace DataBaseSerializationSurrogates.MetadataSurrogates
{
    public class MethodMetadataSurrogate
    {
        #region Constructor

        public MethodMetadataSurrogate( MethodMetadata methodMetadata )
        {
            Name = Name;
            Extension = methodMetadata.Extension;
            ReturnType = TypeMetadataSurrogate.EmitSurrogateTypeMetadata( methodMetadata.ReturnType );
            MethodAttributes = CollectionTypeAccessor.GetTypesMetadata( methodMetadata.MethodAttributes );
            Parameters = CollectionTypeAccessor.GetParametersMetadata( methodMetadata.Parameters );
            GenericArguments = CollectionTypeAccessor.GetTypesMetadata( methodMetadata.GenericArguments );
            Modifiers = methodMetadata.Modifiers;
        }

        #endregion

        #region Properties

        [Key]
        public string Name { get; set; }

        public bool Extension { get; set; }

        public TypeMetadataSurrogate ReturnType { get; set; }

        public IEnumerable<TypeMetadataSurrogate> MethodAttributes { get; set; }

        public IEnumerable<ParameterMetadataSurrogate> Parameters { get; set; }

        public IEnumerable<TypeMetadataSurrogate> GenericArguments { get; set; }

        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }

        #endregion

        public MethodMetadata GetOriginalMethodMetadata()
        {
            return new MethodMetadata()
            {
                Name = Name,
                Extension = Extension,
                ReturnType = ReturnType?.EmitOriginalTypeMetadata(),
                MethodAttributes = CollectionOriginalTypeAccessor.GetOriginalTypesMetadata( MethodAttributes ),
                Parameters = CollectionOriginalTypeAccessor.GetOriginalParametersMetadata( Parameters ),
                GenericArguments = CollectionOriginalTypeAccessor.GetOriginalTypesMetadata( GenericArguments ),
                Modifiers = Modifiers
            };
        }
    }
}