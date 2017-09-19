using System.Security.Cryptography.X509Certificates;

namespace Kernel.Cryptography.CertificateManagement
{
    public class CertificateContext
    {
        public object SearchCriteria { get; set; }
        public X509FindType SearchCriteriaType { get; set; }
        public bool ValidOnly { get; set; }
    }
}