using System;
using System.Collections.Generic;

namespace Pharmacy.DataAccessLayer.Models
{
    public partial class Stockpile
    {
        public int StockpileId { get; set; }
        public int PharmacyId { get; set; }
        public int MedicineId { get; set; }
        public int Amount { get; set; }
        public int Alerts { get; set; }
        public int? AlertThreshold { get; set; }

        public virtual Medicine Medicine { get; set; }
        public virtual Pharmacy Pharmacy { get; set; }
    }
}
