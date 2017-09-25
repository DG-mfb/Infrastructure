using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;
using System.Threading.Tasks;
using Kernel.Cryptography.Validation;
using SecurityManagement.BackchannelCertificateValidationRules;

namespace SecurityManagement
{
    internal class CertificateValidator : X509CertificateValidator, ICertificateValidator
    {
        ICertificateValidationConfigurationProvider _configurationProvider;
        public CertificateValidator(ICertificateValidationConfigurationProvider configurationProvider)
        {
            this._configurationProvider = configurationProvider;
        }

        public X509CertificateValidationMode X509CertificateValidationMode
        {
            get
            {
                var configuration = this._configurationProvider.GetConfiguration();
                return configuration.X509CertificateValidationMode;
            }
        }

        public bool Validate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            var context = new BackchannelCertificateValidationContext(certificate, chain, sslPolicyErrors);
            Func<BackchannelCertificateValidationContext, Task> seed = x => Task.CompletedTask;

            var rules = this.GetBackchannelCertificateValidationRules();
            var validationDelegate = rules.Aggregate(seed, (f, next) => new Func<BackchannelCertificateValidationContext, Task>(c => next.Validate(c, f)));
            var task = validationDelegate(context);
            task.Wait();
            return context.IsValid;
        }
        
        public override void Validate(X509Certificate2 certificate)
        {
            //throw new NotImplementedException();
        }

        //ToDo: use factory or DI container
        private ICollection<IBackchannelCertificateValidationRule> GetBackchannelCertificateValidationRules()
        {
            return new IBackchannelCertificateValidationRule[] {new BackchannelNoValidationRule(), new BackchannelChainTrustValidationRule() };
        }
    }
}