using Autofac;
using CarRepairShopSupportSystem.WebAPI.DAL.MsSqlServerDB;

namespace CarRepairShopSupportSystem.WebAPI.Autofac
{
    public class DbContextModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<MsSqlServerContext>().AsSelf();
        }
    }
}