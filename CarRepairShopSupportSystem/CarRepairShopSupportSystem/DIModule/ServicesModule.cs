using Autofac;
using CarRepairShopSupportSystem.BLL.Service;

namespace CarRepairShopSupportSystem.DIModule
{
    internal class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterAssemblyTypes(typeof(AccessTokenService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();
        }
    }
}