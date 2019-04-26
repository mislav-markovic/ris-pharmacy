using System;
using System.Collections.Generic;

namespace Pharmacy.DataAccessLayer.Models
{
    public partial class Prescription
    {
        public Prescription()
        {
            PrescriptionMedicine = new HashSet<PrescriptionMedicine>();
        }

        public int PrescriptionId { get; set; }
        public int UserId { get; set; }
        public string Buyer { get; set; }
        public DateTime SaleTime { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<PrescriptionMedicine> PrescriptionMedicine { get; set; }
    }
}
