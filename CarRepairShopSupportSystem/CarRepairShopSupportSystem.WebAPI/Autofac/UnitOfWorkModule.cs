using Autofac;
using CarRepairShopSupportSystem.WebAPI.DAL.UnitOfWork;

namespace CarRepairShopSupportSystem.WebAPI.Autofac
{
    public class UnitOfWorkModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork));
        }
    }
}