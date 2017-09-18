using System;
using Kernel.Cryptography.CertificateManagement;

namespace Kernel.Federation.MetaData.Configuration.Cryptography
{
    public class KeyDescriptorConfiguration
    {
        public KeyUsage Use { get; set; }
        public KeyTarget KeyTarget { get; set; }
        public Uri Algorithm { get; }
        public bool IsDefault { get; set; }
        public CertificateContext CertificateContext { get; set; }
    }
}