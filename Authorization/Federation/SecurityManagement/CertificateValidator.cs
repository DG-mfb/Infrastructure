using System;
using System.IdentityModel.Selectors;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Kernel.Federation.CertificateProvider;

namespace SecurityManagement
{
    internal class CertificateValidator : X509CertificateValidator, ICertificateValidator
    {
        public bool Validate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        
        public override void Validate(X509Certificate2 certificate)
        {
            throw new NotImplementedException();
        }
    }
}