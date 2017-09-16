using System;

namespace Kernel.Federation.MetaData.Configuration.Cryptography
{
    public class KeyDescriptorConfiguration
    {
        public KeyUsage Use { get; set; }
        public Uri Algorithm { get; }
        public CertificateConfiguration KeyInfo { get; set; }
    }
}