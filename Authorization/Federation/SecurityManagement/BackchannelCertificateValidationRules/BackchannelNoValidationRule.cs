using System;
using Kernel.Cryptography.Validation;

namespace SecurityManagement.BackchannelCertificateValidationRules
{
    internal class BackchannelNoValidationRule : BackchannelValidationRule
    {
        protected override bool ValidateInternal(BackchannelCertificateValidationContext context)
        {
            context.IsValid = true;
            return true;
        }
    }
}