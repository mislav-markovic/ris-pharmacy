using System;
using System.Collections.Generic;

namespace Pharmacy.DataAccessLayer.Models
{
    public partial class Medicine
    {
        public Medicine()
        {
            OrderMedicine = new HashSet<OrderMedicine>();
            PrescriptionMedicine = new HashSet<PrescriptionMedicine>();
            Stockpile = new HashSet<Stockpile>();
            WarehouseMedicine = new HashSet<WarehouseMedicine>();
        }

        public int MedicineId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<OrderMedicine> OrderMedicine { get; set; }
        public virtual ICollection<PrescriptionMedicine> PrescriptionMedicine { get; set; }
        public virtual ICollection<Stockpile> Stockpile { get; set; }
        public virtual ICollection<WarehouseMedicine> WarehouseMedicine { get; set; }
    }
}
