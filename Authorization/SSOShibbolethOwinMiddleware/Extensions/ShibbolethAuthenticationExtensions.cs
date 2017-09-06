﻿using System;
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
            app.Use((object)typeof(ShibbolethOwinMiddleware), (object)app, (object)options);
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
    }
}