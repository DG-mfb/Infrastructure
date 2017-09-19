using System;

namespace Kernel.Cryptography.Validation
{
    public interface IBackchannelCertificateValidationRule
    {
        bool Validate(BackchannelCertificateValidationContext context, Func<BackchannelCertificateValidationContext, bool> next);
    }
}