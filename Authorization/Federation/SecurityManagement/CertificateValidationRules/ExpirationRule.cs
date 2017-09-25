using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kernel.Cryptography.Validation;

namespace SecurityManagement.CertificateValidationRules
{
    internal abstract class CertificateValidationRule : ICertificateValidationRule
    {
        public Task Validate(CertificateValidationContext context, Func<CertificateValidationContext, Task> next)
        {
            this.Internal(context);
            throw new NotImplementedException();
        }

        private void Internal(CertificateValidationContext context)
        {
            throw new NotImplementedException();
        }
    }
}
