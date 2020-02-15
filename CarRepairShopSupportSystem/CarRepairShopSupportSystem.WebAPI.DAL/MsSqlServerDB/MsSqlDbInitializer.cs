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
    //public class MsSqlDbInitializer : CreateDatabaseIfNotExists<MsSqlServerContext>
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

            IList<OrderStatus> defaultOrderStatuses = new List<OrderStatus>();

            defaultOrderStatuses.Add(new OrderStatus() { Name = $"Zaplanowane" });
            defaultOrderStatuses.Add(new OrderStatus() { Name = $"W trakcie" });
            defaultOrderStatuses.Add(new OrderStatus() { Name = $"Zakończone" });
            defaultOrderStatuses.Add(new OrderStatus() { Name = $"Niezrealizowana" });

            context.OrderStatuses.AddRange(defaultOrderStatuses);

            IList<Permission> defaultPermissions = new List<Permission>();

            defaultPermissions.Add(new Permission() { Name = $"{PermissionId.SuperAdmin}" });
            defaultPermissions.Add(new Permission() { Name = $"{PermissionId.Admin}" });
            defaultPermissions.Add(new Permission() { Name = $"{PermissionId.User}" });
            defaultPermissions.Add(new Permission() { Name = $"{PermissionId.Guest}" });

            context.Permissions.AddRange(defaultPermissions);

            IList<Service> defaultServices = new List<Service>();

            defaultServices.Add(new Service() { Name = "Diagnoza pojazdu", Description = string.Empty, Price = 50.00M, ExecutionTimeInMinutes = 30 });
            defaultServices.Add(new Service() { Name = "Wymiana opoy", Description = string.Empty, Price = 20.00M, ExecutionTimeInMinutes = 40 });
            defaultServices.Add(new Service() { Name = "Wymiana oleju", Description = string.Empty, Price = 50.00M, ExecutionTimeInMinutes = 50 });
            defaultServices.Add(new Service() { Name = "Wymiana klocków hamulcowych", Description = string.Empty, Price = 100.00M, ExecutionTimeInMinutes = 60 });

            context.Services.AddRange(defaultServices);

            IList<User> defaultUsers = new List<User>();

            defaultUsers.Add(new User() { Username = "TestSuperAdmin"
                                        , Salt = new byte[32] { 20, 10, 150, 5, 15, 45, 60, 80,
                                                                20, 10, 150, 5, 15, 45, 60, 80,
                                                                20, 10, 150, 5, 15, 45, 60, 80,
                                                                20, 10, 150, 5, 15, 45, 60, 80 }
                                        , Password = "NO8Qsz2QXnlMQ4x9K1tNzhsDVhudR9QOai2DSfRRZ2UOMePNt5NqjnGksUJtBNwuYC7lQpFsJr1hcQLUWiBjcQ==" //"1"
                                        , FirstName = "Grzegorz"
                                        , LastName = "Tarcz"
                                        , Email = "jakis@wp.pl"
                                        , PhoneNumber = 986594394
                                        , CreateDate = DateTime.Now
                                        , LastLogin = DateTime.Now
                                        , PermissionId = 1
                                        , Permission = defaultPermissions[0] });

            defaultUsers.Add(new User() { Username = "TestAdmin"
                                        , Salt = new byte[32] { 20, 10, 150, 5, 15, 45, 60, 80,
                                                                20, 10, 150, 5, 15, 45, 60, 80,
                                                                20, 10, 150, 5, 15, 45, 60, 80,
                                                                20, 10, 150, 5, 15, 45, 60, 80 }
                                        , Password = "NO8Qsz2QXnlMQ4x9K1tNzhsDVhudR9QOai2DSfRRZ2UOMePNt5NqjnGksUJtBNwuYC7lQpFsJr1hcQLUWiBjcQ==" //"1"
                                        , FirstName = "Grzegorz"
                                        , LastName = "TestAdmin"
                                        , Email = "jakis@wp.pl"
                                        , PhoneNumber = 986594394
                                        , CreateDate = DateTime.Now
                                        , LastLogin = DateTime.Now
                                        , PermissionId = 2
                                        , Permission = defaultPermissions[1] });

            defaultUsers.Add(new User() { Username = "TestUser"
                                        , Salt = new byte[32] { 20, 10, 150, 5, 15, 45, 60, 80,
                                                                20, 10, 150, 5, 15, 45, 60, 80,
                                                                20, 10, 150, 5, 15, 45, 60, 80,
                                                                20, 10, 150, 5, 15, 45, 60, 80 }
                                        , Password = "NO8Qsz2QXnlMQ4x9K1tNzhsDVhudR9QOai2DSfRRZ2UOMePNt5NqjnGksUJtBNwuYC7lQpFsJr1hcQLUWiBjcQ==" //"1"
                                        , FirstName = "Grzegorz"
                                        , LastName = "TestUser"
                                        , Email = "jakis@wp.pl"
                                        , PhoneNumber = 986594394
                                        , CreateDate = DateTime.Now
                                        , LastLogin = DateTime.Now
                                        , PermissionId = 3
                                        , Permission = defaultPermissions[2] });

            defaultUsers.Add(new User() { Username = "TestGuest"
                                        , Salt = new byte[32] { 20, 10, 150, 5, 15, 45, 60, 80,
                                                                20, 10, 150, 5, 15, 45, 60, 80,
                                                                20, 10, 150, 5, 15, 45, 60, 80,
                                                                20, 10, 150, 5, 15, 45, 60, 80 }
                                        , Password = "NO8Qsz2QXnlMQ4x9K1tNzhsDVhudR9QOai2DSfRRZ2UOMePNt5NqjnGksUJtBNwuYC7lQpFsJr1hcQLUWiBjcQ==" //"1"
                                        , FirstName = "Wiesław"
                                        , LastName = "TestGuest"
                                        , Email = "jakis@wp.pl"
                                        , PhoneNumber = 986594394
                                        , CreateDate = DateTime.Now
                                        , LastLogin = DateTime.Now
                                        , PermissionId = 4
                                        , Permission = defaultPermissions[3] });

            context.Users.AddRange(defaultUsers);

            IList<Timetable> defaultTimetables = new List<Timetable>();

            System.Globalization.Calendar calendar = new System.Globalization.GregorianCalendar();
            int daysInMonth = calendar.GetDaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            DateTime dateTimeFirstDayOfMouth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DayOfWeek dayOfWeekFirstDayOfMouth = calendar.GetDayOfWeek(dateTimeFirstDayOfMouth);

            for (int i = 1; i <= daysInMonth; i++)
            {
                if ((i + (int)dayOfWeekFirstDayOfMouth) % 7 > 1) //  1, 7, 8, 14, 15, 21, 22, 28, 29, 35, 36
                {
                    for (int k = 8; k < 16; k++)
                    {
                        defaultTimetables.Add(new Timetable()
                        {
                            DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, i, k, 0, 0),
                            NumberOfEmployeesForCustomer = 2,
                            NumberOfEmployeesReservedForCustomer = 0,
                            NumberOfEmployeesForManager = 2,
                            NumberOfEmployeesReservedForManager = 0,
                            WorkingUsers = new List<User> { defaultUsers[1] }
                        });
                    }
                }
            }

            context.Timetables.AddRange(defaultTimetables);

            IList<VehicleBrand> defaultVehicleBrands = new List<VehicleBrand>();

            defaultVehicleBrands.Add(new VehicleBrand() { Name = "Audi" });
            defaultVehicleBrands.Add(new VehicleBrand() { Name = "BMW" });
            defaultVehicleBrands.Add(new VehicleBrand() { Name = "Mercedes" });

            context.VehicleBrands.AddRange(defaultVehicleBrands);

            IList<VehicleEngine> defaultVehicleEngines = new List<VehicleEngine>();

            defaultVehicleEngines.Add(new VehicleEngine() { EngineCode = "ALZ", CapacityCCM = 1595, PowerKW = 75 });
            defaultVehicleEngines.Add(new VehicleEngine() { EngineCode = "BFB", CapacityCCM = 1781, PowerKW = 120 });
            defaultVehicleEngines.Add(new VehicleEngine() { EngineCode = "BKE", CapacityCCM = 1896, PowerKW = 85 });

            context.VehicleEngines.AddRange(defaultVehicleEngines);

            IList<VehicleFuel> defaultVehicleFuels = new List<VehicleFuel>();

            defaultVehicleFuels.Add(new VehicleFuel() { Name = "Benzyna" });
            defaultVehicleFuels.Add(new VehicleFuel() { Name = "Olej napędowy" });
            defaultVehicleFuels.Add(new VehicleFuel() { Name = "LPG" });

            context.VehicleFuels.AddRange(defaultVehicleFuels);

            IList<VehicleModel> defaultVehicleModels = new List<VehicleModel>();

            defaultVehicleModels.Add(new VehicleModel() { Name = "A8", VehicleBrandId = 1 });
            defaultVehicleModels.Add(new VehicleModel() { Name = "Q8", VehicleBrandId = 1 });
            defaultVehicleModels.Add(new VehicleModel() { Name = "Serii 1", VehicleBrandId = 2 });
            defaultVehicleModels.Add(new VehicleModel() { Name = "X7", VehicleBrandId = 2 });
            defaultVehicleModels.Add(new VehicleModel() { Name = "Klasa C", VehicleBrandId = 3 });
            defaultVehicleModels.Add(new VehicleModel() { Name = "Klasa E", VehicleBrandId = 3 });

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

            IList<VehiclePart> defaultVehicleParts = new List<VehiclePart>();

            defaultVehicleParts.Add(new VehiclePart() { Name = "Opona", Price = 150.00M });
            defaultVehicleParts.Add(new VehiclePart() { Name = "Felga", Price = 120.00M });
            defaultVehicleParts.Add(new VehiclePart() { Name = "Olej", Price = 150.00M });
            defaultVehicleParts.Add(new VehiclePart() { Name = "Filtr", Price = 100.00M });

            context.VehicleParts.AddRange(defaultVehicleParts);

            IList<Vehicle> defaultVehicles = new List<Vehicle>();

            defaultVehicles.Add(new Vehicle() { EngineMileage = 123
                                                , RegistrationNumbers = "123"
                                                , Description = string.Empty
                                                , UserId = 1
                                                , User = defaultUsers[0]
                                                , VehicleEngineId = 1
                                                , VehicleEngine = defaultVehicleEngines[0]
                                                , VehicleFuelId = 1
                                                , VehicleFuel = defaultVehicleFuels[0]
                                                , VehicleModelId = 1
                                                , VehicleModel = defaultVehicleModels[0]
                                                , VehicleTypeId = 1
                                                , VehicleType = defaultVehicleTypes[0] });

            context.Vehicles.AddRange(defaultVehicles);
        }
    }
}
