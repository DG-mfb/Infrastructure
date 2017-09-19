using System;
using System.Security.Cryptography.X509Certificates;
using Kernel.Cryptography.CertificateManagement;

namespace Kernel.Federation.MetaData.Configuration.Cryptography
{
    public class X509StoreCertificateConfiguration : CertificateStore<X509Store>
    {
        private readonly CertificateContext _certificateContext;

        public X509StoreCertificateConfiguration(CertificateContext certificateContext)
            :base(new X509Store(((X509CertificateContext)certificateContext).StoreName, ((X509CertificateContext)certificateContext).StoreLocation))
        {
            if (certificateContext == null)
                throw new ArgumentNullException("certificateContext");

            this._certificateContext = certificateContext;
        }

        public override X509Certificate2 GetX509Certificate2()
        {
            if (this._certificateContext == null)
                throw new ArgumentNullException("certificateContext");

            using (base.Store)
            {
                base.Store.Open(OpenFlags.ReadOnly);
                var certificates = base.Store.Certificates;
                var cert = certificates.Find(this._certificateContext.SearchCriteriaType, this._certificateContext.SearchCriteria, this._certificateContext.ValidOnly);
                if (cert.Count != 1)
                    throw new InvalidOperationException(String.Format("There must be one certificate found with search criteria type: {0}. Search criteria: {1}", this._certificateContext.SearchCriteriaType, this._certificateContext.SearchCriteria));

                return cert[0];
            }
        }
    }
}