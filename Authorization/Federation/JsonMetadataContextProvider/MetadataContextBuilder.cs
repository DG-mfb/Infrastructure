﻿using System;
using Kernel.Federation.MetaData.Configuration;
using Kernel.Serialisation;

namespace JsonMetadataContextProvider
{
    internal class MetadataContextBuilder : IMetadataContextBuilder
    {
        private readonly ISerializer serialiser;

        public MetadataContextBuilder(ISerializer serialiser)
        {

        }
        public MetadataContext BuildContext()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}