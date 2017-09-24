using System.Collections.Generic;
using System.ServiceModel.Security;
using Kernel.Data;

namespace ORMMetadataContextProvider.Models.GlobalConfiguration
{
    public class SecuritySettings : BaseModel
    {
        public SecuritySettings()
        {
            this.BackchannelCertificateRules = new LinkedList<string>();
        }
        public X509CertificateValidationMode X509CertificateValidationMode { get; set; }
        public int BackchannelCertificateValidationMode { get; set; }
        public ICollection<string> BackchannelCertificateRules { get; set; }
    }
}