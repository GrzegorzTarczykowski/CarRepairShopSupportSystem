using Autofac;
using CarRepairShopSupportSystem.BLL.Models;

namespace CarRepairShopSupportSystem.DIModule
{
    class ApplicationSessionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<ApplicationSession>().AsSelf().SingleInstance();
        }
    }
}