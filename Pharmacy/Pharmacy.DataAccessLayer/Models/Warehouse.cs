using System;
using System.Collections.Generic;

namespace Pharmacy.DataAccessLayer.Models
{
    public partial class Warehouse
    {
        public Warehouse()
        {
            Order = new HashSet<Order>();
            PharmacyWarehouse = new HashSet<PharmacyWarehouse>();
            WarehouseMedicine = new HashSet<WarehouseMedicine>();
        }

        public int WarehouseId { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }

        public virtual Location Location { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<PharmacyWarehouse> PharmacyWarehouse { get; set; }
        public virtual ICollection<WarehouseMedicine> WarehouseMedicine { get; set; }
    }
}
