using System;
using System.Collections.Generic;

namespace Pharmacy.DataAccessLayer.Models
{
    public partial class OrderMedicine
    {
        public int MedicineId { get; set; }
        public int OrderId { get; set; }
        public int Amount { get; set; }

        public virtual Medicine Medicine { get; set; }
        public virtual Order Order { get; set; }
    }
}
