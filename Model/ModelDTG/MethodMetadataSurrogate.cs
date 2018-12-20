using System;
using System.Collections.Generic;
using ModelBase;
using ModelBase.Enums;
using static Model.ModelDTG.Accessors.CollectionOriginalTypeAccessor;
using static Model.ModelDTG.Accessors.CollectionTypeAccessor;

namespace Model.ModelDTG
{
    public class MethodMetadataSurrogate
    {
        #region Constructor

        public MethodMetadataSurrogate( MethodMetadataBase methodMetadata )
        {
            Name = Name;
            Extension = methodMetadata.Extension;
            ReturnType = TypeMetadataSurrogate.EmitSurrogateTypeMetadata( methodMetadata.ReturnType );
            MethodAttributes = GetTypesMetadata( methodMetadata.MethodAttributes );
            Parameters = GetParametersMetadata( methodMetadata.Parameters );
            GenericArguments = GetTypesMetadata( methodMetadata.GenericArguments );
            Modifiers = methodMetadata.Modifiers;
        }

        #endregion

        #region Properties

        public string Name { get; set; }
        public bool Extension { get; set; }
        public TypeMetadataSurrogate ReturnType { get; set; }
        public IEnumerable<TypeMetadataSurrogate> MethodAttributes { get; set; }
        public IEnumerable<ParameterMetadataSurrogate> Parameters { get; set; }
        public IEnumerable<TypeMetadataSurrogate> GenericArguments { get; set; }
        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }

        #endregion

        public MethodMetadataBase GetOriginalMethodMetadata()
        {
            return new MethodMetadataBase()
            {
                Name = Name,
                Extension = Extension,
                ReturnType = ReturnType?.EmitOriginalTypeMetadata(),
                MethodAttributes = GetOriginalTypesMetadata( MethodAttributes ),
                Parameters = GetOriginalParametersMetadata( Parameters ),
                GenericArguments = GetOriginalTypesMetadata( GenericArguments ),
                Modifiers = Modifiers
            };
        }
    }
}