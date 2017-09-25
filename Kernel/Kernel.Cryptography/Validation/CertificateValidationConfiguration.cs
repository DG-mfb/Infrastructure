using System;
using System.Collections.Generic;

namespace Kernel.Cryptography.Validation
{
    public class CertificateValidationConfiguration
    {
        public CertificateValidationConfiguration()
        {
            this.ValidationRules = new List<ValidationRuleDescriptor>();
        }
        public Type BackchannelPinningValidator { get; }
        public bool UsePinningValidation { get; set; }
        public ICollection<ValidationRuleDescriptor> ValidationRules { get; }
    }
}