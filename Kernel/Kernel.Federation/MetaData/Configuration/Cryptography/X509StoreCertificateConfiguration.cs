using System.Security.Cryptography.X509Certificates;

namespace Kernel.Federation.MetaData.Configuration.Cryptography
{
    class X509StoreCertificateConfiguration : CertificateConfiguration<X509Store>
    {
        string _certName;
        public X509StoreCertificateConfiguration(X509Store store, string certName) : base(store)
        {
            this._certName = certName;
        }
    }
}