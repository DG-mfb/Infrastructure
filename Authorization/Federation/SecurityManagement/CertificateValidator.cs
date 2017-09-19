using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Kernel.Cryptography.Validation;
using SecurityManagement.BackchannelCertificateValidationRules;

namespace SecurityManagement
{
    internal class CertificateValidator : X509CertificateValidator, ICertificateValidator
    {
        public bool Validate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            var rules = this.GetBackchannelCertificateValidationRules();
            throw new NotImplementedException();
        }
        
        public override void Validate(X509Certificate2 certificate)
        {
            throw new NotImplementedException();
        }

        //ToDo: use factory or DI container
        private ICollection<IBackchannelCertificateValidationRule> GetBackchannelCertificateValidationRules()
        {
            return new IBackchannelCertificateValidationRule[] {new BackchannelNoValidationRule(), new BackchannelChainTrustValidationRule() };
        }
    }
}