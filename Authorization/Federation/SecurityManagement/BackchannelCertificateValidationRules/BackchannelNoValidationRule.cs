using System;
using Kernel.Cryptography.Validation;

namespace SecurityManagement.BackchannelCertificateValidationRules
{
    internal class BackchannelNoValidationRule : BackchannelValidationRule
    {
        protected override bool ValidateInternal(BackchannelCertificateValidationContext context)
        {
            return true;
        }
    }
}