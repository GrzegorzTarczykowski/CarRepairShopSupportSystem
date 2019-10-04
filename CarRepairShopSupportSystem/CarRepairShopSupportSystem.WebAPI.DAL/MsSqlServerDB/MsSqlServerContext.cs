using CarRepairShopSupportSystem.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.DAL.MsSqlServerDB
{
    public class MsSqlServerContext : DbContext
    {
        public MsSqlServerContext() : base("CarRepairShopSupportSystemConnectionStrings")
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleBrand> VehicleBrands { get; set; }
        public DbSet<VehicleColour> VehicleColours { get; set; }
        public DbSet<VehicleEngine> VehicleEngines { get; set; }
        public DbSet<VehicleFuel> VehicleFuels { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<VehiclePart> VehicleParts { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Message>()
                        .HasRequired(m => m.UserReceiver)
                        .WithMany(u => u.ReceivedMessages)
                        .HasForeignKey(m => m.UserReceiverId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                        .HasRequired(m => m.UserSender)
                        .WithMany(u => u.SentMessages)
                        .HasForeignKey(m => m.UserSenderId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                        .HasRequired(o => o.Vehicle)
                        .WithMany(v => v.Orders)
                        .HasForeignKey(o => o.VehicleId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                        .HasMany<Service>(o => o.ContainsServices)
                        .WithMany(s => s.ContainedInOrders)
                        .Map(cs =>
                        {
                            cs.MapLeftKey($"{nameof(Order)}Id");
                            cs.MapRightKey($"{nameof(Service)}Id");
                            cs.ToTable($"{nameof(Order)}{nameof(Service)}");
                        });

            modelBuilder.Entity<Order>()
                        .HasMany<VehiclePart>(o => o.UsedVehicleParts)
                        .WithMany(vp => vp.UsedInOrders)
                        .Map(cs =>
                        {
                            cs.MapLeftKey($"{nameof(Order)}Id");
                            cs.MapRightKey($"{nameof(VehiclePart)}Id");
                            cs.ToTable($"{nameof(Order)}{nameof(VehiclePart)}");
                        });

            modelBuilder.Entity<Permission>()
                        .HasKey(p => p.PermissiondId);
            
            modelBuilder.Entity<User>()
                        .HasMany<Order>(u => u.WorksOnOrders)
                        .WithMany(o => o.WorkByUsers)
                        .Map(cs =>
                        {
                            cs.MapLeftKey($"{nameof(User)}Id");
                            cs.MapRightKey($"{nameof(Order)}Id");
                            cs.ToTable($"{nameof(User)}{nameof(Order)}");
                        });
        }
    }
}
