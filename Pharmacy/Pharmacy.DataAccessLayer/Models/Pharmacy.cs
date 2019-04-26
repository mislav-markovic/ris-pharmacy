using System;
using System.Collections.Generic;

namespace Pharmacy.DataAccessLayer.Models
{
    public partial class Pharmacy
    {
        public Pharmacy()
        {
            PharmacyWarehouse = new HashSet<PharmacyWarehouse>();
            Stockpile = new HashSet<Stockpile>();
            User = new HashSet<User>();
        }

        public int PharmacyId { get; set; }
        public string Name { get; set; }
        public int? LocationId { get; set; }

        public virtual Location Location { get; set; }
        public virtual ICollection<PharmacyWarehouse> PharmacyWarehouse { get; set; }
        public virtual ICollection<Stockpile> Stockpile { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
