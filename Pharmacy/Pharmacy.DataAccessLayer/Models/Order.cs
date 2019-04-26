using System;
using System.Collections.Generic;

namespace Pharmacy.DataAccessLayer.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderMedicine = new HashSet<OrderMedicine>();
        }

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderIssuedTime { get; set; }
        public DateTime OrderFulfilledTime { get; set; }
        public int WarehouseId { get; set; }

        public virtual User User { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual ICollection<OrderMedicine> OrderMedicine { get; set; }
    }
}
