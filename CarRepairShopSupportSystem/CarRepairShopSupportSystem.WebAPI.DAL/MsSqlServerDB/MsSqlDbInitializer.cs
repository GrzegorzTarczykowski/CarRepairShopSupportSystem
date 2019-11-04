using CarRepairShopSupportSystem.WebAPI.DAL.Enums;
using CarRepairShopSupportSystem.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.DAL.MsSqlServerDB
{
    public class MsSqlDbInitializer : DropCreateDatabaseAlways<MsSqlServerContext>
    {
        protected override void Seed(MsSqlServerContext context)
        {
            base.Seed(context);

            IList<Client> defaultClients = new List<Client>();

            defaultClients.Add(new Client() { ClientIdCode = "DOTNET"
                                            , ClientSecret = "EEF47D9A-DBA9-4D02-B7B0-04F4279A6D20"
                                            , ClientName = ""
                                            , Active = true
                                            , RefreshTokenLifeTime = 7200
                                            , AllowedOrigin = "*" });

            context.Clients.AddRange(defaultClients);

            IList<Permission> defaultPermissions = new List<Permission>();

            defaultPermissions.Add(new Permission() { Name = $"{PermissionId.SuperAdmin}" });
            defaultPermissions.Add(new Permission() { Name = $"{PermissionId.Admin}" });
            defaultPermissions.Add(new Permission() { Name = $"{PermissionId.User}" });
            defaultPermissions.Add(new Permission() { Name = $"{PermissionId.Guest}" });

            context.Permissions.AddRange(defaultPermissions);

            IList<User> defaultUsers = new List<User>();

            defaultUsers.Add(new User() { Username = "TestAdmin"
                                        , Salt = new byte[32] { 20, 10, 150, 5, 15, 45, 60, 80,
                                                                20, 10, 150, 5, 15, 45, 60, 80,
                                                                20, 10, 150, 5, 15, 45, 60, 80,
                                                                20, 10, 150, 5, 15, 45, 60, 80 }
                                        , Password = "NO8Qsz2QXnlMQ4x9K1tNzhsDVhudR9QOai2DSfRRZ2UOMePNt5NqjnGksUJtBNwuYC7lQpFsJr1hcQLUWiBjcQ==" //"1"
                                        , FirstName = "Grzegorz"
                                        , LastName = "Tarcz"
                                        , Email = "jakis@wp.pl"
                                        , PhoneNumber = "NieDzwonDoMnie"
                                        , CreateDate = DateTime.Now
                                        , LastLogin = DateTime.Now
                                        , PermissionId = 1
                                        , Permission = defaultPermissions[0] });

            defaultUsers.Add(new User() { Username = "TestGuest"
                                        , Salt = new byte[32] { 20, 10, 150, 5, 15, 45, 60, 80,
                                                                20, 10, 150, 5, 15, 45, 60, 80,
                                                                20, 10, 150, 5, 15, 45, 60, 80,
                                                                20, 10, 150, 5, 15, 45, 60, 80 }
                                        , Password = "NO8Qsz2QXnlMQ4x9K1tNzhsDVhudR9QOai2DSfRRZ2UOMePNt5NqjnGksUJtBNwuYC7lQpFsJr1hcQLUWiBjcQ==" //"1"
                                        , CreateDate = DateTime.Now
                                        , LastLogin = DateTime.Now
                                        , PermissionId = 4
                                        , Permission = defaultPermissions[3] });

            context.Users.AddRange(defaultUsers);

            IList<VehicleBrand> defaultVehicleBrands = new List<VehicleBrand>();

            defaultVehicleBrands.Add(new VehicleBrand() { Name = "Audi" });
            defaultVehicleBrands.Add(new VehicleBrand() { Name = "BMW" });
            defaultVehicleBrands.Add(new VehicleBrand() { Name = "Mercedes" });

            context.VehicleBrands.AddRange(defaultVehicleBrands);

            IList<VehicleModel> defaultVehicleModels = new List<VehicleModel>();

            defaultVehicleModels.Add(new VehicleModel() { Name = "Audi A8" });
            defaultVehicleModels.Add(new VehicleModel() { Name = "Audi Q8" });
            defaultVehicleModels.Add(new VehicleModel() { Name = "BMW Serii 1" });
            defaultVehicleModels.Add(new VehicleModel() { Name = "BMW X7" });
            defaultVehicleModels.Add(new VehicleModel() { Name = "Mercedes Klasa C" });
            defaultVehicleModels.Add(new VehicleModel() { Name = "Mercedes Klasa E" });

            context.VehicleModels.AddRange(defaultVehicleModels);

            IList<VehicleType> defaultVehicleTypes = new List<VehicleType>();

            defaultVehicleTypes.Add(new VehicleType() { Name = "Micro" });
            defaultVehicleTypes.Add(new VehicleType() { Name = "Sedan" });
            defaultVehicleTypes.Add(new VehicleType() { Name = "Cuv" });
            defaultVehicleTypes.Add(new VehicleType() { Name = "Suv" });
            defaultVehicleTypes.Add(new VehicleType() { Name = "Hatchback" });
            defaultVehicleTypes.Add(new VehicleType() { Name = "Roadster" });
            defaultVehicleTypes.Add(new VehicleType() { Name = "Pickup" });
            defaultVehicleTypes.Add(new VehicleType() { Name = "Van" });
            defaultVehicleTypes.Add(new VehicleType() { Name = "Coupe" });
            defaultVehicleTypes.Add(new VehicleType() { Name = "Supercar" });
            defaultVehicleTypes.Add(new VehicleType() { Name = "Campervan" });
            defaultVehicleTypes.Add(new VehicleType() { Name = "Mini Truck" });
            defaultVehicleTypes.Add(new VehicleType() { Name = "Cabriolet" });
            defaultVehicleTypes.Add(new VehicleType() { Name = "Minivan" });
            defaultVehicleTypes.Add(new VehicleType() { Name = "Truck" });
            defaultVehicleTypes.Add(new VehicleType() { Name = "Big Truck" });

            context.VehicleTypes.AddRange(defaultVehicleTypes);
        }
    }
}
