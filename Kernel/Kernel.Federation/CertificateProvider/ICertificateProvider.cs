using System.Security.Cryptography.X509Certificates;

namespace Kernel.Federation.CertificateProvider
{
    public interface ICertificateProvider
    {
        X509Certificate2 GetX509Certificate2();
    }
}
