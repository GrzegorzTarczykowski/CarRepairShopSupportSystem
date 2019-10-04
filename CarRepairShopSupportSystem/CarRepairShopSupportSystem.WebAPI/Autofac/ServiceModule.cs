using Autofac;
using CarRepairShopSupportSystem.WebAPI.BLL.IService;

namespace CarRepairShopSupportSystem.WebAPI.Autofac
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterAssemblyTypes(typeof(IService).Assembly).AsImplementedInterfaces();
        }
    }
}