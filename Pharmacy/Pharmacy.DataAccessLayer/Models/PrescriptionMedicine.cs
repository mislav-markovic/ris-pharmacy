using System;
using System.Collections.Generic;

namespace Pharmacy.DataAccessLayer.Models
{
    public partial class PrescriptionMedicine
    {
        public int PrescriptionId { get; set; }
        public int MedicineId { get; set; }
        public int Amount { get; set; }
        public int PrescriptionMedicineId { get; set; }

        public virtual Medicine Medicine { get; set; }
        public virtual Prescription Prescription { get; set; }
    }
}
