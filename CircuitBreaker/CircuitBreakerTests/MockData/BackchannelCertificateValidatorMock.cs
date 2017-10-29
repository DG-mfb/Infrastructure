using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Kernel.Security.Validation;

namespace CircuitBreakerTests.MockData
{
    internal class BackchannelCertificateValidatorMock : IBackchannelCertificateValidator
    {
        private readonly Func<bool> _validate;
        public BackchannelCertificateValidatorMock(Func<bool> validate)
        {
            this._validate = validate;
        }
        public bool Validate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return this._validate();
        }
    }
}