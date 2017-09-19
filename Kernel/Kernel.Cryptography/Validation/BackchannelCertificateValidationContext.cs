using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Kernel.Cryptography.Validation
{
    public class BackchannelCertificateValidationContext
    {
        public bool IsValid { get; private set; }
        public X509Certificate Certificate { get; }
        public X509Chain Chain { get; }
        public SslPolicyErrors SslPolicyErrors { get; }
        public BackchannelCertificateValidationContext(X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            this.Certificate = certificate;
            this.Chain = chain;
            this.SslPolicyErrors = sslPolicyErrors;
        }
        public void Validated()
        {
            this.IsValid = true;
        }
    }
}