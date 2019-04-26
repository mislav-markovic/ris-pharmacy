using System;
using System.Collections.Generic;

namespace Pharmacy.DataAccessLayer.Models
{
    public partial class User
    {
        public User()
        {
            Order = new HashSet<Order>();
            Prescription = new HashSet<Prescription>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public int UserRoleId { get; set; }
        public int PharmacyId { get; set; }

        public virtual Pharmacy Pharmacy { get; set; }
        public virtual UserRole UserRole { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<Prescription> Prescription { get; set; }
    }
}
