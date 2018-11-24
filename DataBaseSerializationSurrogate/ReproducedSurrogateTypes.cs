﻿using System.Collections.Generic;
using DataBaseSerializationSurrogate.MetadataSurrogates;

namespace DataBaseSerializationSurrogate
{
    public class ReproducedSurrogateTypes : Dictionary<string, TypeMetadataSurrogate>
    {
        private static ReproducedSurrogateTypes _reproducedSurrogateTypes = null;

        private ReproducedSurrogateTypes()
        {
        }

        public static ReproducedSurrogateTypes Instance =>
            _reproducedSurrogateTypes ?? ( _reproducedSurrogateTypes = new ReproducedSurrogateTypes() );
    }
}