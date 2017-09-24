using System;

namespace ORMMetadataContextProvider.Models.GlobalConfiguration
{
    [Flags]
    public enum ValidationScope
    {
        Certificate = 1,
        BackchannelCertificate= 2
    }
}