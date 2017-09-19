using System;
using Kernel.Cryptography.Validation;

namespace SecurityManagement.BackchannelCertificateValidationRules
{
    internal abstract class BackchannelValidationRule : IBackchannelCertificateValidationRule
    {
        public bool Validate(BackchannelCertificateValidationContext context, Func<BackchannelCertificateValidationContext, bool> next)
        {
            var validationResult = this.ValidateInternal(context);
            if (!validationResult)
                return validationResult;

            return next(context);
        }

        protected abstract bool ValidateInternal(BackchannelCertificateValidationContext context);
    }
}