using System;
using System.Collections.Generic;

namespace Pharmacy.DataAccessLayer.Models
{
    public partial class WarehouseMedicine
    {
        public int WarehouseId { get; set; }
        public int MedicineId { get; set; }

        public virtual Medicine Medicine { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
