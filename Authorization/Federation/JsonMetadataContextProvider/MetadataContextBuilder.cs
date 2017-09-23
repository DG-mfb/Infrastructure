using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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