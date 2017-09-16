using System;
using Kernel.Federation.MetaData;
using Kernel.Initialisation;
using Owin;

namespace SSOShibbolethOwinMiddleware.Extensions
{
    public static class ShibbolethAuthenticationExtensions
    {
        public static IAppBuilder UseShibbolethAuthentication(this IAppBuilder app, ShibbolethAuthenticationOptions options)
        {
            if (app == null)
                throw new ArgumentNullException("app");
            if (options == null)
                throw new ArgumentNullException("options");
            var resolver = ApplicationConfiguration.Instance.DependencyResolver;
            app.Use((object)typeof(ShibbolethOwinMiddleware), (object)app, (object)options, resolver);

            app.Map(options.SPMetadataPath, a =>
            {
                a.Run(c =>
                {
                    var metadataGenerator = ShibbolethAuthenticationExtensions.ResolveMetadataGenerator<ISPMetadataGenerator>();
                    return metadataGenerator.CreateMetadata(MetadataType.SP);

                });
            });
            return app;
        }

        public static IAppBuilder UseShibbolethAuthentication(this IAppBuilder app, string wtrealm, string metadataAddress)
        {
            return app.UseShibbolethAuthentication(new ShibbolethAuthenticationOptions()
            {
                Wtrealm = wtrealm,
                MetadataAddress = metadataAddress
            });
        }
        private static TMetadatGenerator ResolveMetadataGenerator<TMetadatGenerator>() where TMetadatGenerator : IMetadataGenerator
        {
            var resolver = ApplicationConfiguration.Instance.DependencyResolver;
            var metadataGenerator = resolver.Resolve<TMetadatGenerator>();
            return metadataGenerator;
        }
    }
}