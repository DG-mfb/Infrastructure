using AssetManagement.Models;
using Kernel.Authorisation;
using Kernel.Federation.MetaData;
using Kernel.Initialisation;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SSOShibbolethOwinMiddleware;
using SSOShibbolethOwinMiddleware.Extensions;

namespace AssetManagement
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            var resolver = ApplicationConfiguration.Instance.DependencyResolver;
            var optionProvider = resolver.Resolve<IAuthorizationServerOptionsProvider<OAuthAuthorizationServerOptions>>();
            OAuthOptions = optionProvider.GetOptions();
   
            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            //Shibboleth middleware, test metadata
            //ShibbolethAuthenticationExtensions.UseShibbolethAuthentication(app, "appId", "https://www.testshib.org/metadata/testshib-providers.xml");

            //Shibboleth middleware, localhost metadata metadata
            ShibbolethAuthenticationExtensions.UseShibbolethAuthentication(app, "appId", "https://dg-mfb/idp/shibboleth");

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            //app.UseFacebookAuthentication(
            //    appId: "",
            //    appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }
}