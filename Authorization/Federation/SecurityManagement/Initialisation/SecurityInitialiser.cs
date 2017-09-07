﻿using System.Threading.Tasks;
using Kernel.DependancyResolver;
using Shared.Initialisation;

namespace SecurityManagement.Initialisation
{
    public class SecurityInitialiser : Initialiser
    {
        public override byte Order
        {
            get { return 1; }
        }

        protected override Task InitialiseInternal(IDependencyResolver dependencyResolver)
        {
            dependencyResolver.RegisterType<CertificateManager>(Lifetime.Transient);
            dependencyResolver.RegisterType<XmlSignatureManager>(Lifetime.Transient);
            
            return Task.CompletedTask;
        }
    }
}