using System.Security.Cryptography.X509Certificates;
using Kernel.Cryptography.CertificateManagement;

namespace Kernel.Federation.MetaData.Configuration.Cryptography
{
    public class X509StoreCertificateConfiguration : CertificateStore<X509Store>
    {
        string _certName;
        public X509StoreCertificateConfiguration(X509Store store, string certName) : base(store)
        {
            this._certName = certName;
        }
    }
}