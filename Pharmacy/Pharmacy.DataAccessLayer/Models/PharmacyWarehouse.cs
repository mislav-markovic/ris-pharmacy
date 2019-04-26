using System;
using System.Collections.Generic;

namespace Pharmacy.DataAccessLayer.Models
{
    public partial class PharmacyWarehouse
    {
        public int PharmacyId { get; set; }
        public int WarehouseId { get; set; }
        public bool CurrentlySupplies { get; set; }
        public bool CanSupply { get; set; }

        public virtual Pharmacy Pharmacy { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
