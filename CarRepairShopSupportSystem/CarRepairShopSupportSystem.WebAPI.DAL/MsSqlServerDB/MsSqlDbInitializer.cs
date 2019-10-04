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

            defaultPermissions.Add(new Permission() { Name = "SuperAdmin" });
            defaultPermissions.Add(new Permission() { Name = "Admin" });
            defaultPermissions.Add(new Permission() { Name = "User" });

            context.Permissions.AddRange(defaultPermissions);

            IList<User> defaultUsers = new List<User>();

            defaultUsers.Add(new User() { Username = "TestowyAdmin"
                                        , Salt = new byte[1]
                                        , Password = "1"
                                        , FirstName = "Grzegorz"
                                        , LastName = "Tarcz"
                                        , Email = "jakis@wp.pl"
                                        , PhoneNumber = "NieDzwonDoMnie"
                                        , CreateDate = DateTime.Now
                                        , LastLogin = DateTime.Now
                                        , PermissionId = 1
                                        , Permission = defaultPermissions[0] });

            context.Users.AddRange(defaultUsers);

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
