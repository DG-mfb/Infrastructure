namespace Shared.Logging
{
	using System;

    public class ExceptionIdentityContext
    {
        public Guid? UserIdentifier { get; set; }

        public string TenantIdentifier { get; set; }
    }
}