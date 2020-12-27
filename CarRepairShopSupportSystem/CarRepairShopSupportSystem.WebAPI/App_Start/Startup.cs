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
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            var config = new HttpConfiguration
            {
                DependencyResolver = new AutofacWebApiDependencyResolver(container)
            };
            //Set here instead of in global.asax
            WebApiConfig.Register(config);

            app.UseCors(CorsOptions.AllowAll);
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
            var scope = config.DependencyResolver.GetRootLifetimeScope();
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = scope.Resolve<IOAuthAuthorizationServerProvider>(),
                RefreshTokenProvider = scope.Resolve<IAuthenticationTokenProvider>()
            });
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
