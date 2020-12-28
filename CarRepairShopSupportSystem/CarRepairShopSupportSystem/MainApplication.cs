using System;
using Android.App;
using Android.Runtime;
using Autofac;

namespace CarRepairShopSupportSystem
{
    [Application]
    internal class MainApplication : Application
    {
        internal static ILifetimeScope Container { get; private set; }

        protected MainApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate(); 
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(typeof(MainApplication).Assembly);
            Container = builder.Build();
        }
    }
}