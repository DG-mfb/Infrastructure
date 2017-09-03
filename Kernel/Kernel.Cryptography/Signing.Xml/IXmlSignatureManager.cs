using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;

namespace Kernel.Cryptography.Signing.Xml
{
    public interface IXmlSignatureManager
    {
        KeyInfo CreateKeyInfo(X509Certificate2 certificate);
    }
}