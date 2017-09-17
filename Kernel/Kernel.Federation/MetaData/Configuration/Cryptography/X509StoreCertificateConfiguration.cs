using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Kernel.Cryptography.CertificateManagement;

namespace Kernel.Federation.MetaData.Configuration.Cryptography
{
    public class X509StoreCertificateConfiguration : CertificateStore<X509Store>
    {
        string _certName;
        public X509StoreCertificateConfiguration(string storeName, string certName)
            :this(new X509Store(storeName, StoreLocation.LocalMachine), certName)
        {
        }

        public X509StoreCertificateConfiguration(X509Store store, string certName) : base(store)
        {
            this._certName = certName;
        }

        public override X509Certificate2 GetX509Certificate2()
        {
            using (base.Store)
            {
                base.Store.Open(OpenFlags.ReadOnly);
                var certificates = base.Store.Certificates;
                var cert = certificates.Find(X509FindType.FindByIssuerName, this._certName, false);
                if (cert.Count != 1)
                    throw new InvalidOperationException(String.Format("There must be one certificate by isser:{0}", this._certName));
                return cert[0];
            }
        }
    }
}