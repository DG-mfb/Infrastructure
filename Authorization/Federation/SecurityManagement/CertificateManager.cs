using System;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using Kernel.Cryptography.CertificateManagement;

namespace SecurityManagement
{
    public class CertificateManager : ICertificateManager
    {
        public X509Certificate2 GetCertificate(string path, SecureString password)
        {
            return new X509Certificate2(path, password);
        }

        public X509Certificate2 GetCertificate(ICertificateStore store)
        {
            throw new NotImplementedException();
        }
    }
}