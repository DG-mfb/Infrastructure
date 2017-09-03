using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Cryptography.CertificateManagement
{
    public interface ICertificateManager
    {
        X509Certificate2 GetCertificate(string path, SecureString password);
    }
}
