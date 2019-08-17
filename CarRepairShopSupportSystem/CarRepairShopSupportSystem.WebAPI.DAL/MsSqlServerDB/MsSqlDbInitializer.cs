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
            int TableId = 1;
            IList<VehicleType> defaultVehicleType = new List<VehicleType>();

            defaultVehicleType.Add(new VehicleType() { VehicleTypeId = TableId, Name = "Micro" });
            defaultVehicleType.Add(new VehicleType() { VehicleTypeId = TableId++, Name = "Sedan" });
            defaultVehicleType.Add(new VehicleType() { VehicleTypeId = TableId++, Name = "Cuv" });
            defaultVehicleType.Add(new VehicleType() { VehicleTypeId = TableId++, Name = "Suv" });
            defaultVehicleType.Add(new VehicleType() { VehicleTypeId = TableId++, Name = "Hatchback" });
            defaultVehicleType.Add(new VehicleType() { VehicleTypeId = TableId++, Name = "Roadster" });
            defaultVehicleType.Add(new VehicleType() { VehicleTypeId = TableId++, Name = "Pickup" });
            defaultVehicleType.Add(new VehicleType() { VehicleTypeId = TableId++, Name = "Van" });
            defaultVehicleType.Add(new VehicleType() { VehicleTypeId = TableId++, Name = "Coupe" });
            defaultVehicleType.Add(new VehicleType() { VehicleTypeId = TableId++, Name = "Supercar" });
            defaultVehicleType.Add(new VehicleType() { VehicleTypeId = TableId++, Name = "Campervan" });
            defaultVehicleType.Add(new VehicleType() { VehicleTypeId = TableId++, Name = "Mini Truck" });
            defaultVehicleType.Add(new VehicleType() { VehicleTypeId = TableId++, Name = "Cabriolet" });
            defaultVehicleType.Add(new VehicleType() { VehicleTypeId = TableId++, Name = "Minivan" });
            defaultVehicleType.Add(new VehicleType() { VehicleTypeId = TableId++, Name = "Truck" });
            defaultVehicleType.Add(new VehicleType() { VehicleTypeId = TableId++, Name = "Big Truck" });

            context.VehicleTypes.AddRange(defaultVehicleType);
            TableId = 1;

            base.Seed(context);
        }
    }
}
