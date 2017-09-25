﻿using Kernel.Cryptography.Validation;

namespace SecurityManagement.Tests.Mock
{
    internal class CertificateValidationConfigurationProvider : ICertificateValidationConfigurationProvider
    {
        public CertificateValidationConfiguration GetConfiguration()
        {
            return new CertificateValidationConfiguration
            {
                UsePinningValidation = false,
                X509CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom
            };
        }
        public void Dispose()
        {
        }
    }
}
