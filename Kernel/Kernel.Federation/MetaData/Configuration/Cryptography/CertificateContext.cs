using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Federation.MetaData.Configuration.Cryptography
{
    public class CertificateContext
    {
        public string StoreName { get; set; }
        public object SearchCriteria { get; set; }
        public X509FindType SearchCriteriaType { get; set; }
        public StoreLocation StoreLocation { get; set; }
        public bool ValidOnly { get; set; }
    }
}
