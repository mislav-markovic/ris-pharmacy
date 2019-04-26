using System;
using System.Collections.Generic;

namespace Pharmacy.DataAccessLayer.Models
{
    public partial class Location
    {
        public Location()
        {
            Pharmacy = new HashSet<Pharmacy>();
            Warehouse = new HashSet<Warehouse>();
        }

        public int LocationId { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Pharmacy> Pharmacy { get; set; }
        public virtual ICollection<Warehouse> Warehouse { get; set; }
    }
}
