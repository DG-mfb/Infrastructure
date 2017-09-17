using System;
using System.Security.Cryptography.X509Certificates;

namespace Kernel.Cryptography.CertificateManagement
{
    public class CertificateStore<TStore> : ICertificateStore
    {
        public TStore Store { get; }

        public CertificateStore(TStore store)
        {
            this.Store = store;
        }

        public X509Certificate2 GetX509Certificate2()
        {
            throw new NotImplementedException();
        }
    }
}