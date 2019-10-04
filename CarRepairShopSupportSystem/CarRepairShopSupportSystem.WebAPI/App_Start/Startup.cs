using System;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System.Reflection;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Infrastructure;

[assembly: OwinStartup(typeof(CarRepairShopSupportSystem.WebAPI.App_Start.Startup))]

namespace CarRepairShopSupportSystem.WebAPI.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            var scope = config.DependencyResolver.GetRootLifetimeScope();
            
            app.UseCors(CorsOptions.AllowAll);
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);

            var authorizationServerProvider = scope.Resolve<IOAuthAuthorizationServerProvider>();
            var refreshTokenProvider = scope.Resolve<IAuthenticationTokenProvider>();


            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,

                //The Path For generating the Toekn
                TokenEndpointPath = new PathString("/token"),

                //Setting the Token Expired Time (30 minutes)
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),

                //AuthorizationServerProvider class will validate the user credentials
                Provider = authorizationServerProvider,

                //For creating the refresh token and regenerate the new access token
                RefreshTokenProvider = refreshTokenProvider
            };
            
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
