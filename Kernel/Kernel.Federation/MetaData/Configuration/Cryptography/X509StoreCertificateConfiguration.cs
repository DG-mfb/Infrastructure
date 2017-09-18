using System;
using System.Security.Cryptography.X509Certificates;
using Kernel.Cryptography.CertificateManagement;

namespace Kernel.Federation.MetaData.Configuration.Cryptography
{
    public class X509StoreCertificateConfiguration : CertificateStore<X509Store>
    {
        private readonly object _searchCriteria;
        private readonly bool _validOnly;
        private readonly X509FindType _searchCriteriaType;

        public X509StoreCertificateConfiguration(string storeName, object searchCriteria, X509FindType searchCriteriaType, StoreLocation storeLocation, bool validOnly)
            :this(new X509Store(storeName, storeLocation), searchCriteria, searchCriteriaType, validOnly)
        {
        }

        public X509StoreCertificateConfiguration(X509Store store, object searchCriteria, X509FindType searchCriteriaType, bool validOnly) 
            : base(store)
        {
            this._searchCriteria = searchCriteria;
            this._validOnly = validOnly;
            this._searchCriteriaType = searchCriteriaType;
        }

        public override X509Certificate2 GetX509Certificate2()
        {
            using (base.Store)
            {
                base.Store.Open(OpenFlags.ReadOnly);
                var certificates = base.Store.Certificates;
                var cert = certificates.Find(this._searchCriteriaType, this._searchCriteria, this._validOnly);
                if (cert.Count != 1)
                    throw new InvalidOperationException(String.Format("There must be one certificate found with search criteria type: {0}. Search criteria: {1}", this._searchCriteriaType, this._searchCriteria));

                return cert[0];
            }
        }
    }
}